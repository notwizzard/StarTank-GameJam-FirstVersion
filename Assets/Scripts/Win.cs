using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour
{

    [SerializeField]
    GameObject[] enemy;
    [SerializeField]
    Text enemysText;
    [SerializeField]
    GameObject turnOn, turnOff, player, particle;

    bool winned = false;

    void Start()
    {
        StartCoroutine("isWin");
    }

    IEnumerator isWin() {
        if (!winned) {
            int countNow = 0;
            for (int i = 0; i < enemy.Length; i ++) {
                if (enemy[i] == null) {
                    countNow += 1;
                }
            }
            enemysText.text = countNow + " / " + enemy.Length;
            if (countNow == enemy.Length) {
                winned = true;
                Winned();
            }
        }
        

        yield return new WaitForSeconds(1f);
        StartCoroutine("isWin");

    }

    void Winned() {
        Cursor.visible = true;
        float points = PlayerPrefs.GetFloat("points", 0);
        points += 1;
        PlayerPrefs.SetFloat("points", points);
        int wave = PlayerPrefs.GetInt("wave", 1);
        if (wave < 5) PlayerPrefs.SetInt("wave", wave + 1);
        PlayerPrefs.Save();
        turnOn.SetActive(true);
        turnOff.SetActive(false);
        Instantiate(particle, player.transform.position, player.transform.rotation);
        Destroy(player);
    }
}
