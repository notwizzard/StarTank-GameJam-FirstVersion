using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boosters : MonoBehaviour
{
    [SerializeField]
    Text speedText, damageText, pointsText;
    [SerializeField]
    GameObject player, upgradeParticles;

    float speed, points, damage;

    void Start()
    {
        GetAll();
        UpdateTexts();
    }

    void Update()
    {
        
    }

    private void UpdateTexts () {
        speedText.text = "" + speed + " уровень";
        pointsText.text = "Очки улучшений: " + points;
        damageText.text = "" + damage + " уровень";
    }

    public void BoostSpeed() {
        if (points > 0) {
            points -= 1;
            speed += 1;
            Instantiate(upgradeParticles, player.transform.position, player.transform.rotation);
        }
        SaveAll();
        UpdateTexts();
    }

    public void BoostDamage() {
        if (points > 0) {
            points -= 1;
            damage += 1;
            Instantiate(upgradeParticles, player.transform.position, player.transform.rotation);
        }
        SaveAll();
        UpdateTexts();
    }

    private void GetAll () {
        speed = PlayerPrefs.GetFloat("speed", 1f);
        damage = PlayerPrefs.GetFloat("damage", 1f);
        points = PlayerPrefs.GetFloat("points", 0f);
    } 

    private void SaveAll() {
        PlayerPrefs.SetFloat("speed", speed);
        PlayerPrefs.SetFloat("damage", damage);
        PlayerPrefs.SetFloat("points", points);
        PlayerPrefs.Save();
    }

}
