using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static GameObject selectedCharacter;
    private static CharacterMovement _movement;
    private void Start()
    {
        selectedCharacter = null;
    }
    internal static void SelectCharacter(GameObject character)
    {
        selectedCharacter = character;
        selectedCharacter.TryGetComponent(out CharacterMovement movement);
        if(movement is null)
            return;
        _movement = movement;
    }
    internal static void ChangeTarget(Vector3 target)
    {
        if(selectedCharacter is null)
            return;
        _movement.Target = target;

    }
}
