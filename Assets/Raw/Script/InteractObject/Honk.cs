using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honk : MonoBehaviour
{

    // Переменная для хранения аудиоклипа
    public AudioClip soundClip;

    // Переменная для хранения аудиоисточника
    private AudioSource audioSource;

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
    }

    public void PlayerLookOn()
    {

    }

    public void PlayerLookOff()
    {

    }
}
