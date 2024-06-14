using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitRoom : MonoBehaviour
{
    private GameObject player;

    //  [SerializeField] GameObject uiObject;

    //GameObject UICanvas;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        //UICanvas = GameObject.FindWithTag("Canva");
    }

    public void ReycastOver()
    {
        player.GetComponent<Player>().ReturnToShip();
    }

    /*
    public void PlayerLookOn()
    {
        uiObject.transform.SetParent(UICanvas.transform);
    }

    public void PlayerLookOff()
    {
        uiObject.transform.SetParent(transform);
    }
    */
}
