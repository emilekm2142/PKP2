using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public float speed = 0.4f;
    public bool IsClicked = false;
    public bool IsExploded = false;
    public float StepTowardsTrain(Train train)
    {
        Vector3 diff = train.gameObject.transform.position - gameObject.transform.position;
        float norm = Mathf.Sqrt(diff[0] * diff[0] + diff[1] * diff[1] + diff[2] * diff[2]);
        
        gameObject.transform.LookAt(train.gameObject.transform.position);
        gameObject.transform.Translate(new Vector3(0f,0f, speed));
        return norm;
    }

    private void OnMouseDown()
    {
        IsClicked = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision");
        IsExploded = true;
    }
}