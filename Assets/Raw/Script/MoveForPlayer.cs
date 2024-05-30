using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForPlayer : MonoBehaviour
{

    private CursorLockMode _wantedMode;

    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public Transform cameraTransform;
    public bool canMove = true;

    [SerializeField]
    [Tooltip("Sensitivity of mouse rotation")]
    private float _mouseSense = 1.8f;


    private void OnEnable()
    {
         _wantedMode = CursorLockMode.Locked;
    }


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        SetCursorState();

        if (Cursor.visible)
            return;


        if (canMove)
        {
            MovePlayer();
            RotateCamera();
        }
    }

    void MovePlayer()
    {
        float moveDirectionX = Input.GetAxis("Horizontal");
        float moveDirectionZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveDirectionX + transform.forward * moveDirectionZ;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float speed = isRunning ? runSpeed : walkSpeed;

        transform.Translate(move * speed * Time.deltaTime, Space.World);
    }

    void RotateCamera()
    {
            transform.rotation *= Quaternion.AngleAxis(
                -Input.GetAxis("Mouse Y") * _mouseSense,
                Vector3.right
            );

        // Paw
        transform.rotation = Quaternion.Euler(
            transform.eulerAngles.x,
            transform.eulerAngles.y + Input.GetAxis("Mouse X") * _mouseSense,
            transform.eulerAngles.z
        );
    }

    private void SetCursorState()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = _wantedMode = CursorLockMode.None;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _wantedMode = CursorLockMode.Locked;
        }

        // Apply cursor state
        Cursor.lockState = _wantedMode;
        // Hide cursor when locking
        Cursor.visible = (CursorLockMode.Locked != _wantedMode);
    }

}
