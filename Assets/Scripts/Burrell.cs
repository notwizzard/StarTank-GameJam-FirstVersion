using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burrell : MonoBehaviour
{

    [SerializeField]
    GameObject explosionParticle, deadParticle, player;
    [SerializeField]
    float killDistance, burrellSpeed;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "PlayerBullet") {
            Kill();
            Instantiate(explosionParticle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
    }

    public void Kill() {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("EnemyContainer");
        for (int i = 0; i < enemy.Length; i ++) {
            if((transform.position - enemy[i].transform.position).magnitude <= killDistance) {
                Ray ray = new Ray(transform.position, enemy[i].transform.position - transform.position);
                RaycastHit[] hits;
                hits = Physics.RaycastAll(ray, killDistance);
                bool wall = false;
                foreach (RaycastHit hit in hits) {
                    if (hit.collider.gameObject.tag == "Wall") {
                        if ((hit.collider.gameObject.transform.position - transform.position).magnitude < (enemy[i].transform.position - transform.position).magnitude) {
                            wall = true;
                        }
                    }
                }

                if (!wall) {
                    enemy[i].GetComponent<Enemy>().Dead();
                }

            }
        }


        GameObject[] burrell = GameObject.FindGameObjectsWithTag("Burrell");
        List<int> burrellId = new List<int>();
        for (int i = 0; i < burrell.Length; i ++) {
            if((transform.position - burrell[i].transform.position).magnitude <= killDistance) {
                Ray ray = new Ray(transform.position, burrell[i].transform.position - transform.position);
                RaycastHit[] hits;
                hits = Physics.RaycastAll(ray, killDistance);
                bool wall = false;
                foreach (RaycastHit hit in hits) {
                    if (hit.collider.gameObject.tag == "Wall") {
                        if ((hit.collider.gameObject.transform.position - transform.position).magnitude < (burrell[i].transform.position - transform.position).magnitude) {
                            wall = true;
                        }
                    }
                }

                if (!wall) {
                    Rigidbody brb = burrell[i].GetComponent<Rigidbody>();
                    Vector3 burrellTo = (burrell[i].transform.position - transform.position);
                    burrellTo.Normalize();
                    brb.AddForce(burrellTo * burrellSpeed);
                }

            }
        }



        if((transform.position - player.transform.position).magnitude <= killDistance) {
                Ray ray = new Ray(transform.position, player.transform.position - transform.position);
                RaycastHit[] hits;
                hits = Physics.RaycastAll(ray, killDistance);
                bool wall = false;
                foreach (RaycastHit hit in hits) {
                    if (hit.collider.gameObject.tag == "Wall") {
                        if ((hit.collider.gameObject.transform.position - transform.position).magnitude < (player.transform.position - transform.position).magnitude) {
                            wall = true;
                        }
                    }
                }

                if (!wall) {
                    player.GetComponent<PlayerLife>().Death("Тебя убило взрывом!");
                }

            }
        

        
    }
}
