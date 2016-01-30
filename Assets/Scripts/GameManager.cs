using UnityEngine;
using System.Collections.Generic;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public GameObject PlayerPrefab;

    private List<GameObject> Players;

    public void Start()
    {
        Players = new List<GameObject>();

        if (Main.Instance == null)
        {
            AddPlayer(0, 0);
            AddPlayer(1, 1);
        }
        else
        {
            foreach (var player in Main.Instance.GameData.PlayerData)
            {
                AddPlayer(player.PlayerID, player.MaskID);
            }
        }
    }

    private void AddPlayer(int playerId, int maskId)
    {
        var player = (GameObject)Instantiate(PlayerPrefab, GetSpawnPoint(),Quaternion.identity);
        player.GetComponent<Player>().Init(playerId, maskId);
        Players.Add(player);
    }

    private Vector3 GetSpawnPoint()
    {
        return Vector3.zero;
    }
}
