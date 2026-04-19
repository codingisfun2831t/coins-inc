using Godot;
using Godot.Collections;
using System;

public partial class SaveManager : Node
{
    // save path
    private string savePath = "user://savegame.dat";

    // instane
    public static SaveManager Instance { get; private set; }

    // save data!

    public int Coins { get; set; } = 0;

    public Array<string> BoughtCoins { get; set; }

    public override void _Ready()
    {
        Instance = this;

        LoadGame();
    }

    public void SaveGame()
    {
        // get dictionary from data
        var data = new Dictionary
        {
            { "coins", Coins },
            { "boughtCoins", BoughtCoins }
        };

        // save it
        using var file = FileAccess.Open(savePath, FileAccess.ModeFlags.Write);
        if (file != null)
        {
            file.StoreVar(data);
        }
    }

    public void LoadGame()
    {
        if (!FileAccess.FileExists(savePath)) {
            // in case, save current
            SaveGame();
        }

        // load...
        using var file = FileAccess.Open(savePath, FileAccess.ModeFlags.Read);
        if (file != null)
        {
            // GetVar returns a Variant, so cast it to expected type
            var loadedData = (Dictionary)file.GetVar();

            _loadDict(loadedData);
        }
    }

    private void _loadDict(Dictionary data)
    {
        bool has(string key) => data.ContainsKey(key);
        Variant get(string key) => data[key];

        if (has("coins")) Coins = (int)get("coins");
        else Coins = 0;

        if (has("boughtCoins")) BoughtCoins = (Array<string>)get("boughtCoins");
        else BoughtCoins = new() { "coin" };
    }

    public CoinResource RandomBoughtCoin()
    {
        int idx = GD.RandRange(0, BoughtCoins.Count - 1);
        string id = BoughtCoins[idx];

        return CoinManager.Instance.Coins[id];
    }

    public override void _Notification(int what)
    {
        if (what == NotificationWMCloseRequest)
        {
            SaveGame();
            GetTree().Quit();
        }
    }
}