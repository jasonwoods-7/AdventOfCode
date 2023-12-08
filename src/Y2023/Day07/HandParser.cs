namespace AoC.Y2023.Day07;

partial record Hand
{
    public static Hand ParseHand(string rawHand, Card jCard)
    {
        var parts = rawHand.Split(' ');

        var cards = parts[0]
            .Select(c => c switch
            {
                '2' => Card.Two,
                '3' => Card.Three,
                '4' => Card.Four,
                '5' => Card.Five,
                '6' => Card.Six,
                '7' => Card.Seven,
                '8' => Card.Eight,
                '9' => Card.Nine,
                'T' => Card.Ten,
                'J' => jCard,
                'Q' => Card.Queen,
                'K' => Card.King,
                _ => Card.Ace
            })
            .ToArray();

        var bet = parts[1].ParseNumber<int>();

        var kind = HandScorer(cards);

        return new Hand(cards, bet, kind);
    }

    static HandKind HandScorer(Card[] cards) => cards
        .Where(static c => c != Card.Joker)
        .GroupBy(static c => c)
        .Select(static g => g.Count())
        .OrderByDescending(static c => c)
        .ToArray() switch
        {
            [5] => HandKind.FiveOfAKind,
            [4] => HandKind.FiveOfAKind,
            [3] => HandKind.FiveOfAKind,
            [2] => HandKind.FiveOfAKind,
            [1] => HandKind.FiveOfAKind,
            [] => HandKind.FiveOfAKind,
            [4, 1] => HandKind.FourOfAKind,
            [3, 1] => HandKind.FourOfAKind,
            [2, 1] => HandKind.FourOfAKind,
            [1, 1] => HandKind.FourOfAKind,
            [3, 2] => HandKind.FullHouse,
            [2, 2] => HandKind.FullHouse,
            [3, 1, 1] => HandKind.ThreeOfAKind,
            [2, 1, 1] => HandKind.ThreeOfAKind,
            [1, 1, 1] => HandKind.ThreeOfAKind,
            [2, 2, 1] => HandKind.TwoPair,
            [2, 1, 1, 1] => HandKind.OnePair,
            [1, 1, 1, 1] => HandKind.OnePair,
            [1, 1, 1, 1, 1] => HandKind.HighCard,
            _ => throw new InvalidOperationException(string.Join(", ", cards))
        };
}
