using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crest;

public class StopRig : MonoBehaviour
{
    BoatControllerUpdate boatControllerUpdate;


    [SerializeField] GameObject uiObject;

    private GameObject UICanvas;


    // Start is called before the first frame update
    void Start()
    {
        boatControllerUpdate = GameObject.FindWithTag("FizShip").GetComponent<BoatControllerUpdate>();
        UICanvas = GameObject.FindWithTag("Canva");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReycastOver()
    {
        boatControllerUpdate._enginePower = 0;
        boatControllerUpdate._turnPower=0;
        Debug.Log("Raycast hit this object: " + gameObject.name);
    }
    public void PlayerLookOn()
    {
        uiObject.transform.SetParent(UICanvas.transform);
    }

    public void PlayerLookOff()
    {
        uiObject.transform.SetParent(transform);
    }

}
    