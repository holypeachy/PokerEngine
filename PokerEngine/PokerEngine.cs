namespace PokerEngine;

public class PokerEngine
{
    // Engine
    public readonly PokerEngineOptions EngineOptions;
    private readonly IEngineIO _io;

    // Table
    private readonly Deck _deck;
    private readonly List<EnginePlayer> _players;

    // Hand
    public int DealerIndex { get; private set; } = 0;
    public int SbIndex { get; private set; } = 0;
    public int BbIndex { get; private set; } = 0;
    public int CurrentPlayerIndex { get; private set; } = 0;

    public PokerEngine(PokerEngineOptions options, IEngineIO io)
    {
        EngineOptions = options;
        _io = io;
        _players = [];
        _deck = new();
    }

    public void InitializeTable(List<PlayerInfo> playersInfo)
    {
        _players.Clear();
        _deck.ResetDeck();
        DealerIndex = -1;

        foreach (var pi in playersInfo)
        {
            _players.Add(new EnginePlayer(pi, EngineOptions.BuyIn, _deck.NextCard(), _deck.NextCard()));
        }
    }

    public void StartHand()
    {
        _deck.ResetDeck();
        AssignBlinds();
        foreach (var p in _players)
        {
            p.NewHand(_deck.NextCard(), _deck.NextCard());
            Console.WriteLine(p);
        }
        Console.WriteLine();

        _io.GetInput();
    }

    private void AssignBlinds()
    {
        DealerIndex = GetNextBlindIndex(DealerIndex);
        SbIndex = GetNextBlindIndex(DealerIndex);
        BbIndex = GetNextBlindIndex(SbIndex);
        CurrentPlayerIndex = GetNextBlindIndex(BbIndex);
    }

    private int GetNextBlindIndex(int index)
    {
        int temp = index + 1;
        if (temp > _players.Count - 1) temp = 0;
        return temp;
    }
}
