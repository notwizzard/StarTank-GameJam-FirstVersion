using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotate : MonoBehaviour
{
    [SerializeField]
    private float rotSpeed;
    [SerializeField]
    private float horViewRange, vertViewRange;
    [SerializeField]
    private Transform body;

    float xRot = 0f, yRot = 0f;

    private void Start()
    {
        rotSpeed = PlayerPrefs.GetFloat("sensetivity", rotSpeed);
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        xRot += -Input.GetAxis("Mouse Y") * rotSpeed;
        yRot += Input.GetAxis("Mouse X") * rotSpeed;
        
        //yRot = Mathf.Clamp (yRot, -horViewRange + (body.rotation.y * 180), horViewRange + (body.rotation.y * 180));
        xRot = Mathf.Clamp (xRot, -vertViewRange, vertViewRange);
       // transform.Rotate(0f, yRot * rotSpeed * Time.deltaTime, xRot * rotSpeed * Time.deltaTime);
        //yRot = Mathf.Clamp(yRot * rotSpeed, minAngelY, maxAngelY);
        //xRot = Mathf.Clamp(-xRot * rotSpeed, minAngelX, maxAngelX);
        transform.rotation = Quaternion.Euler(0f, yRot, xRot);  
    }
}
