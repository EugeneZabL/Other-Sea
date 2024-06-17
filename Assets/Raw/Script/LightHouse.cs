using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHouse : MonoBehaviour
{
    [SerializeField] GameObject MyLight;
    private Honk Cap;

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        Cap = GameObject.FindGameObjectWithTag("Honk").GetComponent<Honk>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // ���������, �������� �� ������ �������
        if (other.CompareTag("RigBodMarker"))
        {
            Debug.Log("����� ����� � ���� ���������� ��������");

            Cap.SayHi(this.gameObject);

            // ��� ���, ������� ������ �����������, ����� ����� ������ � ����
            // ��������: 
            // other.GetComponent<Player>().ActivateQuestItem();
        }
    }

    // ����� ����������, ����� ������ � ����������� ������� �� ����
    private void OnTriggerExit(Collider other)
    {
        // ���������, �������� �� ������ �������
        if (other.CompareTag("RigBodMarker"))
        {
            Debug.Log("����� ����� �� ���� ���������� ��������");
            // ��� ���, ������� ������ �����������, ����� ����� ������� �� ����
            // ��������: 
            // other.GetComponent<Player>().DeactivateQuestItem();
            Cap.SayBy();
        }
    }

    public void JustDoThis()
    {
        if(!MyLight.activeSelf)
        {
            MyLight.SetActive(true);
            player.LightHouseLeft--;
        }
    }
}
