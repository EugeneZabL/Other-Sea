using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounBy4Dots : MonoBehaviour
{
    public Transform[] floatPoints; // Массив из 4 угловых точек

    public GameObject shipMain;

    public float speed;

    void FixedUpdate()
    {

    }

    private void Update()
    {
        float x = (((floatPoints[0].transform.position.y + floatPoints[1].transform.position.y) / 2) + ((floatPoints[2].transform.position.y + floatPoints[3].transform.position.y)/2 ));
        float z = (((floatPoints[0].transform.position.y + floatPoints[3].transform.position.y) / 2) + ((floatPoints[1].transform.position.y + floatPoints[2].transform.position.y) )/2);
        transform.rotation = Quaternion.Euler(-x, 0f, -z);


        shipMain.transform.Translate(0,0,speed*Time.deltaTime);
    }
}
