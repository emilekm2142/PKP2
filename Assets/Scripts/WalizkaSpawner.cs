using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalizkaSpawner : MonoBehaviour
{
    public List<Walizka> walizki = new List<Walizka>();
    private GameObject manager;
    public GameObject walizkaPrefab;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void spawn()
    {
        var cities = FindObjectsOfType<City>();
        for (int i = 0; i < 25; i++)
        {
            var randomCity = cities[Mathf.RoundToInt(Random.Range(0, cities.Length))];
            walizki.Add	(Instantiate	(walizkaPrefab	, randomCity.gameObject.transform.position + new Vector3(Random.Range(5,11), -1.3f, Random.Range(5,11)), Quaternion.identity	).GetComponent	<Walizka>());
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
