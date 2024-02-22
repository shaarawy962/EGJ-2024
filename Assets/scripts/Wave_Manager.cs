using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wave_Manager : MonoBehaviour
{
    public Waves_Scriptable_Object[] Waves;
    public Waves_Scriptable_Object CurrentWave;
    //should start at -1
    public int CurrentWaveIndex = -1;
    public Wave_Interval NextWaveInterval;
    //should start at -1
    public int NextWaveIntervalIndex = -1;
    [Tooltip("Small dalay a fraction of a seconed to prevent glitches and spawning on top of each other.")]
    public float SpawnDelay;
    public Transform[] SpawnPoints;

    public ShadyTimer WaveTimer;
    public ShadyTimer IntervalTimer;

    public UnityEvent WaveEnded;
    public UnityEvent WaveStarted;
    public UnityEvent IntervalStarted;

    List<Enemy> SpawnedOnes = new List<Enemy>(10);

    //responsible for wave and interval timers (can be disabled)
    //may change later
    void Update()
    {
        WaveTimer.UpdateTimer(Time.deltaTime);
        IntervalTimer.UpdateTimer(Time.deltaTime);

        if (SpawnedOnes.Count == 0)
        {
            WaveEnded.Invoke();
            if (LoadNextWave())
            {
                WaveTimer = new ShadyTimer(CurrentWave.WaveDelay, false);
                WaveTimer.Event += StartWave;
            }
        }

    }

    /// <summary>
    /// Loads the next wave and set the interval index.
    /// </summary>
    /// <returns>Returns true if next wave exists false if this was the last wave.</returns>
    public bool LoadNextWave()
    {
        if (CurrentWaveIndex >= Waves.Length - 1)
            return false;

        //should clear current npc's
        CurrentWaveIndex++;
        CurrentWave = Waves[CurrentWaveIndex];
        NextWaveIntervalIndex = -1;
        return true;
    }

    public void StartWave()
    {
        LoadNextInterval();
        WaveStarted.Invoke();
        StartInterval();
    }

    /// <summary>
    /// Starts the next interval and spawns the enemies.
    /// </summary>
    /// <returns>Returns true if the next interval in the wave exists and false if this was the last interval/</returns>
    public bool LoadNextInterval()
    {
        if (CurrentWave is null || CurrentWave.Intervals is null || NextWaveIntervalIndex >= CurrentWave.Intervals.Length - 1)
            return false;

        NextWaveIntervalIndex++;
        NextWaveInterval = (Wave_Interval)CurrentWave.Intervals[NextWaveIntervalIndex].Clone();
        return true;
    }

    /// <summary>
    /// Starts spawning enemies , loads next interval and starts a timer that will start the next interval
    /// </summary>
    public void StartInterval()
    {
        StartCoroutine(SpawnEnemies());
        if (LoadNextInterval())
            IntervalTimer = new ShadyTimer(NextWaveInterval.IntervalDelay, false);
        IntervalStarted.Invoke();
    }

    public void EndWave()
    {
        for (int i = 0; i < SpawnedOnes.Count; i++)
        {
            SpawnedOnes[i].Flee();
        }
    }

    //better implement a pool later
    public IEnumerator SpawnEnemies()
    {
        Wave_Interval CurrentWaveInterval = NextWaveInterval;
        for (int i = 0; i < CurrentWaveInterval.Enemies.Length; i++)
        {
            while (CurrentWaveInterval.Enemies[i].count > 0)
            {
                //select a random spawn point
                Transform SpawnPoint = SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Length - 1)];
                var a = Instantiate(CurrentWaveInterval.Enemies[i].enemy, SpawnPoint.position, SpawnPoint.rotation);
                SpawnedOnes.Add(a);
                a.GotDefeated += EnemyDefeated;
                CurrentWaveInterval.Enemies[i].count--;
                yield return new WaitForSeconds(SpawnDelay);
            }
        }
    }

    public void EnemyDefeated(Enemy args)
    {
        args.GotDefeated -= EnemyDefeated;
        SpawnedOnes.Remove(args);
    }

}
