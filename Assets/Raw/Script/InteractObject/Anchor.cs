using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Anchor : MonoBehaviour
{
    [SerializeField] ShipAnchor scriptAnc;

    bool isActive = false;

    [SerializeField] GameObject uiObject;

    private GameObject UICanvas;

    public TextMeshProUGUI TextTMP;


    private void Start()
    {
        UICanvas = GameObject.FindWithTag("Canva");
    }

    public void ReycastOver()
    {
        isActive = !isActive;

        Debug.Log("Anchor is " + isActive);

        scriptAnc.TipReset();
        scriptAnc.isActive = isActive;

        TextTMP.text = "Anchor is " + isActive;
    }

    public void PlayerLookOn()
    {
        TextTMP.text = "Anchor is " + isActive;
        uiObject.transform.SetParent(UICanvas.transform);
    }

    public void PlayerLookOff()
    {
        uiObject.transform.SetParent(transform);
    }
}
