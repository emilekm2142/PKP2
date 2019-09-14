using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public string name;
   
    public TrainPath currentPath;

    public bool isFollowingAnyPath = false;

    // Start is called before the first frame update
    public Thomas thomas;
    List<Wagon> wagons = new List<Wagon>();

    void Start()
    {
        thomas = GetComponentInChildren<Thomas>();
    }

    public void FollowPath(TrainPath path)
    {
        Start();
        thomas.FollowPath(path);
        for (int i = 0; i < wagons.Count; i++)
        {
            Debug.Log(i);
            var d = i; 
            Run.After(0.4f, () =>
            {
                
                wagons[d].FollowPath(path);
              });
           
        }
    }

    public Wagon AddWagon(bool isPlayer)
    {
        var wagon = Instantiate(GameObject.FindObjectOfType<GameManager>().wagonPrefab, new Vector3(5, 0, 5),
            Quaternion.identity);
        wagon.name ="Wagon of " + name;
       
        wagons.Add(wagon.GetComponent<Wagon>());
        return wagon.GetComponent<Wagon>();
    }


    // Update is called once per frame
    void Update()
    {
    }
}