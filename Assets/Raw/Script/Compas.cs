using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compas : MonoBehaviour
{
    // Update is called once per frame
    // ������ ��������� ����������� (�� ��������� ��� ��� Z)
    private Vector3 northDirection = Vector3.forward;

    void Update()
    {
        // ���������� ��������, ������� ���������� ������ �� ����� ������������ ������� ���������
        Quaternion targetRotation = Quaternion.LookRotation(northDirection, Vector3.up);

        // ��������� ��� �������� � ������� �������
        transform.rotation = targetRotation;
    }
}
