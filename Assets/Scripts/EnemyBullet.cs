using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    [SerializeField]
    private float speed, range;
    [SerializeField]
    GameObject bulletParticle, duloFrom;

    private Rigidbody rb;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.tag != "Enemy") {
            Destroy(Instantiate(bulletParticle, transform.position, transform.rotation), 5f);
            Destroy(gameObject);
        }
    }
}
