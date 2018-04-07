using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float cameraRotationLimit = 85f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 thrusterForce = Vector3.zero;

    private float cameraRotation = 0f;
    private float currentCameraRotation = 0f;


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

    internal void RotateCamera(float pCameraRotation)
    {
        this.cameraRotation = pCameraRotation;
    }

    internal void ApplyThruster(Vector3 pThusterForce)
    {
        this.thrusterForce = pThusterForce;
    }

    private void PerformMovement()
    {
        if (this.velocity != Vector3.zero)
        {
            this.rb.MovePosition(this.rb.position + this.velocity * Time.deltaTime);
        }
        if(this.thrusterForce != Vector3.zero)
        {
            this.rb.AddForce(this.thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }
    private void PerformRotation()
    {
        if (this.rotation != Vector3.zero)
        {
            this.rb.MoveRotation(this.rb.rotation * Quaternion.Euler(this.rotation));
        }
        if(this.cam != null)
        {
            this.currentCameraRotation -= this.cameraRotation;
            this.currentCameraRotation = Mathf.Clamp(this.currentCameraRotation, (this.cameraRotationLimit * -1), this.cameraRotationLimit);

            this.cam.transform.localEulerAngles = new Vector3(this.currentCameraRotation, 0f, 0f);
        }
    }
}
