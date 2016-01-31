using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelectionObject : MonoBehaviour {

    public Text ButtonText = null;
    private LevelSelection mLevelSelectionController = null;
    public string LevelScene = "";

    // Use this for initialization
    void Start () {
        mLevelSelectionController = LevelSelection.Instance;
        if (LevelScene == "level1")
        {
            SelectLevelObject();
        }
	}

   
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClickedLevelObject()
    {
        if (ButtonText != null)
        {
            this.SelectLevelObject();
        }
    }

    public void SelectLevelObject()
    {
        mLevelSelectionController.DeselectAll();
        mLevelSelectionController.SelectedLevel = LevelScene;
        if (ButtonText != null)
        {
            ButtonText.fontStyle = FontStyle.Bold;

        }
    }

    public void DeselectLevelObject()
    {
        if (ButtonText != null)
        {
            ButtonText.fontStyle = FontStyle.Normal;
        }
    }
}
