using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crest;

public class PlayerBoatController : MonoBehaviour
{
    [SerializeField] Transform controledPlayerPos;
    [SerializeField] Transform Sturval;

    [SerializeField] BoatControllerUpdate Boat;

    bool isActive = false;

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            ButtonCheck();
        }
    }

    void ButtonCheck()
    {
        if(Input.GetKey(KeyCode.W))
        {
            Boat._enginePower += 0.001f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Boat._enginePower -= 0.001f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Boat._turnPower += 0.001f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Boat._turnPower -= 0.001f;
        }
        if (Input.GetKey(KeyCode.X))
        {
            Boat._enginePower = 0;
            Boat._turnPower = 0;
        }
    }


    public Vector3 Intial()
    {
        isActive = true;
        return controledPlayerPos.transform.position;
    }

    public void Stopp()
    {
        isActive=false;
    }
}
