using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compas : MonoBehaviour
{
    // Update is called once per frame
    // Вектор северного направления (по умолчанию это ось Z)
    private Vector3 northDirection = Vector3.forward;

    void Update()
    {
        // Определяем вращение, которое направляет объект на север относительно мировых координат
        Quaternion targetRotation = Quaternion.LookRotation(northDirection, Vector3.up);

        // Применяем это вращение к объекту компаса
        transform.rotation = targetRotation;
    }
}
