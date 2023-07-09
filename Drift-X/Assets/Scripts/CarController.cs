using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float moveSpeed = 50f;
    public float maxSpeed = 15;
    public float drag = 0.98f;
    public float steerAngle = 20;
    public float traction = 1;

    private Vector3 moveForce;

    // Update is called once per frame
    void Update()
    {
        //Moving codes
        moveForce += transform.forward * Input.GetAxis("Vertical") *moveSpeed * Time.deltaTime;
        transform.position += moveForce * Time.deltaTime;

        //Steering code
        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerInput * moveForce.magnitude * steerAngle * Time.deltaTime);

        //Drag codes
        moveForce *= drag;
        moveForce = Vector3.ClampMagnitude(moveForce, maxSpeed);

        //Traction
        Debug.DrawRay(transform.position, moveForce.normalized * 3);
        Debug.DrawRay(transform.position, transform.forward * 3, Color.red);
        moveForce = Vector3.Lerp(moveForce.normalized, transform.forward, traction * Time.deltaTime) * moveForce.magnitude;
    }
}
