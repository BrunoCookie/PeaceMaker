using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DrawCircleGizmo : MonoBehaviour
{
    public float radius;
    public Vector3 offset;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + offset, radius);
    }
}
