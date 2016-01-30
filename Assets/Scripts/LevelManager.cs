using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : SingletonMonoBehaviour<LevelManager> {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void LoadAndFadeToLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
}
