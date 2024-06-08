using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crest;

public class StopRig : MonoBehaviour
{
    BoatControllerUpdate boatControllerUpdate;
    
    // Start is called before the first frame update
    void Start()
    {
        boatControllerUpdate = GameObject.FindWithTag("FizShip").GetComponent<BoatControllerUpdate>();
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


}
    