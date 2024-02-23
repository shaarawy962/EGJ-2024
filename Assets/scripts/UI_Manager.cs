using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Playables;


#region delegates
namespace UI_Delegates
{
    public delegate void CharacterTypesChanged(int newTypes);
    public delegate void ChangeCanUpgradeCharacter(bool bCanUpgrade);
    public delegate void WaveTimerEnded();
    public delegate void WaveTimerStarted(float TimeRemaining);
    public delegate void WaveIndexChanged(int newIndex);
}
#endregion


public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject mainUi;
    [SerializeField] private TMP_Text WaveTimerText;
    [SerializeField] private TMP_Text WaveCountText;
    [SerializeField] private HorizontalLayoutGroup HorizontalBox;
    [SerializeField] private GameObject CharacterEntry;
    [SerializeField] private int DummyCharacterCount;
    [SerializeField] private PlayableDirector WaveTimeline;
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject LostScreen;
    [SerializeField] private Button SettingsBtn;
    private List<GameObject> HorizontalBoxCharacterEntries;
    private GameObject spawnedPauseMenu;

    private bool bIsGamePaused = false;

    public UI_Delegates.CharacterTypesChanged onCharacterTypesChanged;
    public UI_Delegates.WaveTimerEnded onWaveTimerEnded;
    public UI_Delegates.WaveIndexChanged onWaveIndexChanged;
    public UI_Delegates.WaveTimerStarted onWaveTimerStarted;

    private bool isTimerOn;
    private float WaveTimeRemaining;
    private int WaveIndex;


    private int CharacterTypes;
    
    public void SetCharacterTypes(int Count)
    {
        CharacterTypes = Count;
        RefreshEntries();
    }

    public void SetWaveTimer(float TimeRemaining)
    {
        WaveTimeRemaining = TimeRemaining;
        isTimerOn = true;
    }

    public void ToggleGamePause()
    {
        if (bIsGamePaused)
        {
            bIsGamePaused = false;
            Destroy(spawnedPauseMenu);
            spawnedPauseMenu = null;
            Time.timeScale = 1.0f;
        }
        else
        {
            bIsGamePaused = true;
            if (mainUi)
            {
                spawnedPauseMenu = Instantiate(PauseScreen);
                if (spawnedPauseMenu.GetComponent<PauseMenu>() != null )
                    spawnedPauseMenu.GetComponent<PauseMenu>().uI_manager = this;
            }
            Time.timeScale = 0.0f;
        }
    }


    public void OnLostGame()
    {
        if (LostScreen)
            Instantiate(LostScreen);
        Time.timeScale = 0.0f;
    }

    private void RefreshEntries()
    {
        for (int i = 0; i < HorizontalBoxCharacterEntries.Count; i++)
        {
            Destroy(HorizontalBoxCharacterEntries[i]);
        }
        HorizontalBoxCharacterEntries.Clear();
        SpawnEntries();
    }

    void SpawnEntries()
    {
        for (int i = 0; i < CharacterTypes; i++)
        {
            if (HorizontalBox)
            {
                GameObject CharEntryInst = Instantiate(CharacterEntry, HorizontalBox.transform);
                HorizontalBoxCharacterEntries.Add(CharEntryInst);
                CharacterEntrySetup SetupComponent = CharEntryInst.GetComponent<CharacterEntrySetup>();
                if (SetupComponent) {
                    SetupComponent.manager = this;
                }
                //CharEntryInst.transform.SetParent(HorizontalBox.gameObject.transform);
            }
        }
    }

    public void UpdateWaveIndex(int newIndex)
    {
        WaveCountText.text = "WAVE " + WaveIndex;
        if (WaveTimeline)
            WaveTimeline.Play();
    }


    private void Awake()
    {
        CharacterTypes = DummyCharacterCount;
        onCharacterTypesChanged = SetCharacterTypes;
        onWaveIndexChanged = UpdateWaveIndex;
        onWaveTimerStarted = SetWaveTimer;
        HorizontalBoxCharacterEntries = new List<GameObject>();
        WaveCountText.text = "";
        SettingsBtn.onClick.AddListener(ToggleGamePause);
    }

    void UpdateTimer(float DeltaTime)
    {
        if (WaveTimeRemaining > 0)
        {
            WaveTimeRemaining -= DeltaTime;
        }
        else
        {
            Debug.Log("TIME is UP!");
            isTimerOn = false;
            HideTimerText();
        }
        DisplayTimerText(WaveTimeRemaining);
    }

    // Start is called before the first frame update
    void Start()
    {
        //SpawnEntries();
        spawnedPauseMenu = null;

        //Invoke("DebugMethod", 10);
        //ToggleMainUi(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerOn)
        {
            UpdateTimer(Time.deltaTime);
        }
        else
        {
            HideTimerText();
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ToggleGamePause();
        }
    }

    void DisplayTimerText(float TimeRemaining)
    {
        TimeRemaining += 1;

        float minutes = Mathf.FloorToInt(TimeRemaining / 60);
        float seconds = Mathf.FloorToInt(TimeRemaining % 60);

        WaveTimerText.text = "Time Remaining: " + string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    private void HideTimerText()
    {
        WaveTimerText.text = "";
    }

    private void DebugMethod()
    {
        SetCharacterTypes(4);
        onWaveIndexChanged?.Invoke(4);
        onWaveTimerStarted?.Invoke(305);
    }
    public void ToggleMainUi(bool isOn)
    {
        this.mainUi.SetActive(isOn);
    }

}
