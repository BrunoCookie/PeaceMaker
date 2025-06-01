using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MoodSetting")]
public class MoodSetting : ScriptableObject
{
    public float length;
    public Gradient barGradient;
    public Sprite barImage;

    public enum moods { happy, sad, angry, nausea, mentalbreakdown};
    public moods[] moodArray; //all used Moods in current Setting
    public float[] moodEnd; //matching endTime of the moods in same order

    public void AdjustGradient()
    {
        barGradient.mode = GradientMode.Fixed;
        GradientColorKey[] colorKey = new GradientColorKey[moodArray.Length];

        for(int i = 0; i<moodArray.Length; i++)
        {
            float relativeTime = moodEnd[i] / length;
            Color moodColor = Color.white;
            if (moodArray[i] == moods.happy) moodColor = Color.green;
            else if (moodArray[i] == moods.sad) moodColor = Color.blue;
            else if (moodArray[i] == moods.angry) moodColor = Color.red;
            else if (moodArray[i] == moods.nausea) moodColor = Color.yellow;
            else if (moodArray[i] == moods.mentalbreakdown) moodColor = Color.magenta;

            colorKey[i].color = moodColor;
            colorKey[i].time = relativeTime;
        }
        
        barGradient.colorKeys = colorKey;
    }

    public moods getMood(float time)
    {
        int i = 0;
        while (time > moodEnd[i]) i++;
        return moodArray[i];
    }
}
