using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class CoinManager : Node
{
    public static CoinManager Instance { get; private set; }
    public Dictionary<string, CoinResource> Coins { get; set; }
	public override void _Ready()
    {
        Instance = this;
        Coins = new();
        string path = "res://coins/";

        using var dir = DirAccess.Open(path);
        if (dir != null)
        {
            dir.ListDirBegin();
            string fileName = dir.GetNext();

            while (fileName != "")
            {
                // Only load .tres files (ignore .import or folders)
                if (!dir.CurrentIsDir() && fileName.EndsWith(".tres"))
                {
                    var res = GD.Load<CoinResource>(path + fileName);
                    if (res != null)
                    {
                        Coins.Add(res.ID, res);
                    }
                }
                fileName = dir.GetNext();
            }
        }
        else
        {
            GD.PrintErr($"Failed to open directory: {path}");
        }

        Coins = new Dictionary<string, CoinResource>(Coins.OrderBy(kv => kv.Value.Price).ToDictionary(kv => kv.Key, kv => kv.Value));
    }
}
