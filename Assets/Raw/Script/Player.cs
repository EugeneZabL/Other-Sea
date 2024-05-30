using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _rotateSpeed = 2.0f;
    [SerializeField] private float _minYAngle = -45.0f; // Минимальный угол наклона камеры (вверх)
    [SerializeField] private float _maxYAngle = 45.0f;  // Максимальный угол наклона камеры (вниз)

    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    //[SerializeField] private Transform shipTransform;

    private Rigidbody rb;
    private bool isGrounded;

    private float _currentYRotation = 0.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    void Update()
    {
        Move();
        Jump();
        MouseRotate();
    }

    void MouseRotate()
    {
        // Получаем текущий угол поворота по вертикали
        float mouseY = -Input.GetAxis("Mouse Y") * _rotateSpeed;
        float mouseX = Input.GetAxis("Mouse X") * _rotateSpeed; 

        // Обновляем текущий угол по вертикали, но ограничиваем его
        _currentYRotation = Mathf.Clamp(_currentYRotation + mouseY, _minYAngle, _maxYAngle);

        // Применяем вращение по вертикали
        Quaternion verticalRotation = Quaternion.Euler(_currentYRotation, 0, 0);
        Quaternion horizontalRotation = Quaternion.Euler(0, mouseX, 0);

        // Обновляем вращение камеры
        _playerCamera.transform.localRotation = verticalRotation;
        _playerCamera.transform.parent.rotation *= horizontalRotation;
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0; // Чтобы не было движения по оси Y

        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        Vector3 velocity = movement.normalized * speed;

         // Перемещаем персонажа в локальных координатах
         Vector3 localMovement = transform.localPosition + velocity * Time.deltaTime;
         rb.MovePosition(transform.parent.TransformPoint(localMovement));
    }

    private void Jump()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
