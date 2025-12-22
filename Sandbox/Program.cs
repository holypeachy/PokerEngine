namespace PokerEngine.Sandbox;

class Program
{
    static void Main()
    {
        var engineOptions = new PokerEngineOptions { BuyIn = 1000, BigBlind = 50 };
        EngineIO io = new();
        PokerEngine engine = new(engineOptions, io);
        io.SetEngine(engine);

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

/*
! ISSUES:
! 

TODO
TODO: Add detailed and standardized logging, log to file as well

? Future Ideas
? 

* Notes
* 

* Changes
* 
*/