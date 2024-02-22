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
    private List<GameObject> HorizontalBoxCharacterEntries;


    public UI_Delegates.CharacterTypesChanged onCharacterTypesChanged;
    public UI_Delegates.WaveTimerEnded onWaveTimerEnded;
    public UI_Delegates.WaveIndexChanged onWaveIndexChanged;
    public UI_Delegates.WaveTimerStarted onWaveTimerStarted;

    public bool isTimerOn;
    public float WaveTimeRemaining;
    public int WaveIndex;


    private int CharacterTypes;
    
    public void SetCharacterTypes(int Count)
    {
        CharacterTypes = Count;
        RefreshEntries();
    }

    public void SetWaveTimer(float TimeRemaining)
    {
        WaveTimeRemaining = TimeRemaining;
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

    void UpdateWaveIndex(int newIndex)
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
        }
        DisplayTimerText(WaveTimeRemaining);
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnEntries();

        Invoke("DebugMethod", 10);
        ToggleMainUi(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerOn)
        {
            UpdateTimer(Time.deltaTime);
        }
    }


    public void FinishWave()
    {

    }
    
    void DisplayTimerText(float TimeRemaining)
    {
        TimeRemaining += 1;

        float minutes = Mathf.FloorToInt(TimeRemaining / 60);
        float seconds = Mathf.FloorToInt(TimeRemaining % 60);

        WaveTimerText.text = "Time Remaining: " + string.Format("{0:00}:{1:00}", minutes, seconds);

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
