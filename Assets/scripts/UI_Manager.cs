using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


#region delegates
namespace UI_Delegates
{
    public delegate void CharacterTypesChanged(int newTypes);
    public delegate void ChangeCanUpgradeCharacter(bool bCanUpgrade);
    public delegate void WaveTimerEnded();
}
#endregion


public class UI_Manager : MonoBehaviour
{
    [SerializeField] private TMP_Text WaveTimerText;
    [SerializeField] private TMP_Text WaveCountText;
    [SerializeField] private HorizontalLayoutGroup HorizontalBox;
    [SerializeField] private GameObject CharacterEntry;
    [SerializeField] private int DummyCharacterCount;
    private List<GameObject> HorizontalBoxCharacterEntries;


    public UI_Delegates.CharacterTypesChanged onCharacterTypesChanged;
    public UI_Delegates.WaveTimerEnded onWaveTimerEnded;

    public bool isTimerOn;
    public float WaveTimeRemaining;
    public int WaveIndex;


    private int CharacterTypes;

    public void SetCharacterTypes(int Count)
    {
        CharacterTypes = Count;
        RefreshEntries();
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

    void UpdateWaveIndex()
    {
        WaveCountText.text = "WAVE " + WaveIndex;
    }


    private void Awake()
    {
        CharacterTypes = DummyCharacterCount;
        onCharacterTypesChanged = SetCharacterTypes;
        HorizontalBoxCharacterEntries = new List<GameObject>();
    }

    void DisplayTimerText()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnEntries();

        Invoke("DebugMethod", 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerOn)
        {

        }
    }


    public void FinishWave()
    {

    }


    private void DebugMethod()
    {
        SetCharacterTypes(4);
    }

}
