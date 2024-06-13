using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMinus : MonoBehaviour
{
    private Radio _radio;
    // Start is called before the first frame update
    void Start()
    {
        _radio = GameObject.FindWithTag("Radioo").GetComponent<Radio>();
    }

    public void ReycastOver()
    {
        _radio.DecreaseVolume();
    }
}
