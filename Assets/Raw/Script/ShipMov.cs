using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMov : MonoBehaviour
{

    public float MaxSpeed = 5.0f;
    float CorrSpeed = 0.0f;

    public float speedOfROtate = 3.0f;

    public float MaxRorate = 3.0f;
    float CorrRotate = 0.00f;

    public GameObject Point;

    private void Start()
    {
        Point.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5); 
    }


    // Update is called once per frame
    void Update()
    {
        MovePoin();

        transform.position = Vector3.Lerp(transform.position, Point.transform.position, CorrSpeed * Time.deltaTime);


        Vector3 direction = Point.transform.position - transform.position;
        direction.y = 0; // Игнорирование изменения по оси Y

        // Если направление ненулевое, поворачиваем объект
        if (direction != Vector3.zero)
        {
            // Определение целевой ротации
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Плавный поворот объекта к целевой ротации с использованием Slerp для еще более плавного эффекта
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedOfROtate * Time.deltaTime);
        }
    }

    void MovePoin()
    {
        if(CorrSpeed>MaxSpeed)
        {
            CorrSpeed = MaxSpeed;
        }
        if(CorrSpeed<0)
        {
            CorrSpeed = 0;
        }

        if (CorrRotate > MaxRorate)
        {
            CorrRotate = MaxRorate;
        }
        if (CorrRotate < -MaxRorate)
        {
            CorrRotate = -MaxRorate;
        }

        Point.transform.localPosition = new Vector3(CorrRotate, 0, 7+(CorrSpeed*0.1f));

    }

    public float GiveCorSpeed()
    {
        return CorrSpeed;
    }

    public float GiveCorRotation()
    {
        return CorrRotate;
    }

    public void SetSpeed(float speed)
    {
        if (speed == 1f)
            CorrSpeed = CorrSpeed + 0.001f;
        else if(speed == -1f)
            CorrSpeed = CorrSpeed - 0.001f;
        else
            CorrSpeed = speed;
    }

    public void SetRotate(float rotate)
    {
        if (rotate == 1f)
            CorrRotate = CorrRotate + 0.001f;
        else if (rotate == -1f)
            CorrRotate = CorrRotate - 0.001f;
        else
            CorrRotate = rotate;
    }
}
