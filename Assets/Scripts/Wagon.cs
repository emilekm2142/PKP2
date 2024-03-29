﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wagon : MonoBehaviour
{
    public Train parent;
    public int number;
    public Light playerLight;
    public List<Hat> currentHats = new List<Hat>();
    public bool colliding;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnTriggerEnter(Collider other){
       // if (transform.position!= new Vector3(5,0,5) && !Utils.HasComponent<City>(other.gameObject)) colliding =true;
        if (Utils.HasComponent<City>(other.gameObject))
        {
            FindObjectOfType	<UI>().ShowStation	();
        }
    }

    public void OnTriggerStay(Collider other)
    {
       // colliding = true;
    }

    public void OnTriggerExit(Collider other){
        colliding =false;
    }
    private TrainPath currentPath;

    public void AddHat(Hat hat)
    {
        //var hatsManager = GameObject.FindObjectOfType<HatsManager>();
        var newHat = Instantiate(hat.gameObject, this.transform);
        newHat.transform.rotation = transform.rotation;
        newHat.transform.localPosition = new Vector3(0,0,0);
        currentHats.Add(newHat.GetComponent<Hat>());
        
    }
    public void FollowPath(TrainPath path,int firstStep)
    {
        currentPath = path;
   
        var i = firstStep;
        Run.EachFrame(() =>
        {
        
            if (!colliding)
            {
          
                if (i < currentPath.points.Count - 1)
                {
                   
                    gameObject.transform.position = currentPath.points[i];
                    gameObject.transform.LookAt(currentPath.points[i + 5]);
                    i++;
                }
            }
        });
    }

    bool IsPlayer()
    {
        return Utils.HasComponent<Player>(this.gameObject);
    }

    public void ChangeColor(Color c)
    {
        foreach (var child in GetComponentsInChildren<MeshRenderer>())
        {
            child.material.color = c;
        }
    }
    
    public void ChangeTexture(Texture2D t)
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
            
    }
}
