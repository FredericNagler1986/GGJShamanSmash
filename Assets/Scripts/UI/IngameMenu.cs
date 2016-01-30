using UnityEngine;
using System.Collections;

public class IngameMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ToggleMenu()
    {
        
    }

    public void BackToMainMenu()
    {
        LevelManager.Instance.LoadAndFadeToLevel("MainMenu");
    }
}
