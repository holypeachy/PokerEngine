namespace PokerEngine.Sandbox;

public class EngineIO : IEngineIO
{
    private PokerEngine _engine;

    public PlayerInput GetInput(GameState gameState)
    {
        Dictionary<int, PlayerMove> moves = new();
        _engine.PrintGameState();
        Console.WriteLine("IO Request");
        Console.WriteLine($"Output Type: {gameState.OutputType}");
        Console.WriteLine( "Current Player: " + (gameState.PlayerToAct is not null ? gameState.PlayerToAct.Id : "null"));
        Console.Write("Possible Moves: ");
        int count = 1;
        if (gameState.PossibleMoves is not null)
        {
            foreach (var item in gameState.PossibleMoves)
            {
                Console.Write($"{count}-" + item + " ");
                moves.Add(count++, item);
            }
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("null");
        }
        Console.WriteLine($"Min Bet: {gameState.MinBet}");
        Console.WriteLine($"{nameof(_engine.AdditionalRaiseCount)}: {_engine.AdditionalRaiseCount}");
        Console.WriteLine("Select your move:");
        string? moveIn = Console.ReadLine();
        if (string.IsNullOrEmpty(moveIn)) throw new Exception();
        Console.WriteLine("Type amount:");
        int amount = 0;
        if (!int.TryParse(Console.ReadLine(), out amount)) throw new Exception();
        PlayerMove selectedMove = moves[int.Parse(moveIn)];
        
        return new PlayerInput { Move = selectedMove, Amount = amount };
    }

    public void SetEngine(PokerEngine engine)
    {
        _engine = engine;
    }
}