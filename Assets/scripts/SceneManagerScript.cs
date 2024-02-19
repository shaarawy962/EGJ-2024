using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    private Scene currentScene;

    [SerializeField] private string[] SceneNames;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OpenScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void OpenNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Current scene name: " + currentScene.name);
    }
}
