using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractScript : MonoBehaviour
{
    bool isInZone = false;

    private void Update()
    {
        if(isInZone && Input.GetKeyDown(KeyCode.Space))
        {
            ConvertMates();
        }
    }

    private void ConvertMates()
    {
        GameMode.instance.brain.GetComponent<Life>().Heal((int)(2 * Mate.mateCount));
        Score.instance.AddRepairPoints(40 * Mate.mateCount);
        //MoodSystem.instance.DecreaseMood(3.5f * Mate.mateCount);
        
        //Debug.Log("Mates Converted!");
        MateArranger.RemoveAllMates();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (space) Debug.Log("WATER");
        if (collision.tag == "Player")
        {
            isInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInZone = false;
        }
    }

}
