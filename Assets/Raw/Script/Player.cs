using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _rotateSpeed = 2.0f;
    [SerializeField] private float _minYAngle = -45.0f; // Минимальный угол наклона камеры (вверх)
    [SerializeField] private float _maxYAngle = 45.0f;  // Максимальный угол наклона камеры (вниз)

    public float groundDistance = 0.4f; // Расстояние для проверки земли
    public LayerMask groundMask; // Маска слоя для определения земли

    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;

    private float _currentYRotation = 0.0f;

    [SerializeField] private Transform shipTransform;
    private Vector3 lastShipPosition;

    [SerializeField] GameObject BudkaOfControll;
    public int PlayerState = 2;    // 0 - Ничего
                            // 1 - Камера
                            // 2 - Камера - относительно корабля
                            // 3 - Камера + Передвижение
                            // 4 - Камера + Передвижение - относительно корабля

    public bool onShip = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        characterController = GetComponent<CharacterController>();
        //shipTransform = transform.parent; // Предполагается, что корабль является родителем
        
        
        lastShipPosition = shipTransform.position; // Подумай как переадаптировать.
    }
    void Update()
    {
        switch(PlayerState)
        {
            case 0:
                break;
            case 1:
                CameraControll();
                GravityCalk();
                break;
            case 2:
                CameraControll();
                UpdatePositionByShip();
                GravityCalk();
                break;
            case 3:
                CameraControll();
                Move();
                break;
            case 4:
                CameraControll();
                Move();
                break;
            default:
                Debug.LogError("Неизвесный тип игрока");
                break;
        }

       // Move();
       // CameraControll();  
    }

    void FixedUpdate()
    {
        // Для корректного определения isGrounded выполняем проверку в FixedUpdate
        isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void ControllerTest()
    {

    }

    void CameraControll()
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


    void Move()
    {
        // Проверяем, находится ли игрок на земле с помощью Raycast
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Небольшое значение для стабилизации персонажа на земле
        }

        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        characterController.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        GravityCalk();
        UpdatePositionByShip();

    }

    void UpdatePositionByShip()
    {
        //Обновляем позицию игрока относительно движения корабля
        Vector3 shipMovement = shipTransform.position - lastShipPosition;
        characterController.Move(shipMovement);
        lastShipPosition = shipTransform.position;
    }

    void GravityCalk()
    {

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        if (groundMask != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, groundDistance);
        }
    }

}
