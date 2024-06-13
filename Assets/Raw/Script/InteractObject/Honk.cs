using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honk : MonoBehaviour
{

    // Переменная для хранения аудиоклипа
    public AudioClip soundClip;

    // Переменная для хранения аудиоисточника
    private AudioSource audioSource;

    private GameObject LightHouse;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = soundClip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReycastOver()
    {
        audioSource.Play();

        if(LightHouse!=null)
        {
            LightHouse.GetComponent<LightHouse>().JustDoThis();
        }
    }

    public void PlayerLookOn()
    {

    }

    public void PlayerLookOff()
    {

    }

    public void SayHi(GameObject ObjectLH)
    {
        LightHouse = ObjectLH;
    }

    public void SayBy()
    {
        LightHouse = null;
    }
}
