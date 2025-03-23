using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private MovementHandler _movementHandler;

    private AnimState _currentState = AnimState.iddle;
    private bool _isIddle = true;


    private void Update()
    {
        if (_movementHandler.GetIsMoving())
        {
            _currentState = AnimState.isWalking;
            _isIddle = false;
        }
        else
        {
            if (_isIddle == false)
            {
                _isIddle = true;
                _currentState = AnimState.iddle;
            }
        }

        HandleAnimation();
    }

    private void HandleAnimation()
    {
        if (_currentState == AnimState.isWalking)
        {
            _animator.SetBool(_currentState.ToString(), true);
            return;
        }
        _animator.SetBool(AnimState.isWalking.ToString(), false);
        _animator.SetTrigger(_currentState.ToString());
    }
}
