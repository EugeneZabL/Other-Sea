using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _rotateSpeed = 2.0f;
    [SerializeField] private float _minYAngle = -45.0f; // ����������� ���� ������� ������ (�����)
    [SerializeField] private float _maxYAngle = 45.0f;  // ������������ ���� ������� ������ (����)

    public float groundDistance = 0.4f; // ���������� ��� �������� �����
    public LayerMask groundMask; // ����� ���� ��� ����������� �����

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

    public int PlayerState = 2;    // 1-�����
                                   // 2-���������

    Vector3 positionBeforeTeleport;


    // ��������� �����������
    public float zoomSpeed = 2f;         // �������� �����������
    public float minFOV = 15f;           // ����������� �������� FOV (������������ �����������)
    public float maxFOV = 60f;           // ������������ �������� FOV (�������� ��������� ������)

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
        //shipTransform = transform.parent; // ��������������, ��� ������� �������� ���������


        lastShipPosition = shipTransform.position; // ������� ��� ����������������.

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
                Debug.LogError("���������� ��� ������");
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
        // �������� ������� ���� �������� �� ���������
        float mouseY = -Input.GetAxis("Mouse Y") * _rotateSpeed;
        float mouseX = Input.GetAxis("Mouse X") * _rotateSpeed;

        // ��������� ������� ���� �� ���������, �� ������������ ���
        _currentYRotation = Mathf.Clamp(_currentYRotation + mouseY, _minYAngle, _maxYAngle);

        // ��������� �������� �� ���������
        Quaternion verticalRotation = Quaternion.Euler(_currentYRotation, 0, 0);
        Quaternion horizontalRotation = Quaternion.Euler(0, mouseX, 0);

        // ��������� �������� ������
        _playerCamera.transform.localRotation = verticalRotation;
        _playerCamera.transform.parent.rotation *= horizontalRotation;
    }


    void Move()
    {
        // ���������, ��������� �� ����� �� ����� � ������� Raycast
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // ��������� �������� ��� ������������ ��������� �� �����
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
        //��������� ������� ������ ������������ �������� �������
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
        // ��������� ������� ������ ���� (����� ������)
        if (Input.GetMouseButtonDown(0))
        {
            // �������� ������, ����������� � �������
            Camera cam = Camera.main;
            if (cam != null)
            {
                // ������� ������� �� ������ ������
                Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, raycastDistance))
                {
                    // ��������� ������� ������ ReycastOver() � �������, � ������� ����� �������
                    var target = hit.transform.GetComponent<MonoBehaviour>();
                    if (target != null)
                    {
                        // �������� ����� ReycastOver(), ���� �� ����������
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
            // ���������, ������ �� ������ ������ ����
            if (Input.GetMouseButton(1))
            {
                // ��������� FOV ��� �����������
                targetFOV -= zoomSpeed * Time.deltaTime;
            }
            else
            {
                // ���������� FOV � ��������� ��������
                targetFOV += zoomSpeed * Time.deltaTime;
            }

            // ������������ FOV � �������� minFOV � maxFOV
            targetFOV = Mathf.Clamp(targetFOV, minFOV, maxFOV);

            // ��������� ����� �������� FOV � ������
            cam.fieldOfView = targetFOV;
        }

    }
}
