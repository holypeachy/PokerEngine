namespace PokerEngine.Sandbox;

public class EngineIO : IEngineIO
{
    private PokerEngine _engine;

    public PlayerInput GetInput(GameState gameState)
    {
        _engine.PrintGameState();
        Console.WriteLine("IO Request");
        Console.WriteLine($"Output Type: {gameState.OutputType}");
        Console.WriteLine( "Current Player: " + (gameState.PlayerToAct is not null ? gameState.PlayerToAct.Id : "null"));
        Console.Write("Possible Moves: ");
        if (gameState.PossibleMoves is not null)
        {
            foreach (var item in gameState.PossibleMoves)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("null");
        }
        Console.WriteLine($"Min Bet: {gameState.MinBet}");
        Console.ReadLine();

        if (gameState.CommunityCards.Count == 0) return new PlayerInput { Move = PlayerMove.Call, Amount = gameState.MinBet };
        else return new PlayerInput { Move = PlayerMove.Call, Amount = 10 };
    }

    public void SetEngine(PokerEngine engine)
    {
        _engine = engine;
    }
}