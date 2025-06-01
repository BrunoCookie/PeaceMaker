using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MateArranger : MonoBehaviour
{
    private List<Transform> spots = new List<Transform>();

    private void Start()
    {
        for (int i = 0; i<transform.childCount; i++)
        {
            spots.Add(transform.GetChild(i));
        }
        InvokeRepeating("Arrange", 0, 0.5f);
    }

    private void Arrange()
    {
        List<Transform> toArrange = new List<Transform>();
        for(int i = 0; i<spots.Count; i++) //Check every Spot
        {
            if (spots[i].childCount > 1) //Check every Spots Children
            {
                for(int k = 1; k<spots[i].childCount; k++) //If too much children -> Add to toArrange List
                {
                    toArrange.Add(spots[i].GetChild(k));
                }
            }
        }
        //Debug.LogWarning(toArrange.Count + " Mates are falsely arranged!");

        if (toArrange.Count < 1) return; //If there are no fucked up Mates -> you're done! :)

        //Arrange the fucked up Mates
        foreach(Transform currentSpot in spots)
        {
            if (toArrange.Count < 1) return;
            if (currentSpot.childCount < 1)
            {
                toArrange[0].GetComponent<Mate>().ChangeTarget(currentSpot);
                toArrange.RemoveAt(0);
            }
        }
    }

    public static void RemoveAllMates()
    {
        GameObject[] mates = GameObject.FindGameObjectsWithTag("Mate");
        foreach(GameObject mate in mates)
        {
            Destroy(mate);
        }
    }
}
