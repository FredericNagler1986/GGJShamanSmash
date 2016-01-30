using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public GameObject PlayerPrefab;

    public List<GameObject> PlayerStats;
    public List<Image> MaskSlots;
    public List<Image> HealthSlots;

    public Content Content;

    private List<GameObject> Players;

    public void Start()
    {
        Players = new List<GameObject>();

        foreach(var playerStat in PlayerStats)
        {
            playerStat.SetActive(false);
        }

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
        PlayerStats[playerId].SetActive(true);
        MaskSlots[playerId].sprite = Content.GetMaskSprite(maskId);

        var player = (GameObject)Instantiate(PlayerPrefab, GetSpawnPoint(),Quaternion.identity);
        player.GetComponent<Player>().Init(playerId, maskId, HealthSlots[playerId]);
        Players.Add(player);
    }

    private Vector3 GetSpawnPoint()
    {
        return Vector3.zero;
    }
}
