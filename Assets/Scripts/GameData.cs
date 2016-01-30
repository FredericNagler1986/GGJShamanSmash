using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public struct GamePlayerData
{
    [SerializeField]
    public int PlayerID;
    [SerializeField]
    public int MaskID;

    public GamePlayerData(int playerID, int maskID)
    {
        PlayerID = playerID;
        MaskID = maskID;
    }
}

[Serializable]
public class GameData
{
    [SerializeField]
    List<GamePlayerData> mPlayerData = new List<GamePlayerData>();

    public List <GamePlayerData> PlayerData
    {
        get
        {
            return mPlayerData;
        }
    }

    public GameData()
    {

    }

    public void AddPlayerPlayerData (int playerID, int maskID)
    {
        mPlayerData.Add(new GamePlayerData(playerID, maskID));
    }
}

