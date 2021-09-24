using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject player, deadParticle, dulo, duloFrom, enemyBullet;
    [SerializeField]
    private float maxHP, speed, minDistance, range;
    [SerializeField]
    private GameObject hpObject;
    [SerializeField]
    private Image hpImage; 
    [SerializeField]
    AudioClip clip;

    private AudioSource audio;


    private Transform playerPosition;
    private bool goSee = false;
    public float HP;

    System.Random rnd = new System.Random();

    bool started = false;

    NavMeshAgent agent;

    void Start()
    {
        HP = maxHP;
        agent = gameObject.GetComponent<NavMeshAgent>();
        StartCoroutine("AgentUpdate");
        audio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerVisible() && !started) {
            started = true;
            StartCoroutine("Fire");
        }

        if (goSee) transform.LookAt(player.transform);
        hpObject.transform.LookAt(Camera.main.transform);

        hpImage.fillAmount = HP / maxHP;

        if (HP <= 0) Dead();

    }

    void SmoothLookAt(Vector3 newDirection){ 
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(newDirection), Time.deltaTime); 
    }

    public void Dead () {
        Destroy(Instantiate(deadParticle, transform.position, transform.rotation), 5f);
        Destroy(gameObject);
    }

    IEnumerator AgentUpdate () {
        playerPosition = player.transform;
        
        
        if (isPlayerVisible() && (transform.position - playerPosition.position).magnitude < minDistance) {
            goSee = true;
            agent.destination = transform.position;
        }
        else {
            agent.destination = playerPosition.position;
            goSee = false;
        }


        // + new Vector3((Random.insideUnitCircle * 5).x, 0, (Random.insideUnitCircle * 5).y)
        yield return new WaitForSeconds(1f);
        StartCoroutine("AgentUpdate");
    }

    IEnumerator Fire () {

        
        if (isPlayerVisible()) {
            audio.PlayOneShot(clip);
            GameObject bul = Instantiate(enemyBullet, dulo.transform.position, dulo.transform.rotation);
            Rigidbody rb = bul.GetComponent<Rigidbody>();
            Vector3 to = dulo.transform.position - duloFrom.transform.position;
            to = to + new Vector3((Random.insideUnitCircle * range).x, 0, (Random.insideUnitCircle * range).y);
            to.Normalize();
            rb.AddForce(to * speed); 
        }


        // + new Vector3((Random.insideUnitCircle * 5).x, 0, (Random.insideUnitCircle * 5).y)

        
        float next = (float)rnd.Next(100, 501) / 100;
        yield return new WaitForSeconds(next);
        StartCoroutine("Fire");
    }

    private bool isPlayerVisible () {
        Ray ray = new Ray(transform.position, player.transform.position - transform.position);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.collider.gameObject.tag == "Player") {
            return true;
        }
        return false;
    }
    
}
