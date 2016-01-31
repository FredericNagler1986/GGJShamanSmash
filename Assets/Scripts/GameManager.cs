using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public GameObject PlayerPrefab;

    public List<GameObject> PlayerStats;
    public List<Image> MaskSlots;
    public List<Image> HealthSlots;
    public List<Image> ColorImage;

    public List<Image> Slots1;
    public List<Image> Slots2;
    public List<Image> Slots3;

    public Content Content;

    public GameObject UIGameObject;

    private List<Player> Players;

    private bool isFinished = false;

    public void Start()
    {
        Players = new List<Player>();

        foreach(var playerStat in PlayerStats)
        {
            playerStat.SetActive(false);
        }

        if (Main.Instance == null)
        {
            AddPlayer(0, 0);
            AddPlayer(1, 1);
            AddPlayer(2, 2);
            AddPlayer(3, 3);
        }
        else
        {
            foreach (var player in Main.Instance.GameData.PlayerData)
            {
                AddPlayer(player.PlayerID, player.MaskID);
            }
        }
    }

    private IEnumerator Finish()
    {
        var player = Players[0];

        if (Players.Count == 1)
        {
            player.Win();
        }

        UIGameObject.SetActive(false);

        yield return new WaitForSeconds(2);

        Main.Instance.ResultData = new ResultData(player.PlayerId, player.MaskId);

        Main.Instance.MoveToResultScreen();
    }

    private void Update()
    {
        if (isFinished)
            return;

        if(Players.Count <= 1 && Main.Instance != null)
        {
            StartCoroutine(Finish());
            isFinished = true;
            return;
        }

        for(var i=0; i<Players.Count; i++)
        {
            var player = Players[i];
            if(player.HP <= 0)
            {
                Players.Remove(player);
                StartCoroutine(RemovePlayer(player));
                return;
            }
        }
    }

    private IEnumerator RemovePlayer(Player player)
    {    
        player.Die();

        Time.timeScale = 0.25f;

        yield return new WaitForSeconds(0.5f);
        
        Time.timeScale = 1.0f;
    }

    public void ScreenShake(float force)
    {
        force = Mathf.Min(force, 0.4f);

        iTween.ShakePosition(Camera.main.gameObject, iTween.Hash("x", force, "y", force, "time", 1.0f));
    }

    private void AddPlayer(int playerId, int maskId)
    {
        PlayerStats[playerId].SetActive(true);
        MaskSlots[playerId].sprite = Content.GetMaskSprite(maskId);
        ColorImage[playerId].color = Content.GetPlayerColor(playerId);

        var instance = (GameObject)Instantiate(PlayerPrefab, GetSpawnPoint(),Quaternion.identity);
        var player = instance.GetComponent<Player>();
        player.Init(playerId, maskId, HealthSlots[playerId],Slots1[playerId], Slots2[playerId], Slots3[playerId]);
        Players.Add(player);
    }

    private Vector3 GetSpawnPoint()
    {
        return Vector3.zero;
    }
}
