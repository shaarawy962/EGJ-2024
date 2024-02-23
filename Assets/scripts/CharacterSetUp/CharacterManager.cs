using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterManager : MonoBehaviour
{
    private PlayableCharacter selectedCharacter;

    [SerializeField]
    private CatCharacter catCharacter;

    [SerializeField]
    private HoundCharacter houndCharacter;


    public Action<int> onCharacterSelected;

    [SerializeField] private LayerMask characterMask;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private LayerMask EnemyMask;


    private Camera _camera;

    private void Start()
    {
        selectedCharacter = null;
       
         _camera = Camera.main;
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = _camera.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast((Vector2)ray, Vector3.forward, 100, characterMask);
            if (hit.collider is null)
                return;
            Debug.Log(hit.collider.name);
            
            SelectCharacter(hit.collider.gameObject);
            
        }
        else if (Input.GetMouseButtonDown(1))
        {
            var ray = _camera.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast((Vector2)ray, Vector3.forward, 100, groundMask);
            if (hit.collider is null)
                return;
            var target = _camera.ScreenToWorldPoint(Input.mousePosition);
            target.z = hit.collider.transform.position.z;
            selectedCharacter.ChangeTarget(target, 0.01f);
        }
    }

    internal void SelectCharacter(GameObject character)
    {
        if (character.CompareTag("Enemy"))
        {
            if(selectedCharacter is null)
                return;
            var offset = selectedCharacter.attackRange/2;
            var target = character.transform.position;
            selectedCharacter.enemyTarget = character.transform;
            selectedCharacter.ChangeTarget(target , offset);
            return;
        }

        character.TryGetComponent(out PlayableCharacter charaterComp);
        if (charaterComp is null)
            return;
        selectedCharacter = charaterComp;
        onCharacterSelected?.Invoke(1);
    }
    
}
