using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject panel;
    public bool CustomizationDisplayed = false;
    public Text stationText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowStation()
    {
        Debug.Log	("joy");
        stationText.text = "Stacja! +100pkt";
        stationText.GetComponent<Animator>().Play	("textInAnim");
    }

    public void ShowCustomization()
    {
        panel.SetActive	(!panel.active);
    }

    public void SetHatToHussar()
    {
        FindObjectOfType<GameManager>().getMyWagon().GetComponent<Wagon>().AddHat(FindObjectOfType<HatsManager>().hats.First(d=>d.type==CoolHatTypes.Hussar).hat.GetComponent<Hat>());

    }

    public void SetHatToCylinder()
    {
        FindObjectOfType<GameManager>().getMyWagon().GetComponent<Wagon>().AddHat(FindObjectOfType<HatsManager>().hats.First(d=>d.type==CoolHatTypes.Cylinder).hat.GetComponent<Hat>());

    }
    public void SetHatToBeret()
    {
        FindObjectOfType<GameManager>().getMyWagon().GetComponent<Wagon>().AddHat(FindObjectOfType<HatsManager>().hats.First(d=>d.type==CoolHatTypes.Beret).hat.GetComponent<Hat>());

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
