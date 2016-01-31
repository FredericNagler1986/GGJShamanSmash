using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour {
    ResultData mResultData = null;
    public Image Mask = null;
    public Image PlayerSprite;

    void OnEnable()
    {
        Main.Instance.GameState = EGameState.Result;
        mResultData = Main.Instance.ResultData;
        if (mResultData == null)
        {
            mResultData = new ResultData(1, 7);
        }
        Mask.sprite = Main.Instance.GameContent.Masks[mResultData.PlayerMask];
        PlayerSprite.color = Main.Instance.GameContent.PlayerColors[mResultData.PlayerWon];
    }

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BackToMainMenue()
    {
        Time.timeScale = 1.0f;
        LevelManager.Instance.LoadAndFadeToLevel("MainMenu");
    }
}
