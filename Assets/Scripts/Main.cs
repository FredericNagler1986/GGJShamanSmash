using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Main : SingletonMonoBehaviour<Main> {

    void Awake()
    {
        GameObject.DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ExitGame()
    {
        Application.Quit();
    }
}
