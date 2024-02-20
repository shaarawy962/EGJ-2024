using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_Manager : MonoBehaviour
{
    public Waves_Scriptable_Object[] Waves;
    public Waves_Scriptable_Object CurrentWave;
    //should start at -1
    public int CurrentWaveIndex = -1;
    public Wave_Interval CurrentWaveInterval;
    //should start at -1
    public int CurrentWaveIntervalIndex = -1;
    [Tooltip("Small dalay a fraction of a seconed to prevent glitches and spawning on top of each other.")]
    public float SpawnDelay;
    public Transform[] SpawnPoints;
    [Tooltip("Min & max range for the wave delay.")]
    public Vector2 WaveDelay;

    public float WaveTimer = 0;
    public float IntervalTimer = 0;

    //responsible for wave and interval timers (can be disabled)
    //may change later
    void Update()
    {
        IntervalTimer -= Time.deltaTime;
        WaveTimer -= Time.deltaTime;

        if (WaveTimer < 0)
        {
            WaveTimer = UnityEngine.Random.Range(WaveDelay.x, WaveDelay.y);
            StartNextWave();
        }

        if(IntervalTimer < 0 && CurrentWave != null)
        {
            IntervalTimer = UnityEngine.Random.Range(CurrentWave.IntervalDelay.x, CurrentWave.IntervalDelay.y);
            StartNextInterval();
        }
    }

    /// <summary>
    /// Starts the next wave and the first interval in the next wave/
    /// </summary>
    /// <returns>Returns true if next wave exists false if this was the last wave.</returns>
    public bool StartNextWave()
    {
        if (CurrentWaveIndex >= Waves.Length - 1)
            return false;

        //should clear current npc's
        CurrentWaveIndex++;
        CurrentWave = Waves[CurrentWaveIndex];
        CurrentWaveIntervalIndex = -1;
        StartNextInterval();
        return true;
    }

    /// <summary>
    /// Starts the next interval and spawns the enemies.
    /// </summary>
    /// <returns>Returns true if the next interval in the wave exists and false if this was the last interval/</returns>
    public bool StartNextInterval()
    {
        if (CurrentWave is null || CurrentWave.Intervals is null || CurrentWaveIntervalIndex >= CurrentWave.Intervals.Length - 1)
            return false;

        CurrentWaveIntervalIndex++;
        CurrentWaveInterval = (Wave_Interval)CurrentWave.Intervals[CurrentWaveIntervalIndex].Clone();
        StartCoroutine(SpawnEnemies());
        return true;
    }

    public IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < CurrentWaveInterval.Enemies.Length; i++)
        {
            while (CurrentWaveInterval.Enemies[i].count > 0)
            {
                //select a random spawn point
                Transform SpawnPoint = SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Length - 1)];
                Instantiate(CurrentWaveInterval.Enemies[i].enemy , SpawnPoint.position , SpawnPoint.rotation);
                CurrentWaveInterval.Enemies[i].count--;

                float SpawnTimer = 0;
                while (SpawnTimer < SpawnDelay)
                {
                    SpawnTimer += Time.deltaTime;
                    yield return null;
                }
            }
        }
    }

}
