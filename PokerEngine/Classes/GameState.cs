namespace PokerEngine;

public record GameState
{
    public List<PlayerState> PlayerStates;
    public List<Card> CommunityCards;
    public OutputType OutputType;
    
    public PlayerState? PlayerToAct;
    public List<PlayerMove>? PossibleMoves;
    public int MinBet;
}