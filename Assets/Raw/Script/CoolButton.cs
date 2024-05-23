using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolButton : MonoBehaviour
{
    public GameObject POLL;
    Vector3 oldPosition;

    public ShipMov Shipi;

   
    private void Start()
    {
        oldPosition = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 t = new Vector3(Camera.main.transform.position.x,0,Camera.main.transform.position.z);
        if(Vector3.Distance(oldPosition,t)>=50)
        {
            POLL.transform.position = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);
            Debug.Log("NEW POSITION!");
            oldPosition = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);
        }
    }

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
    }

}
