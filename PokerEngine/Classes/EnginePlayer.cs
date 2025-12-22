namespace PokerEngine;

public class EnginePlayer : Player
{
    public int Stack { get; private set; }
    public int Bet { get; private set; } = 0;
    public bool HasBet = false;

    public EnginePlayer(string name, int stack, Card first, Card second) : base(name, first, second)
    {
        Stack = stack;
    }

    public EnginePlayer(PlayerInfo playerInfo, int stack, Card first, Card second) : base(playerInfo.Id.ToString(), first, second)
    {
        Stack = stack;
    }

    public void MakeBet(int amount)
    {
        MakeBlindBet(amount);
        HasBet = true;
    }

    public void MakeBlindBet(int amount)
    {
        if (amount > Stack)
        {
            Bet = Stack;
            Stack = 0;
            return;
        }
        Stack -= amount;
        Bet += amount;
    }

    public override string ToString()
    {
        return $"Name: {Name} | Hand: {HoleCards} | Stack: {Stack} | Bet: {Bet}";
    }
}