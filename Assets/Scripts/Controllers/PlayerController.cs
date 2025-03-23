using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    struct CursorMapping
    {
        public CursorType type;
        public Texture2D texture;
        public Vector2 hotspot;
    }

    private Health _health;
    private bool _isDraggingUi = false;

    [SerializeField] private CursorMapping[] _cursorMappings = null;
    [SerializeField] private float _maxNavMeshProjection = 1f;
    [SerializeField] private Transform _rightHand;
    [SerializeField] private Transform _leftHand;

    private PlayerInput _playerInput;
    private InputAction _clickAction;
    private InputAction _pointAction;

    void Awake()
    {
        _health = GetComponent<Health>();
        _playerInput = GetComponent<PlayerInput>();
        _clickAction = _playerInput.actions["Click"];
        _pointAction = _playerInput.actions["Point"];
    }

    void Update()
    {
        if (InteractWithUI())
        { 
            return; 
        }
        if (_health.IsDead())
        {
            SetCursor(CursorType.None);
            return;
        }
        if (InteractWithComponent()) 
        { 
            return; 
        }
        SetCursor(CursorType.None);
    }

    private bool InteractWithComponent()
    {
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay(),15f);
        foreach (RaycastHit hit in hits)
        {
            IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
            foreach (IRaycastable raycastable in raycastables)
            {
                if (raycastable.HandleRaycast(this))
                {
                    SetCursor(raycastable.GetCursorType());
                    return true;
                }
            }
        }
        return false;
    }

    private bool InteractWithUI()
    {
        if (_clickAction.triggered 
            && _clickAction.ReadValue<float>() == 0)
        {
            _isDraggingUi = false;
        }
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (_clickAction.triggered && _clickAction.ReadValue<float>() > 0)
            {
                _isDraggingUi = true;
            }

            SetCursor(CursorType.UI);
            return true;
        }

        if (_isDraggingUi)
        {
            return true;
        }
        return false;
    }

    private void SetCursor(CursorType type)
    {
        CursorMapping mapping = GetCursorType(type);
        Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
    }

    private CursorMapping GetCursorType(CursorType type)
    {
        foreach (CursorMapping mapping in _cursorMappings)
        {
            if (mapping.type == type)
            {
                return mapping;
            }
        }
        return _cursorMappings[0];
    }

    private static Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
