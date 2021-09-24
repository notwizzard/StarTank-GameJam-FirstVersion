using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float forwardSpeed, backSpeed, rotSpeed, maxVelocity, superMaxVelocity;
    [SerializeField]
    private Transform moveTo;
    [SerializeField]
    private float timeToGetSuper, superDuration;
    [SerializeField]
    Image nitro;

    private Rigidbody rb;
    private bool isSuper = false;
    private float superState = 0f;
    private float nowMaxVelocity;
    private AudioSource audio;

    private void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        float delta = PlayerPrefs.GetFloat("speed", 1f);
        superMaxVelocity += delta;
        forwardSpeed += delta;
        backSpeed += delta;
        maxVelocity += delta;

        rb = gameObject.GetComponent<Rigidbody>();
        nowMaxVelocity = maxVelocity;
    }

    private void Update()
    {

        // movement
        if (Input.GetAxis("Vertical") > 0) {
            Vector3 to = moveTo.transform.position - transform.position;
            to.y = 0f;
            to.Normalize();
            if (Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2)) < nowMaxVelocity) rb.AddForce(to * forwardSpeed * Time.deltaTime);
        }
        if (Input.GetAxis("Vertical") < 0) {
            Vector3 to = transform.position - moveTo.transform.position;
            to.y = 0f;
            to.Normalize();
            if (Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2)) < nowMaxVelocity) rb.AddForce(to * backSpeed * Time.deltaTime);
        }

        // rotate
        if (Input.GetKey(KeyCode.A)) transform.Rotate(new Vector3(0, -rotSpeed, 0) * Time.deltaTime);
        if (Input.GetKey(KeyCode.D)) transform.Rotate(new Vector3(0, rotSpeed, 0) * Time.deltaTime);

        // super
        if (Input.GetKeyDown(KeyCode.Space)) {
            isSuper = true;
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            isSuper = false;
            nowMaxVelocity = maxVelocity;
        }
        if (!isSuper) {
            superState += Time.deltaTime / timeToGetSuper;
            superState = Mathf.Clamp(superState, 0f, 1f);
        }
        if (isSuper) {
            superState -= Time.deltaTime / superDuration;
            superState = Mathf.Clamp(superState, 0f, 1f);
            if (superState > 0.01f) {
                nowMaxVelocity = superMaxVelocity;
            }
            else {
                nowMaxVelocity = maxVelocity;
            }
        }

        nitro.fillAmount = superState;


        audio.pitch = Mathf.Clamp((Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) / maxVelocity, 0.7f, 1.3f);
    }
}
