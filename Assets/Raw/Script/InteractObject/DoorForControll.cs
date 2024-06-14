using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorForControll : MonoBehaviour
{
    GameObject _cabina;
    GameObject _player;
    [SerializeField] GameObject uiObject;
    GameObject UICanvas;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _cabina = GameObject.FindGameObjectWithTag("Cabina");
        UICanvas = GameObject.FindWithTag("Canva");
    }

    public void ReycastOver()
    {
        if(!_cabina.GetComponent<PlayerBoatController>().isActive)
        {
            _player.GetComponent<Player>().ResetLastPlayerPos(0);

            _player.transform.SetParent(_cabina.transform);

            _player.transform.position = _cabina.GetComponent<PlayerBoatController>().Intial();
        }
    }

    public void PlayerLookOn()
    {
        if(!_cabina.GetComponent<PlayerBoatController>().isActive)
        uiObject.transform.SetParent(UICanvas.transform);
    }

    public void PlayerLookOff()
    {
        uiObject.transform.SetParent(transform);
    }
}
