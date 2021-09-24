using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sensetivity : MonoBehaviour
{
    [SerializeField]
    Text text;

    Slider slider;

    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("sensetivity", 2f);
        text.text = "" + PlayerPrefs.GetFloat("sensetivity", 2f);
    }

    public void SaveSens () {
        float newSens = slider.value;
        newSens = Mathf.Round(newSens * 10f) / 10;
        text.text = "" + newSens;
        PlayerPrefs.SetFloat("sensetivity", newSens);
        PlayerPrefs.Save();
    }
}
