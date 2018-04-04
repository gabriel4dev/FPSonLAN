using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float mouseSensitivity = 3;

    private PlayerMotor playerMotor;

    private void Start()
    {
        this.playerMotor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;

        Vector3 velocity = (movHorizontal + movVertical).normalized * speed;

        this.playerMotor.Move(velocity);

        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 vRotation = new Vector3(0f, yRot, 0f).normalized * mouseSensitivity;
        this.playerMotor.Rotate(vRotation);

        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 vCameraRotation = new Vector3(xRot, 0f, 0f).normalized * mouseSensitivity;
        this.playerMotor.RotateCamera(vCameraRotation);
    }
}
