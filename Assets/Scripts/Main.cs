using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class Main : SingletonMonoBehaviour<Main>
{
    public EGameState GameState = EGameState.None;
    public GameObject ResultScreen = null;
    [SerializeField]
    public GameData GameData = null;
    public GameObject IngameMenu = null;

    public Content GameContent;
    public ResultData ResultData;

    void Awake()
    {
        GameObject.DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        GameState = EGameState.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            if (ResultScreen != null)
            {
                if (Main.Instance.GameState == EGameState.Ingame)
                {
                    ResultScreen.SetActive(true);
                    Main.Instance.GameState = EGameState.Result;
                }
                
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(IngameMenu != null && GameState == EGameState.Ingame)
            {
                ToggleIngameMenu();

            }
        }
    }

    public void ToggleIngameMenu()
    {
        IngameMenu.SetActive(!IngameMenu.activeSelf);
        Time.timeScale = IngameMenu.activeSelf ? 0.0f : 1.0f;
    }

    public void MoveToResultScreen()
    {
        ResultScreen.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
