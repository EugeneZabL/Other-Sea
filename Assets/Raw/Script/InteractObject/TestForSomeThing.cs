using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForSomeThing : MonoBehaviour
{
    [SerializeField] GameObject uiObject;

    private GameObject UICanvas;
    // Start is called before the first frame update
    void Start()
    {
       UICanvas = GameObject.FindWithTag("Canva");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerLookOn()
    {
        Debug.Log("Player is looking at: " + this.name);
        uiObject.transform.SetParent(UICanvas.transform);
    }

    public void PlayerLookOff()
    {
        Debug.Log("Player stopped looking at: " + this.name);
        uiObject.transform.SetParent(transform);
    }

}
