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

    public float raycastDistance = 100f;

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

    [SerializeField] PlayerBoatController BudkaOfControll;

    public int PlayerState = 2;    // 1-ходит
                                   // 2-управляет

    Vector3 positionBeforeTeleport;


    // Настройки приближения
    public float zoomSpeed = 2f;         // Скорость приближения
    public float minFOV = 15f;           // Минимальное значение FOV (максимальное приближение)
    public float maxFOV = 60f;           // Максимальное значение FOV (исходное положение камеры)

    private Camera cam;
    private float targetFOV;


    private void Start()
    {

        cam = Camera.main;
        if (cam != null)
        {
            targetFOV = cam.fieldOfView;
        }

        Cursor.lockState = CursorLockMode.Locked;

        characterController = GetComponent<CharacterController>();
        //shipTransform = transform.parent; // Предполагается, что корабль является родителем


        lastShipPosition = shipTransform.position; // Подумай как переадаптировать.

    }
    void Update()
    {
        switch (PlayerState)
        {
            case 0:
                CameraControll();
                FovChanger();
                //GravityCalk();
                //UpdatePositionByShip();
                break;
            case 1:
                CameraControll();
                Move();
                break;
            default:
                Debug.LogError("Неизвесный тип игрока");
                break;
        }
        ButtonCheker();
        rayCastAction();
        // Move();
        // CameraControll();  
    }

    void ButtonCheker()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerState == 1)
        {
            PlayerState = 0;
            positionBeforeTeleport = transform.position - shipTransform.position;
            transform.SetParent(shipTransform);
            transform.position = BudkaOfControll.Intial();
            //lastShipPosition = transform.position;
        }
        if (Input.GetKeyDown(KeyCode.F) && PlayerState == 0)
        {
            PlayerState = 1;
            transform.SetParent(null);
            transform.position = shipTransform.position + positionBeforeTeleport;
            BudkaOfControll.Stopp();
        }
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

    void rayCastAction()
    {
        // Проверяем нажатие кнопки мыши (левой кнопки)
        if (Input.GetMouseButtonDown(0))
        {
            // Получаем камеру, привязанную к объекту
            Camera cam = Camera.main;
            if (cam != null)
            {
                // Создаем рейкаст от центра камеры
                Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, raycastDistance))
                {
                    // Проверяем наличие метода ReycastOver() у объекта, в который попал рейкаст
                    var target = hit.transform.GetComponent<MonoBehaviour>();
                    if (target != null)
                    {
                        // Вызываем метод ReycastOver(), если он существует
                        var method = target.GetType().GetMethod("ReycastOver");
                        if (method != null)
                        {
                            method.Invoke(target, null);
                        }
                    }
                }
            }
        }
    }

    void FovChanger()
    {
        if (cam != null)
        {
            // Проверяем, зажата ли правая кнопка мыши
            if (Input.GetMouseButton(1))
            {
                // Уменьшаем FOV для приближения
                targetFOV -= zoomSpeed * Time.deltaTime;
            }
            else
            {
                // Возвращаем FOV к исходному значению
                targetFOV += zoomSpeed * Time.deltaTime;
            }

            // Ограничиваем FOV в пределах minFOV и maxFOV
            targetFOV = Mathf.Clamp(targetFOV, minFOV, maxFOV);

            // Применяем новое значение FOV к камере
            cam.fieldOfView = targetFOV;
        }

    }
}
