using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    private Rigidbody RB;
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        transform.localRotation = Quaternion.Euler(Mathf.Clamp(transform.eulerAngles.x + (Input.GetAxis("Mouse Y") * -1), -90f, 90f), transform.eulerAngles.y + Input.GetAxis("Mouse X"), 0);
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
        RB.velocity = ((transform.forward * Move_Direction.x) + (transform.right * Move_Direction.y)) * 15;
        if (Input.GetKey(KeyCode.Space))
        {
            RB.velocity = new Vector3(RB.velocity.x, 15, RB.velocity.z);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            RB.velocity = new Vector3(RB.velocity.x, -15, RB.velocity.z);
        }
        else
        {
            RB.velocity = new Vector3(RB.velocity.x, 0, RB.velocity.z);
        }
    }
}
