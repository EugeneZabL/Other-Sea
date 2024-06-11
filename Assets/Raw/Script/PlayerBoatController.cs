using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crest;

public class PlayerBoatController : MonoBehaviour
{
    [SerializeField] Transform controledPlayerPos;
    [SerializeField] Transform Sturval;

    [SerializeField] BoatControllerUpdate Boat;

    [SerializeField] float MaxSpeed;
    [SerializeField] float MaxRotate;

    float _Speed = 0f;
    float _Rotate = 0f;

    bool isActive = false;

    private void Start()
    {
        //Boat._enginePower = _Speed;
       // Boat._turnPower = _Rotate;
    }

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

        _Speed = Boat._enginePower;
        _Rotate = Boat._turnPower;

        if(Input.GetKey(KeyCode.W))
        {
            _Speed += 0.001f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _Speed -= 0.001f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _Rotate += 0.001f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _Rotate-= 0.001f;
        }

        CheckLimit();

        Boat._enginePower = _Speed;
        Boat._turnPower = _Rotate;
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

    void CheckLimit()
    {
        if(_Speed>MaxSpeed)
            _Speed = MaxSpeed;
        else if(_Speed<-MaxSpeed/2)
            _Speed = -MaxSpeed/2;

        if(_Rotate>MaxRotate)
            _Rotate = MaxRotate;
        else if(_Rotate<-MaxRotate)
                _Rotate = -MaxRotate;
    }
}
