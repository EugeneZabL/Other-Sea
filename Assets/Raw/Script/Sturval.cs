using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crest;

public class Sturval : MonoBehaviour
{

    [SerializeField] BoatControllerUpdate boatControllerUpdate;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, 0, boatControllerUpdate._turnPower*-500);
    }
}
