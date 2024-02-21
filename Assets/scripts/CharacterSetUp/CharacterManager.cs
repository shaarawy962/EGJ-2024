using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterManager : MonoBehaviour
{
    private GameObject selectedCharacter;
    private CharacterSetUp _characterSetUp;
    private void Start()
    {
        selectedCharacter = null;
    }
    internal void SelectCharacter(GameObject character)
    {
        if (character.CompareTag("Enemy"))
        {
            if(selectedCharacter is null)
                return;
            var offset = _characterSetUp.attackRange;
            var target = character.transform.position;
            ChangeTarget(target , offset);
            return;
        }
        selectedCharacter = character;
        selectedCharacter.TryGetComponent(out CharacterSetUp movement);
        if(movement is null)
            return;
        _characterSetUp = movement;
    }
    internal void ChangeTarget(Vector3 target , float offset)
    {
        if(selectedCharacter is null)
            return;
        _characterSetUp.target = target;
        _characterSetUp.offset = offset;
    }
}
