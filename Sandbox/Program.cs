namespace PokerEngine.Sandbox;
class Program
{
    static void Main()
    {
        var engineOptions = new PokerEngineOptions { BuyIn = 1000 };
        EngineIO io = new();
        PokerEngine engine = new(engineOptions, io);

        List<PlayerInfo> playersInfo =
        [
            new PlayerInfo("Alpha"),
            new PlayerInfo("Tango"),
            new PlayerInfo("Sierra"),
            new PlayerInfo("Quebec"),
            new PlayerInfo("Zulu"),
        ];

        engine.InitializeTable(playersInfo);
        engine.StartHand();
    }
}