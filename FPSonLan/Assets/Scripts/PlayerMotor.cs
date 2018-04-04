using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;


    private Rigidbody rb;

    private void Start()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        this.PerformMovement();
        this.PerformRotation();
    }

    internal void Move(Vector3 pVelocity)
    {
        this.velocity = pVelocity;
    }
    internal void Rotate(Vector3 pRotation)
    {
        this.rotation = pRotation;
    }

    internal void RotateCamera(Vector3 pCameraRotation)
    {
        this.cameraRotation = pCameraRotation;
    }

    private void PerformMovement()
    {
        if (this.velocity != Vector3.zero)
        {
            this.rb.MovePosition(this.rb.position + this.velocity * Time.deltaTime);
        }
    }
    private void PerformRotation()
    {
        if (this.rotation != Vector3.zero)
        {
            this.rb.MoveRotation(this.rb.rotation * Quaternion.Euler(this.rotation));
            Debug.Log("Trying to turn arround...");
        }
        if(this.cam != null)
        {
            cam.transform.Rotate(this.cameraRotation * -1);
        }
    }
}
