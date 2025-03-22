using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance;
    private PlayerInput _playerInput;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        _playerInput = GetComponent<PlayerInput>();
    }

    public string GetCurrentActionMapName()
    {
        return _playerInput.currentActionMap.name;
    }

    public InputAction GetInputAction(string mapName, string inputActionName)
    {
        InputAction action = _playerInput.actions.FindActionMap(mapName)?.FindAction(inputActionName);

        return action;
    }

    public void SwitchCurrentActionMap(string mapName)
    {
        _playerInput.SwitchCurrentActionMap(mapName);
    }


}
