namespace PokerEngine;

public static class PotAlgo
{
    class ChipTracker(EnginePlayer owner, int value, bool HasFolded)
    {
        public EnginePlayer Owner { get; } = owner;
        public int Value { get; set; } = value;
        public bool HasFolded { get; } = HasFolded;

        public override string ToString()
        {
            return $"owner: {Owner.Name} | value: {Value} | folded: {HasFolded}";
        }
    }

    public static List<Pot> GetPots(List<EnginePlayer> players)
    {
        bool atLeastOneNonFolded = false;
        foreach (var item in players)
        {
            if (!item.HasFolded)
            {
                atLeastOneNonFolded = true;
                break;
            }
        }
        if (atLeastOneNonFolded == false) throw new Exception();

        List<ChipTracker> trackers = [];
        foreach (EnginePlayer p in players)
        {
            if (p.Bet != 0)
            {
                trackers.Add(new ChipTracker(p, p.Bet, p.HasFolded));
            }
        }

        if (trackers.Count == 0) throw new Exception();

        Console.WriteLine("\nTrackers:");
        foreach (ChipTracker t in trackers)
        {
            Console.WriteLine(t);
        }
        Console.WriteLine();

        return SplitPot(trackers);
    }

    private static List<Pot> SplitPot(List<ChipTracker> trackers)
    {
        // end condition
        if (trackers.Count == 0) return [];

        // pot splitting logic
        Pot pot;
        int min = GetMin(trackers);
        int potTotal = 0;
        int foldedTotal = 0;
        List<EnginePlayer> potPlayers = [];

        // loop through trackers and decrease value
        foreach (ChipTracker t in trackers)
        {
            if (t.HasFolded)
            {
                if (t.Value <= min)
                {
                    foldedTotal += t.Value;
                    t.Value = 0;
                }
                else
                {
                    foldedTotal += min;
                    t.Value -= min;
                }
            }
            else
            {
                potPlayers.Add(t.Owner);
                potTotal += min;
                t.Value -= min;
            }
        }

        pot = new Pot(potTotal + foldedTotal, potPlayers);

        // prepare trackers for next recursion
        trackers.RemoveAll(t => t.Value == 0);

        // we combine all the pots
        List<Pot> pots = [pot];
        pots.AddRange(SplitPot(trackers));
        return pots;
    }

    private static int GetMin(List<ChipTracker> trackers)
    {
        // add guard for if all remaining trackers are folded, should throw error
        int min = int.MaxValue;
        bool found = false;

        foreach (var t in trackers)
        {
            if (t.HasFolded) continue;

            found = true;
            if (t.Value < min) min = t.Value;
        }

        if (!found)
            throw new InvalidOperationException("GetMin() called with no non-folded trackers remaining.");

        return min;
    }

}