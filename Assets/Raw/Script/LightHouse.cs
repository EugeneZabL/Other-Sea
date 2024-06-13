using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHouse : MonoBehaviour
{
    [SerializeField] GameObject MyLight;
    private Honk Cap;
    // Start is called before the first frame update
    void Start()
    {
        Cap = GameObject.FindGameObjectWithTag("Honk").GetComponent<Honk>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, является ли объект игроком
        if (other.CompareTag("RigBodMarker"))
        {
            Debug.Log("Игрок вошел в зону квестового предмета");

            Cap.SayHi(this.gameObject);

            // Ваш код, который должен выполняться, когда игрок входит в зону
            // Например: 
            // other.GetComponent<Player>().ActivateQuestItem();
        }
    }

    // Метод вызывается, когда объект с коллайдером выходит из зоны
    private void OnTriggerExit(Collider other)
    {
        // Проверяем, является ли объект игроком
        if (other.CompareTag("RigBodMarker"))
        {
            Debug.Log("Игрок вышел из зоны квестового предмета");
            // Ваш код, который должен выполняться, когда игрок выходит из зоны
            // Например: 
            // other.GetComponent<Player>().DeactivateQuestItem();
            Cap.SayBy();
        }
    }

    public void JustDoThis()
    {
        if(!MyLight.activeSelf)
        {
            MyLight.SetActive(true);
        }
    }
}
