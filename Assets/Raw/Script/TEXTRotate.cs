using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEXTRotate : MonoBehaviour
{

    private GameObject UICanvas;


    private void Start()
    {
        UICanvas = GameObject.FindWithTag("Canva");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
       if(transform.IsChildOf(UICanvas.transform))
       {
            transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
       }
    }

}
