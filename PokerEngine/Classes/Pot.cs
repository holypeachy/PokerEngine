using System.Diagnostics;

namespace PokerEngine;

public class Pot(int value, List<EnginePlayer> players)
{
    public List<EnginePlayer> Players { get; private set; } = players;
    public int Value { get; private set; } = value;
    public List<EnginePlayer>? Winners { get; set; } = null;

    public void PayWinners()
    {
        Debug.Assert(Winners is not null && Winners.Count > 0, "Winners should never be null when paying winners. This means we never determined the winners of this pot.");

        int split = Value / Winners.Count;
        foreach (var w in Winners)
        {
            w.Pay(split);
        }
    }

    public override string ToString()
    {
        string players = "| ";
        foreach (var item in Players)
        {
            players += item.Name + " | ";
        }

        string wString = string.Empty;
        if (Winners is not null)
        {
            foreach (EnginePlayer w in Winners)
            {
                wString += $"\t{w.Name} ({Value / Winners.Count()}) | {w.Stack} => {w.Stack + Value / Winners.Count()}\n";
            }
        }

        return $"players ({Players.Count}): {players}\nvalue: {Value}\nwinner(s):\n{wString}";
    }
}