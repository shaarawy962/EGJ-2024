using System;
using UnityEngine;

public class ShadyTimer
{
    public Action Event;
    public float Delay;
    public bool Loop;
    public bool IsFired = false;

    private float TimeRemaining;

    public ShadyTimer(float delay, bool loop )
    {
        Delay = delay;
        Loop = loop;
        TimeRemaining = Delay;
        IsFired = false;
    }

    //should be called in a fixed update loop
    public void UpdateTimer(float deltatime)
    {
        if (IsFired)
            return;

        TimeRemaining -= deltatime;
        if (TimeRemaining <= 0)
        {
            Event.Invoke();
            if (Loop)
                TimeRemaining = Delay;
            else
            {
                TimeRemaining = 0;
                IsFired = true;
            }
        }
    }

}
