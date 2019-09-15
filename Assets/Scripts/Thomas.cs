using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thomas : MonoBehaviour
{
    private TrainPath currentPath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FollowPath(TrainPath path)
    {
        currentPath = path;
        var i = 0;
        Run.EachFrame(() =>
        {
            if (i < currentPath.points.Count - 1)
            {
                gameObject.transform.parent.position = currentPath.points[i]; 
                //gameObject.transform.position = currentPath.points[i];
                gameObject.transform.LookAt(currentPath.points[i+5]);
                i++;
            }
        });
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
