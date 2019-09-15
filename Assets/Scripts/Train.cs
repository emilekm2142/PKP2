using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Train : MonoBehaviour
{
    public string name;
   
    public TrainPath currentPath;

    public bool isFollowingAnyPath = false;

    // Start is called before the first frame update
    public Thomas thomas;
    public List<Wagon> wagons = new List<Wagon>();

    void Start()
    {
        thomas = GetComponentInChildren<Thomas>();
    }

    public void FollowPath(TrainPath path)
    {
        Start();
        thomas.FollowPath(path, 85*wagons	.Count);
        Run.After(0, () =>
        {
            for (int i = 0; i < wagons.Count; i++)
            {
                Debug.Log(i);
                var d = i;
                wagons[d].number = d;
                wagons[d].parent = this;
                Run.After(0, () => { wagons[d].FollowPath(path, (wagons	.Count-d)*76); });

            }
        });
    }

    public Wagon AddWagon(bool isPlayer)
    {
        var wagon = Instantiate(GameObject.FindObjectOfType<GameManager>().wagonPrefab, new Vector3(5, 0, 5),
            Quaternion.identity);
        wagon.name ="Wagon of " + name;
        if (isPlayer)
        {
            wagon.AddComponent<Player>();
            //wagon.GetComponent<Wagon>().AddHat(FindObjectOfType<HatsManager>().hats.First(d=>d.type==CoolHatTypes.Cylinder).hat.GetComponent<Hat>());
        }
        wagons.Add(wagon.GetComponent<Wagon>());
        return wagon.GetComponent<Wagon>();
    }


    // Update is called once per frame
    void Update()
    {
    }
}