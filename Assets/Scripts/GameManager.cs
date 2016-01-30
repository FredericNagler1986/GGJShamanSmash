using UnityEngine;
using System.Collections.Generic;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public GameObject PlayerPrefab;

    private List<GameObject> Players;

    public void Start()
    {
        Players = new List<GameObject>();
        AddPlayer(0, 0);
        AddPlayer(1, 1);
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
