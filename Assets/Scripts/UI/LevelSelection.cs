using UnityEngine;
using System.Collections;

public class LevelSelection : MonoBehaviour
{

    private static LevelSelection sInstance = null;
    public static LevelSelection Instance
    {
        get
        {
            return sInstance;
        }
    }

    [SerializeField]
    public string SelectedLevel = "";

    void Awake()
    {
        sInstance = this;
    }

    public void DeselectAll()
    {
        LevelSelectionObject[] levelObjects = GameObject.FindObjectsOfType<LevelSelectionObject>();
        for (int i = 0; i < levelObjects.Length; i++)
        {
            levelObjects[i].DeselectLevelObject();
        }
    }

}
