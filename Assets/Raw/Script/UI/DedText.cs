using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DedText : MonoBehaviour
{
    TextMeshProUGUI TextTMP;

    Player player;

    string text = "Hey, you! Help me light all the Lighthouses. Lighthouses left - ";
    // Start is called before the first frame update
    void Start()
    {
        TextTMP = GetComponent<TextMeshProUGUI>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player.LightHouseLeft==0)
        {
            TextTMP.text = "Thx, bro!";
        }
        else
        {
            TextTMP.text = text + player.LightHouseLeft;
        }
    }
}
