namespace AoC.Y2022.Day02;

public class Day02 : IAoCRunner<string[], int>
{
    const int RockPoints = 1;
    const int PaperPoints = 2;
    const int ScissorsPoints = 3;

    const int LosePoints = 0;
    const int TiePoints = 3;
    const int WinPoints = 6;

    public string[] ParseInput(string[] puzzleInput) => puzzleInput;

    public int RunPart1(string[] input) => input
        .Sum(t => t switch
        {
            "A X" => RockPoints     + TiePoints,    // rock ties with rock
            "B X" => RockPoints     + LosePoints,   // rock loses to paper
            "C X" => RockPoints     + WinPoints,    // rock beats scissors
            "A Y" => PaperPoints    + WinPoints,    // paper beats rock
            "B Y" => PaperPoints    + TiePoints,    // paper ties with paper
            "C Y" => PaperPoints    + LosePoints,   // paper loses to scissors
            "A Z" => ScissorsPoints + LosePoints,   // scissors loses to scissors
            "B Z" => ScissorsPoints + WinPoints,    // scissors beats paper
            "C Z" => ScissorsPoints + TiePoints,    // scissors ties with scissors
            _ => throw new InvalidOperationException("Invalid input")
        });

    public int RunPart2(string[] input) => input
        .Sum(t => t switch
        {
            "A X" => LosePoints + ScissorsPoints,   // rock beats scissors
            "B X" => LosePoints + RockPoints,       // paper beats rock
            "C X" => LosePoints + PaperPoints,      // scissors beats paper
            "A Y" => TiePoints  + RockPoints,       // rock ties with rock
            "B Y" => TiePoints  + PaperPoints,      // paper ties with paper
            "C Y" => TiePoints  + ScissorsPoints,   // scissors ties with scissors
            "A Z" => WinPoints  + PaperPoints,      // rock loses to paper
            "B Z" => WinPoints  + ScissorsPoints,   // paper loses to scissors
            "C Z" => WinPoints  + RockPoints,       // scissors loses to rock
            _ => throw new InvalidOperationException("Invalid input")
        });
}
