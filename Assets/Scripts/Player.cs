using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent	<Wagon>().playerLight	.gameObject.SetActive	(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
