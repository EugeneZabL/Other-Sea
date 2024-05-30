using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolButton : MonoBehaviour
{ 

    public ShipMov Shipi;


    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
            Shipi.SetSpeed(1f);
        if (Input.GetKey(KeyCode.S))
            Shipi.SetSpeed(-1f);
        if (Input.GetKey(KeyCode.D))
            Shipi.SetRotate(1f);
        if (Input.GetKey(KeyCode.A))
            Shipi.SetRotate(-1f);

        if (Input.GetKey(KeyCode.X))
        {
            Shipi.SetRotate(0);
        }
    }

}
