using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
   // [SerializeField] bool isSee = true;
    [SerializeField] Vector2 StartPosition;
    [SerializeField] Transform ShipPoint;

    [SerializeField] int MulOFsize = 100;

    [SerializeField] GameObject MapMarkerNor;
    [SerializeField] GameObject MapMarkerQuest;

    [SerializeField] Transform MarkerObj;

    //List<GameObject> listMarker;
    // Start is called before the first frame update
    void Start()
    { 

       // MarkerObj.transform.localPosition = StartPosition;
       // MarkerObj.transform.localScale = new Vector3(200, 200, 200) / MulOFsize;
        transform.localPosition = StartPosition;
        GameObject[] listMarker = GameObject.FindGameObjectsWithTag("MapMarker");
        foreach (GameObject go in listMarker)
        {
            GameObject Marker = GameObject.Instantiate(MapMarkerNor);
            Marker.transform.SetParent(MarkerObj);
            Marker.transform.rotation = transform.GetComponentInParent<Transform>().rotation;
            Marker.transform.localPosition = new Vector2(go.transform.position.x/MulOFsize,go.transform.position.z/MulOFsize) + StartPosition;
            Marker.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
        listMarker = GameObject.FindGameObjectsWithTag("MapMarkerQuest");
        foreach (GameObject go in listMarker)
        {
            GameObject Marker = GameObject.Instantiate(MapMarkerQuest);
            Marker.transform.SetParent(MarkerObj);
            Marker.transform.rotation = transform.GetComponentInParent<Transform>().rotation;
            Marker.transform.localPosition = new Vector2(go.transform.position.x / MulOFsize, go.transform.position.z / MulOFsize) + StartPosition;
            Marker.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
        MapMarkerNor.SetActive(false);
        MapMarkerQuest.SetActive(false);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 a = new Vector2 (ShipPoint.position.x / MulOFsize, ShipPoint.position.z / MulOFsize);
        transform.localPosition = a + StartPosition; 
    }
}
