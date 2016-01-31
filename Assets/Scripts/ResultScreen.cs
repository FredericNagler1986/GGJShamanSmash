using UnityEngine;
using System.Collections;

public class ResultScreen : MonoBehaviour {

    void OnEnable()
    {
        Main.Instance.GameState = EGameState.Result;
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
