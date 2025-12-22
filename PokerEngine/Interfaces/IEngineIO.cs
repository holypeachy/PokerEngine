namespace PokerEngine;

public interface IEngineIO
{
    PlayerInput GetInput(GameState gameState);
}