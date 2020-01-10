using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public GameObject Player;
    public Slider Slider;
    public Text Value;


    void Start()
    {

    }

    void Update()
    {
        Player.GetComponent<CharController>().speedsetting = Slider.value;
        Value.text = "" + Slider.value;
    }
}
