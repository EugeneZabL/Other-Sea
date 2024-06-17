using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeLoad : MonoBehaviour
{

    float screenWidth = Screen.width;
    float screenHeight = Screen.height;

    bool isAnim = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(screenWidth, screenHeight*2.5f, 1);
        transform.localPosition = new Vector3(0,-screenHeight* 2.5f, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isAnim)
        {
            transform.localPosition = new Vector3(0, transform.localPosition.y+15, 0);
            if(transform.localPosition.y >= screenHeight * 2.5f)
            {
                isAnim = false;
                transform.localPosition = new Vector3(0, -screenHeight * 2.5f, 0);
            }    
        }
    }

    public void AskForAnim()
    {
        isAnim = true;
        transform.localPosition = new Vector3(0, 0, 0);
    }

}
