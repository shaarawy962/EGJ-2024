using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterEntrySetup : MonoBehaviour
{
    private Button UpgradeButton;


    public UI_Manager manager { get; set; }


    private UI_Delegates.ChangeCanUpgradeCharacter onCanUpgradeCharacterChanges;

    private void SetButtonState(bool bValue)
    {
        if (!UpgradeButton) return;
        UpgradeButton.interactable = bValue;
    }

    private void Awake()
    {
        UpgradeButton = GetComponentInChildren<Button>();
        SetButtonState(false);
        onCanUpgradeCharacterChanges = SetButtonState;
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DebugMethod", 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void DebugMethod()
    {
        onCanUpgradeCharacterChanges?.Invoke(true);
    }

}
