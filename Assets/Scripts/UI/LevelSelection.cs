using UnityEngine;
using System.Collections;

public class LevelSelection : SingletonMonoBehaviour<LevelSelection>
{

    [SerializeField]
    public string SelectedLevel = "";

   

    public void DeselectAll()
    {
        LevelSelectionObject[] levelObjects = GameObject.FindObjectsOfType<LevelSelectionObject>();
        for (int i = 0; i < levelObjects.Length; i++)
        {
            levelObjects[i].DeselectLevelObject();
        }
    }

    void OnEnable()
    {
        Main.Instance.GameState = EGameState.StageSelection;
    }

    public void LoadLevel()
    {
        if (SelectedLevel != "")
        {
            LevelManager.Instance.LoadAndFadeToLevel(SelectedLevel);
            gameObject.SetActive(false);
            Main.Instance.GameState = EGameState.Ingame;
        }
    }

}
