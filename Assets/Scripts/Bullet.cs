using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    GameObject bulletParticle;
    [SerializeField]
    AudioClip shooted;

    private Rigidbody rb;
    
    private void Start()
    {
        Destroy(gameObject, 5f);  
    }

    private void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.tag != "Player") {
            if (coll.gameObject.tag == "Enemy")
                GameObject.FindGameObjectsWithTag("HeadWithAudio")[0].GetComponent<AudioSource>().PlayOneShot(shooted);
            Destroy(Instantiate(bulletParticle, transform.position, transform.rotation), 1f);
            Destroy(gameObject);
        }
        
    }
}
