using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{

    [SerializeField]
    private Text lastText;
    [SerializeField]
    private GameObject turnOff, cam, turnOn, toCam, dieParticles, body, head, win, fire;

    public bool died = false;
    float timeOfDie = 0, timeToDie = 4f;
    Vector3 lastCamPlace;
    Quaternion lastAngels;

    private void Update () {
        if (Time.time >= timeOfDie + timeToDie) died = false;
        if (died) {
            cam.transform.position = Vector3.Lerp(lastCamPlace, toCam.transform.position, (Time.time - timeOfDie) / timeToDie);
            cam.transform.rotation = Quaternion.Lerp(lastAngels, new Quaternion(180, 0, 0, 180), (Time.time - timeOfDie) / timeToDie);
        }
        
    }
    
    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Lazer") {
            Death("Тебя поджарил лазер!");
        }
        if (collider.gameObject.tag == "Bullet") {
            Death("Тебя подстрелили!");
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Lazer") {
            Death("Тебя поджарил лазер!");
        }
        if (collision.gameObject.tag == "Bullet") {
            Death("Тебя подстрелили!");
        }
    }

    public void Death(string message) {
        Cursor.visible = true;
        turnOn.SetActive(true);
        turnOff.SetActive(false);
        lastText.text = message;
        cam.GetComponent<CameraFollow>().enabled = false;
        lastCamPlace = cam.transform.position;
        lastAngels = cam.transform.rotation;
        timeOfDie = Time.time;
        died = true;

        Instantiate(dieParticles, transform.position, transform.rotation);
        body.SetActive(false);
        head.SetActive(false);
        float sens = PlayerPrefs.GetFloat("sensetivity", 2f);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("sensetivity", sens);
        PlayerPrefs.Save();
        Destroy(win);
        gameObject.GetComponent<AudioSource>().enabled = false;
        gameObject.GetComponent<MeshCollider>().enabled = false;
        gameObject.GetComponent<Move>().enabled = false;
        fire.GetComponent<Fire>().enabled = false;
        
    }

}
