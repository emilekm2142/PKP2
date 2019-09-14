using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationManager : MonoBehaviour
{
    public Wagon myWagon;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Fetch()
    {
        ApiManager apiManager = GameObject.FindObjectOfType<ApiManager>();
        PresetManager presetManager = GameObject.FindObjectOfType<PresetManager>();

        int presetId = (int) apiManager.User.activeSkinId;
        Preset preset = presetManager.presetList[presetId];
        myWagon.ChangeColor(preset.color);
        myWagon.ChangeTexture(preset.texture);
    }
}
