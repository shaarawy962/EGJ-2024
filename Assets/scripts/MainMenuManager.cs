using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private Scene currentScene;

    [SerializeField] private string MainGameSceneName;

    private Button[] ListButtons;
    private Button PlayBtn;
    private Button OptionsBtn;
    private Button QuitBtn;


    private void Awake()
    {
        PlayBtn = GameObject.FindWithTag("PlayBtn").GetComponent<Button>();
        OptionsBtn = GameObject.FindWithTag("OptionsBtn").GetComponent<Button>();
        QuitBtn = GameObject.FindWithTag("QuitBtn").GetComponent<Button>();

        ListButtons = new[] {PlayBtn, OptionsBtn, QuitBtn };
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayBtn.onClick.AddListener(Play);
        OptionsBtn.onClick.AddListener(OpenOptions);
        QuitBtn.onClick.AddListener(Quit);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ListButtons.Length; i++)
        {
            Debug.Log(ListButtons[i].name);
        }
    }

    public void Play()
    {

    }

    public void OpenOptions()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
