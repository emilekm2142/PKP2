using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    // Start is called before the first frame update
    public string text = "";
    void Start()
    {
        
    }

    private void OnMouseEnter()
    {
        FindObjectOfType<UI>().SetTooltip	(text);
    }
    private void OnMouseExit()
    {
        FindObjectOfType	<UI>().tooltip	.gameObject.SetActive	(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
