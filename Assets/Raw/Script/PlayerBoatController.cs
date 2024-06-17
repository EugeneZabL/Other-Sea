using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crest;

public class PlayerBoatController : MonoBehaviour
{
    [SerializeField] Transform controledPlayerPos;
    [SerializeField] Transform Sturval;

    [SerializeField] BoatControllerUpdate Boat;

    public float MaxSpeed;
    [SerializeField] float MaxRotate;

    [SerializeField] Transform PointSpeed;
    [SerializeField] Transform PointSpeedBack;

    float _Speed = 0f;
    float _Rotate = 0f;

    public bool isActive = false;

    private void Start()
    {
        //PlayerPrefs.SetInt("HighScore", 100);
        if (PlayerPrefs.GetInt("FunMode") == 1)
        {
            MaxSpeed = 100;
        }
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

    private void FixedUpdate()
    {
        SpedometerCalc();
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


    void SpedometerCalc()
    {
        if(_Speed>=0)
        {
            PointSpeed.transform.localRotation = Quaternion.Euler(0, 0, _Speed * -100);
            PointSpeedBack.transform.localRotation =  Quaternion.Euler(0, 0, 0);
        }
        else
        {
            PointSpeed.transform.localRotation = Quaternion.Euler(0, 0, 0);
            PointSpeedBack.transform.localRotation = Quaternion.Euler(0, 0, _Speed * -100);
        }
        
    }
}
