using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] Button CloseBtn;

    [HideInInspector]
    public UI_Manager uI_manager;
    // Start is called before the first frame update
    void Start()
    {
        //uI_manager = GameObject.FindWithTag("UI_Manager").GetComponent<UI_Manager>();
        //if (uI_manager != null)
        //    Debug.Log("Found UI Manager");
        CloseBtn.onClick.AddListener(UnPauseGame);
    }


    void UnPauseGame()
    {
        if (uI_manager)
            uI_manager.ToggleGamePause();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
