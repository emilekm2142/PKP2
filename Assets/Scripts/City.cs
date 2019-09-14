using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class City : MonoBehaviour
{
    public string name= "miasto";
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<TextMesh>().text = name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
