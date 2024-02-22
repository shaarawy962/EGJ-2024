using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameModes
{
    Story,
    InitialDialogue,
    Gameplay,
    Lost,
    Won,
    Paused
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    UI_Manager uI_Manager;

    [SerializeField]
    Wave_Manager wave_Manager;

    [SerializeField]
    DialogueManager DialogueManager;

    [SerializeField]
    CharacterManager characterManager;

    GameModes CurrentGameMode = GameModes.Story;

    // Start is called before the first frame update
    void Start()
    {
        DialogueManager.DialogueEnded += OnDialogueEnded;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDialogueEnded()
    {
        if (CurrentGameMode == GameModes.Story)
        {
            wave_Manager.Is_Waves_Runing = true;
            uI_Manager.ToggleMainUi(true);
            CurrentGameMode = GameModes.Gameplay;
        }
    }
}
