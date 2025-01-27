using UnityEngine;
public class CameraMovement : MonoBehaviour
{
    private Rigidbody RB;
    public float Sensitivity = 0.1f;
    public float Move_Speed = 10f;
    private Vector2 Rotation = Vector2.zero;
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        Rotation.x -= Input.GetAxis("Mouse Y") * Sensitivity;
        Rotation.x = Mathf.Clamp(Rotation.x, -90, 90);
        Rotation.y += Input.GetAxis("Mouse X") * Sensitivity;
        while (Rotation.y > 360)
        {
            Rotation.y -= 360;
        }
        while (Rotation.y <= 0)
        {
            Rotation.y += 360;
        }
        transform.localRotation = Quaternion.Euler(Rotation.x, Rotation.y, 0);

        Vector2 Move_Direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            Move_Direction.x = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Move_Direction.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Move_Direction.y = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Move_Direction.y = -1;
        }
        RB.velocity = ((Quaternion.Euler(0, transform.eulerAngles.y, 0) * Vector3.forward * Move_Direction.x) + (Quaternion.Euler(0, transform.eulerAngles.y, 0) * Vector3.right * Move_Direction.y)) * Move_Speed;

        if (Input.GetKey(KeyCode.Space))
        {
            RB.velocity = new Vector3(RB.velocity.x, Move_Speed, RB.velocity.z);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            RB.velocity = new Vector3(RB.velocity.x, -Move_Speed, RB.velocity.z);
        }
        else
        {
            RB.velocity = new Vector3(RB.velocity.x, 0, RB.velocity.z);
        }
    }
}
