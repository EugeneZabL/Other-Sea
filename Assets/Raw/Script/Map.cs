using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] bool isSee = true;
    [SerializeField] Vector2 StartPosition;
    [SerializeField] Transform ShipPoint;

    [SerializeField] int MulOFsize = 100;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = StartPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 a = new Vector2 (ShipPoint.position.x / MulOFsize, ShipPoint.position.z / MulOFsize);
        transform.localPosition = a + StartPosition; 
    }
}
