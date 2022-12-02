namespace AoC.Y2022.Day02;

public class Day02 : IAoCRunner<string[], int>
{
    public string[] ParseInput(string[] puzzleInput) => puzzleInput;

    public int RunPart1(string[] input) => input
        .Select(t => t switch
        {
            "A X" => 1 + 3, // rock ties with rock
            "B X" => 1 + 0, // rock loses to paper
            "C X" => 1 + 6, // rock beats scissors
            "A Y" => 2 + 6, // paper beats rock
            "B Y" => 2 + 3, // paper ties with paper
            "C Y" => 2 + 0, // paper loses to scissors
            "A Z" => 3 + 0, // scissors loses to scissors
            "B Z" => 3 + 6, // scissors beats paper
            "C Z" => 3 + 3, // scissors ties with scissors
            _ => 0
        })
        .Sum();

    public int RunPart2(string[] input) => input
        .Select(t => t switch
        {
            "A X" => 3 + 0, // rock beats scissors
            "B X" => 1 + 0, // paper beats rock
            "C X" => 2 + 0, // scissors beats paper
            "A Y" => 1 + 3, // rock ties with rock
            "B Y" => 2 + 3, // paper ties with paper
            "C Y" => 3 + 3, // scissors ties with scissors
            "A Z" => 2 + 6, // rock loses to paper
            "B Z" => 3 + 6, // paper loses to scissors
            "C Z" => 1 + 6, // scissors loses to rock
            _ => 0
        })
        .Sum();
}
