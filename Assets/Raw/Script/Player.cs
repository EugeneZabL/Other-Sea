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

    public float raycastDistance = 5f;

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

    int PlayerState = 1;            // 1-�����
                                   // 2-���������

    Vector3 positionBeforeTeleport;


    // ��������� �����������
    public float zoomSpeed = 2f;         // �������� �����������
    public float minFOV = 15f;           // ����������� �������� FOV (������������ �����������)
    public float maxFOV = 60f;           // ������������ �������� FOV (�������� ��������� ������)

    private Camera cam;
    private float targetFOV;


    GameObject lastPlayerPos;

    private void Start()
    {
        lastPlayerPos = new GameObject();
        lastPlayerPos.transform.SetParent(shipTransform);


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
                ExitButton();
                //GravityCalk();
                //UpdatePositionByShip();
                break;
            case 1:
                CameraControll();
                Move();
                GravityCalk();
                UpdatePositionByShip();
                break;
            case 2:
                CameraControll();
                Move();
                GravityCalk();
                break;
            default:
                Debug.LogError("���������� ��� ������");
                break;
        }


        //ButtonCheker();
        rayCastAction();


        // Move();
        // CameraControll();  
    }

    void ExitButton()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerState = 1;
            transform.SetParent(null);
            transform.position = lastPlayerPos.transform.position;
            BudkaOfControll.Stopp();
            transform.rotation = Quaternion.Euler(shipTransform.rotation.x, shipTransform.rotation.y, 0);
            lastShipPosition = shipTransform.position;
        }
    }

    void ButtonCheker()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerState == 1)
        {
            PlayerState = 0;
            lastPlayerPos.transform.position = transform.position;
            transform.SetParent(shipTransform);
            //transform.rotation = Quaternion.Euler(shipTransform.rotation.x, shipTransform.rotation.y, 0);
            transform.position = BudkaOfControll.Intial();
        }
        if (Input.GetKeyDown(KeyCode.F) && PlayerState == 0)
        {
            PlayerState = 1;
            transform.SetParent(null);
            transform.position = lastPlayerPos.transform.position;
            BudkaOfControll.Stopp();
            transform.rotation = Quaternion.Euler(shipTransform.rotation.x,shipTransform.rotation.y,0);
            lastShipPosition = shipTransform.position;
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
            velocity.y = -0.5f; // ��������� �������� ��� ������������ ��������� �� �����
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

    private GameObject currentLookedObject = null;

    

    void rayCastAction()
    {

        /*
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
        */

        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            GameObject hitObject = hit.transform.gameObject;

            if (currentLookedObject != hitObject)
            {
                // ���� ����� ������� �� ����� ������
                if (currentLookedObject != null)
                {
                    // ������� ������� "����� ������ �� ������� �� ������� ������"
                    CallSOul(hit, "PlayerLookOff");
                }

                // ������� ������� "����� ������� �� ����� ������"
                CallSOul(hit, "PlayerLookOn");
                currentLookedObject = hitObject;
            }

            if(Input.GetMouseButtonDown(0))
            {
                CallSOul(hit, "ReycastOver");
            }
        }
        else
        {
            // ���� ������� �� �������� �� � ���� ������
            if (currentLookedObject != null)
            {
                // ������� ������� "����� ������ �� ������� �� ������� ������"
                CallSOul(currentLookedObject,"PlayerLookOff");
                currentLookedObject = null;
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


    void CallSOul(RaycastHit hit ,string nameMethod)
    {
        var target = hit.transform.GetComponent<MonoBehaviour>();
        if (target != null)
        {
            // �������� ����� ReycastOver(), ���� �� ����������
            var method = target.GetType().GetMethod(nameMethod);
            if (method != null)
            {
                method.Invoke(target, null);
            }
        }
    }

    void CallSOul(GameObject gameObject, string nameMethod)
    {
        var target = gameObject.transform.GetComponent<MonoBehaviour>();
        if (target != null)
        {
            // �������� ����� ReycastOver(), ���� �� ����������
            var method = target.GetType().GetMethod(nameMethod);
            if (method != null)
            {
                method.Invoke(target, null);
            }
        }
    }

    public void ResetLastPlayerPos(int status)
    {
        lastPlayerPos.transform.position = transform.position;
        PlayerState = status;
    }

    public void ReturnToShip()
    {
        PlayerState = 1;
        transform.SetParent(null);
        transform.position = lastPlayerPos.transform.position;
        transform.rotation = Quaternion.Euler(shipTransform.rotation.x, shipTransform.rotation.y, 0);
        lastShipPosition = shipTransform.position;
    }
}
