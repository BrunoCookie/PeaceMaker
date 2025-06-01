using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class FollowRail : MonoBehaviour
{
    public Rail rail;
    public PlayMode mode;
    public float speed;
    public bool loop;
    [HideInInspector] public bool isCompleted;

    private int currentSeg;
    private float transition;

    public UnityEvent onFollowComplete;
    public UnityEvent onLoopComplete;

    private void Update()
    {
        if (!rail) return;

        if (!isCompleted) Play();
    }

    private void Play()
    {
        float m = (rail.nodes[currentSeg + 1].position - rail.nodes[currentSeg].position).magnitude;
        float s = (Time.deltaTime * 1 / m) * speed;
        transition += s;
        if(transition > 1)
        {
            transition = 0;
            currentSeg++;
            if(currentSeg == rail.nodes.Length - 1)
            {
                if (loop)
                {
                    currentSeg = 0;
                    onLoopComplete.Invoke();
                }
                else
                {
                    isCompleted = true;
                    onFollowComplete.Invoke();
                    return;
                }
            }
        }

        transform.position = rail.PositionOnRail(currentSeg, transition, mode);
    }

    public void ResetFollowing()
    {
        currentSeg = 0;
        isCompleted = false;
    }
}
