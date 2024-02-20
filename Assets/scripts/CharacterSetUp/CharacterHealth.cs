using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private float hp = 100f;
    
    internal void TakeDamage(float amount)
    {
        hp -= amount;
    }
}
