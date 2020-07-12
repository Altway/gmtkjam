using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float moveSpeed;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed * Time.deltaTime;
       transform.position = new Vector3(Mathf.Clamp(transform.position.x, -12.8f, -3f), transform.position.y, Mathf.Clamp(transform.position.z, -4.6f, 12f));

    }
}
