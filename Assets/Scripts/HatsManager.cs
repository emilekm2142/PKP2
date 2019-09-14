using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class HatDataHolder
{
    public Hat hat;
    public CoolHatTypes type;
    public string name;
}
public class HatsManager : MonoBehaviour
{
    public List<HatDataHolder> hats = new List<HatDataHolder>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
