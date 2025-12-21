namespace PokerEngine;

public class EnginePlayer : Player
{
    public int Stack { get; }
    public int Bet { get; } = 0;

    public EnginePlayer(string name, int stack, Card first, Card second) : base(name, first, second)
    {
        Stack = stack;
    }

    public EnginePlayer(PlayerInfo playerInfo, int stack, Card first, Card second) : base(playerInfo.Id.ToString(), first, second)
    {
        Stack = stack;
    }

    public override string ToString()
    {
        return $"Name: {Name} | Hand: {HoleCards} | Stack: {Stack} | Bet: {Bet}";
    }
}