using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField]
    private GameObject dulo, duloFrom, bullet;
    [SerializeField]
    private float timeToFire, speed;
    [SerializeField]
    AudioClip clip, shooted;

    private AudioSource audio;
    private float lastFireTime;

    private void Start()
    {
        lastFireTime = Time.time;
        audio = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) {
            if (Time.time - timeToFire >= lastFireTime) {
                audio.PlayOneShot(clip);
                lastFireTime = Time.time;
                GameObject bul = Instantiate(bullet, dulo.transform.position, dulo.transform.rotation);
                Rigidbody rb = bul.GetComponent<Rigidbody>();
                Vector3 to = dulo.transform.position - duloFrom.transform.position;
                to.Normalize();
                rb.AddForce(to * speed); 
            }
        }
    }

    public void Shooted() {
        audio.PlayOneShot(shooted);
    }
}
