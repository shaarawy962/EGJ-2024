using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GetMouseInput : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private LayerMask characterMask;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private CharacterManager manager;
    private void Start()
    {
        _camera = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = _camera.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast((Vector2) ray, Vector3.forward,100 , characterMask);
           if (hit.collider is null)
               return;
            Debug.Log(hit.collider.name);
            manager.SelectCharacter(hit.collider.gameObject);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            var ray = _camera.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast((Vector2) ray, Vector3.forward,100 , groundMask);
            if (hit.collider is null)
                return;
            var target = _camera.ScreenToWorldPoint(Input.mousePosition);
            target.z = hit.collider.transform.position.z;
            manager.ChangeTarget(target , 0.01f);
        }
    }
    
}
