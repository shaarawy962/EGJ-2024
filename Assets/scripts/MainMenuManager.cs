using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private int sceneIndex;

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
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
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
    }

    public void Play()
    {
        SceneManager.LoadScene(sceneIndex + 1);
    }

    public void OpenOptions()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
