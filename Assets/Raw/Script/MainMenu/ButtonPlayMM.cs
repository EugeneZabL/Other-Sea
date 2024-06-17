using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonPlayMM : MonoBehaviour
{
    private int FMode;
    [SerializeField] TextMeshProUGUI _text;

    [SerializeField] GameObject _gameObject;
    // Start is called before the first frame update
    void Start()
    {
        FMode = PlayerPrefs.GetInt("FanMode");
        if(FMode == null)
        {
            FMode = 0;
            PlayerPrefs.SetInt("FunMode", 0);
        }
        WriteStatusFB();
    }

    // Update is called once per frame
    void Update()
    {
        RotateShip();
    }

    public void SGameButton()
    {
        SceneManager.LoadScene(1);
    }

    public void FunModeSeting()
    {
        if(FMode==0)
            FMode = 1;
        else
            FMode = 0;

        PlayerPrefs.SetInt("FunMode", FMode);
        WriteStatusFB();
    }

    void WriteStatusFB()
    {
        if (FMode == 0)
            _text.text = "Normal Mode";
        else
            _text.text = "Fun Mode";
    }

    void RotateShip()
    {
        _gameObject.transform.Rotate(0,Time.deltaTime*20,0);

    }
}
