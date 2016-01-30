using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Main : SingletonMonoBehaviour<Main>
{
    public EGameState GameState = EGameState.None;
    public GameObject ResultScreen = null;
    [SerializeField]
    public GameData GameData = null;

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
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
