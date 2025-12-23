
namespace PokerEngine;

public class EnginePlayer
{
    public string Name { get; }
    public Pair HoleCards { get; private set; }
    public int Stack { get; private set; }

    public int Bet { get; private set; } = 0;
    public bool HasActed { get; set; } = false;
    public bool HasFolded { get; set; } = false;

    public EnginePlayer(string name, int stack, Card first, Card second)
    {
        Name = name;
        Stack = stack;
        HoleCards = new Pair(first, second);
    }

    public EnginePlayer(PlayerInfo playerInfo, int stack, Card first, Card second)
    {
        Name = playerInfo.Id;
        Stack = stack;
        HoleCards = new Pair(first, second);
    }

    public void ResetTableHand()
    {
        Bet = 0;
        HasActed = false;
        HasFolded = false;
    }

    public void Pay(int amount)
    {
        Stack += amount;
    }

    public void Fold()
    {
        if (HasFolded) throw new Exception();
        HasFolded = true;
    }

    public void Check()
    {
        HasActed = true;
    }

    public void MakeBet(int amount)
    {
        MakeBlindBet(amount);
        HasActed = true;
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

    public void NewHand(Card first, Card second)
    {
        HoleCards = new(first, second);
    }

    public override string ToString()
    {
        return $"Name: {Name} | Hand: {HoleCards} | Stack: {Stack} | Bet: {Bet}";
    }
}