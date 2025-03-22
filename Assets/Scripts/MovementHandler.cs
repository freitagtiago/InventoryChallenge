using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementHandler : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 0.05f;

    private bool _canMove = true;
    private Camera _camera;

    private CharacterController _charController;
    private InputAction _moveAction;

    private bool _isMoving = false;

    private void Awake()
    {
        _camera = Camera.main;
    }
    void Start()
    {
        _moveAction = InputHandler.Instance.GetInputAction("Player", "Move");
    }

    void Update()
    {
        if (_canMove)
        {
            Move();
        }
    }

    private void Move()
    {
        if (InputHandler.Instance.GetCurrentActionMapName() != "Player")
        {
            _isMoving = false;
            return;
        }

        Vector2 input = _moveAction.ReadValue<Vector2>();

        if (input == Vector2.zero)
        {
            _isMoving = false;
            return;
        }

        _isMoving = true;
        float targetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _rotationSpeed, 0.12f);
        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

        transform.position += targetDirection * Time.deltaTime * _moveSpeed;
    }

    public void SetCanMove(bool value)
    {
        _canMove = value;
    }

    public bool GetIsMoving()
    {
        return _isMoving;
    }
}
