using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    [SerializeField] ShipAnchor scriptAnc;

    bool isActive = false;

    public void ReycastOver()
    {
        isActive = !isActive;

        Debug.Log("Anchor is " + isActive);

        scriptAnc.TipReset();
        scriptAnc.isActive = isActive;
    }
}
