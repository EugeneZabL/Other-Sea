using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyShipPos : MonoBehaviour
{
    public GameObject FizShip;


    void Update()
    {
        transform.position = FizShip.transform.position;
        transform.rotation = FizShip.transform.rotation;
    }
}
