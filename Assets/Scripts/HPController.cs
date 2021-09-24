using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour
{
    
    [SerializeField]
    private GameObject e;

    private Enemy enemy;
    float damage;

    void Start()
    {
        enemy = e.GetComponent<Enemy>();
        damage = PlayerPrefs.GetFloat("damage", 1);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "PlayerBullet") {
            enemy.HP = enemy.HP - damage;
        }
        if (collision.gameObject.tag == "Lazer") {
            enemy.HP = 0;
        }
    }
}
