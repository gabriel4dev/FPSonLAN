using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float mouseSensitivity = 3;
    [SerializeField]
    private float thrusterForce = 1000f;

    

    [Header("Spring Settings")]
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;

    private PlayerMotor playerMotor;
    private ConfigurableJoint joint;
    private Animator anim;

    private void Start()
    {
        this.playerMotor = this.GetComponent<PlayerMotor>();
        this.joint = this.GetComponent<ConfigurableJoint>();
        this.anim = this.GetComponent<Animator>();

        this.SetJointSettings(this.jointSpring);
    }

    private void Update()
    {
        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;

        Vector3 velocity = (movHorizontal + movVertical) * speed;

        this.anim.SetFloat("ForwardVelocity", zMov);

        this.playerMotor.Move(velocity);

        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 vRotation = new Vector3(0f, yRot, 0f).normalized * mouseSensitivity;
        this.playerMotor.Rotate(vRotation);

        float xRot = Input.GetAxisRaw("Mouse Y");

        float vCameraRotation = xRot * mouseSensitivity;
        this.playerMotor.RotateCamera(vCameraRotation);

        Vector3 vThusterForce = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            vThusterForce = Vector3.up * this.thrusterForce;
            this.SetJointSettings(0f);
        }
        else
        {
            this.SetJointSettings(this.jointSpring);
        }

        this.playerMotor.ApplyThruster(vThusterForce);
    }

    private void SetJointSettings(float pJointSpring)
    {
        this.joint.yDrive = new JointDrive {
            positionSpring = pJointSpring,
            maximumForce = this.jointMaxForce
        };

    }
}
