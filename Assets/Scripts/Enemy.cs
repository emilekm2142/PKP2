using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public float speed = 0.15f;
    public float StepTowardsTrain(Train train)
    {
        Vector3 diff = train.gameObject.transform.position - gameObject.transform.position;
        float norm = Mathf.Sqrt(diff[0] * diff[0] + diff[1] * diff[1] + diff[2] * diff[2]);
        gameObject.transform.Translate(diff/norm*speed);
        return norm;
    }

}