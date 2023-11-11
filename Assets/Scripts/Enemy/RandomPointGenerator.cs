using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomPointGenerator : MonoBehaviour
{
    public static Vector3 RandomPointGenerate(Vector3 startPoint, float radius)
    {
        Vector3 dir = Random.insideUnitSphere * radius;
        dir += startPoint;
        NavMeshHit hit_;
        Vector3 finalPosition = Vector3.zero;
        if(NavMesh.SamplePosition(dir,out hit_, radius, 1))
        {
            finalPosition = hit_.position;
        }
        return finalPosition;

    }

}
