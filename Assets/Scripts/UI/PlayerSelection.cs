using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerSelection : SingletonMonoBehaviour<PlayerSelection>
{
    public Button ToLevelSelectionButton = null;
    private List<PlayerSelectionObject> mPlayerSelectionObjects = new List<PlayerSelectionObject>();


    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        Main.Instance.GameState = EGameState.CharacterSelection;
        for (int i = 0; i < mPlayerSelectionObjects.Count; i++)
        {
            mPlayerSelectionObjects[i].Status = EPlayerSelectionStatus.Inactive;
        }
    }

    void OnDisable()
    {
        GameData data = new GameData();
        for (int i = 0; i < mPlayerSelectionObjects.Count; i++)
        {
            if (mPlayerSelectionObjects[i].Status == EPlayerSelectionStatus.Ready)
            {
                data.AddPlayerPlayerData(mPlayerSelectionObjects[i].playerID, mPlayerSelectionObjects[i].MaskID);
            }
        }
        Main.Instance.GameData = data;
        
    }

    public void UpdateButton()
    {
        bool active = true;
        int counter = 0;
        for (int i = 0; i < mPlayerSelectionObjects.Count; i++)
        {
            if (mPlayerSelectionObjects[i].Status == EPlayerSelectionStatus.SelectingMask )
            {
                active = false;
            }
            if (mPlayerSelectionObjects[i].Status == EPlayerSelectionStatus.Ready)
            {
                counter++;
            }
        }
        active = counter > 1;
        if (ToLevelSelectionButton != null)
        {
            ToLevelSelectionButton.interactable = active;
        }
    }

    public void RegisterPlayerControl(PlayerSelectionObject plObj)
    {
        if (!mPlayerSelectionObjects.Contains(plObj))
        {
            mPlayerSelectionObjects.Add(plObj);
        }
    }
}
