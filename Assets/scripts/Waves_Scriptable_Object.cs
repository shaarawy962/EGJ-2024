using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Scriptable Objects/Wave Scriptable Object", order = 1)]
public class Waves_Scriptable_Object : ScriptableObject
{
    [Tooltip("Min & max range for the interval delay.")]
    public Vector2 IntervalDelay;
    public Wave_Interval[] Intervals;
}

[Serializable]
public struct Wave_Interval : ICloneable
{
    public EnemyCount[] Enemies;

    public object Clone()
    {
        EnemyCount[] enemies = new EnemyCount[Enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = Enemies[i];
        }
        Wave_Interval clone = new Wave_Interval() { Enemies = enemies };
        return clone;
    }
}

[Serializable]
public struct EnemyCount
{
    public Enemy enemy;
    public int count;
}