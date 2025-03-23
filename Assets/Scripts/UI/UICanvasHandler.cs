using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UICanvasHandler : MonoBehaviour
{
    [SerializeField] private GameObject _uiPanels;
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private GameObject _camera;
    [SerializeField] private MovementHandler _movementHandler;
    private InputAction _inventoryAction;

    private void Awake()
    {
        var playerActionMap = inputActions.FindActionMap("Player");
        _inventoryAction = playerActionMap.FindAction("Inventory");
    }

    private void OnEnable()
    {
        _inventoryAction.performed += OnInventoryPanelPerformed;
    }

    private void OnDisable()
    {
        _inventoryAction.performed -= OnInventoryPanelPerformed;
    }
    public void OnInventoryButtonClick()
    {
        _uiPanels.SetActive(!_uiPanels.activeInHierarchy);

        if (_uiPanels.activeInHierarchy)
        {
            _camera.SetActive(false);
            _movementHandler.SetCanMove(false);
        }
        else
        {
            _camera.SetActive(true);
            _movementHandler.SetCanMove(true);
        }
    }

    public void OnExitButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    public void OnInventoryPanelPerformed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _uiPanels.SetActive(!_uiPanels.activeInHierarchy);

            if (_uiPanels.activeInHierarchy)
            {
                _camera.SetActive(false);
                _movementHandler.SetCanMove(false);
            }
            else
            {
                _camera.SetActive(true);
                _movementHandler.SetCanMove(true);
            }
        }
    }
}
