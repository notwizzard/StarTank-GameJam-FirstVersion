using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Trasition : MonoBehaviour
{
    [SerializeField]
    Text waveText;

    private float timeAnimStart = 0;
    private Image img;
    int wave;

    void Start()
    {
        wave = PlayerPrefs.GetInt("wave", 1);
        waveText.text = "Волна " + wave + " / 5";
    }

    public void onClick(string scene) {
        SceneManager.LoadScene(scene);
    }

    public void Fight() {
        
        SceneManager.LoadScene("Wave" + wave);
    }
}
