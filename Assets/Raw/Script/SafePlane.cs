using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePlane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, является ли объект игроком
        if (other.CompareTag("Player"))
        {
            Debug.Log("Fall!!");

            other.GetComponent<Player>().RespawnPlayer();
        }
    }
}
