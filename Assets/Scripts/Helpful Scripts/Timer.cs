using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public float timer;
    private float starttime;

    public Timer(float _time)
    {
        starttime = Time.time;
        timer = Time.time + _time;
    }

    public bool CheckTimer()
    {
        return Time.time >= timer;
    }

    public float Progress() //Progess in decimal
    {
        return Mathf.Min(1f-((timer - Time.time)/(timer - starttime)), 1f);
    }

}
