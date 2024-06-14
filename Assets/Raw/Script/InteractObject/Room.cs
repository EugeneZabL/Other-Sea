using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] Transform _teleportPos;

    GameObject UICanvas;
    private GameObject player;

    [SerializeField] GameObject uiObject;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        UICanvas = GameObject.FindWithTag("Canva");
    }

    public void ReycastOver()
    {
        player.GetComponent<Player>().ResetLastPlayerPos(2);

        player.transform.position = _teleportPos.position;
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
