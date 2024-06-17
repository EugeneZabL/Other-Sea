using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crest;

public class Engine : MonoBehaviour
{
    [SerializeField] BoatControllerUpdate _fizBoat;
    [SerializeField] int persetOfRand = 2;

    bool isWork = false;

    [SerializeField] GameObject particle;

    [SerializeField] GameObject UiObject;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isWork)
        {
            if (Random.Range(0, 100000) <= persetOfRand)
            {
                particle.SetActive(true);
                isWork = false;
            }
        }
        else
        {
            _fizBoat._enginePower = 0;
        }
    }

    public void ReycastOver()
    {
        if(!isWork)
        {
            isWork = true;
            particle.SetActive(false);
            UiObject.SetActive(false);
        }
    }

    public void PlayerLookOn()
    {
        if(!isWork)
        {
            UiObject.SetActive(true);
        }
            
    }

    public void PlayerLookOff()
    {
        UiObject.SetActive(false);
    }
}
