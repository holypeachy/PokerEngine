namespace PokerEngine;

public record PlayerState
{
    public string Id;
    public Pair HoleCards;
    public int Stack;
    public int Bet;
}