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
    public readonly List<Card> CommunityCards = new(5);
    public int DealerIndex { get; private set; } = 0;
    public int SbIndex { get; private set; } = 0;
    public int BbIndex { get; private set; } = 0;
    public int CurrentPlayerIndex { get; private set; } = 0;
    public int CurrentBet { get; set; } = 0;

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

        // Pre-flop
        Console.WriteLine("Pre-Flop");
        _players[SbIndex].MakeBlindBet(EngineOptions.BigBlind / 2);
        _players[BbIndex].MakeBlindBet(EngineOptions.BigBlind);
        CurrentBet = EngineOptions.BigBlind;
        StartBettingRound();
        ResetHasBet();
        CurrentPlayerIndex = GetNextBlindIndex(DealerIndex);

        // Flop
        Console.WriteLine("Flop");
        _deck.NextCard();
        CommunityCards.AddRange(_deck.NextCards(3));
        StartBettingRound();
        ResetHasBet();
        CurrentPlayerIndex = GetNextBlindIndex(DealerIndex);

        // Turn
        Console.WriteLine("Turn");
        _deck.NextCard();
        CommunityCards.Add(_deck.NextCard());
        StartBettingRound();
        ResetHasBet();
        CurrentPlayerIndex = GetNextBlindIndex(DealerIndex);

        // River
        Console.WriteLine("River");
        _deck.NextCard();
        CommunityCards.Add(_deck.NextCard());
        StartBettingRound();
        ResetHasBet();
        CurrentPlayerIndex = GetNextBlindIndex(DealerIndex);
    }

    private void StartBettingRound()
    {
        while (true)
        {
            EnginePlayer player = _players[CurrentPlayerIndex];
            if (player.HasBet) break;
            var input = _io.GetInput(BuildGameState());
            player.MakeBet(input.Amount);
            if (player.Bet > CurrentBet) CurrentBet = player.Bet;
            CurrentPlayerIndex = GetNextBlindIndex(CurrentPlayerIndex);
        }
    }

    private void ResetHasBet()
    {
        foreach (var p in _players)
        {
            p.HasBet = false;
        }
    }

    private GameState BuildGameState()
    {
        List<PlayerState> playerStates = new();
        foreach (var player in _players)
        {
            playerStates.Add(new PlayerState { Id = player.Name, Stack = player.Stack, HoleCards = player.HoleCards, Bet = player.Bet });
        }
        PlayerState currentPlayer = playerStates[CurrentPlayerIndex];
        int minBet = CurrentBet - currentPlayer.Bet;
        return new GameState
        {
            PlayerStates = playerStates,
            CommunityCards = new List<Card>(CommunityCards),
            MinBet = minBet,
            OutputType = OutputType.InputRequest,
            PlayerToAct = currentPlayer,
            PossibleMoves = new List<PlayerMove> { PlayerMove.Call, PlayerMove.Check },
        };
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

    public void PrintGameState()
    {
        Console.WriteLine("-- Game State --");
        Console.WriteLine("Players: ");
        foreach (var item in _players)
        {
            Console.Write(_players.IndexOf(item) == CurrentPlayerIndex ? " ➡️  " : "");
            Console.Write(item + (_players.IndexOf(item) == DealerIndex ? "   🎃 DEALER" : _players.IndexOf(item) == SbIndex ? "   🥈 SMALL BLIND" : _players.IndexOf(item) == BbIndex ? "   🪙  BIG BLIND" : ""));
            Console.WriteLine(item.HasBet ? "  ✅" : "");
        }
        Console.WriteLine("\nCommunity Cards:");
        foreach (var item in CommunityCards)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }
}
