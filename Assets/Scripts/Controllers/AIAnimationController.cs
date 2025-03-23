using UnityEngine;
using UnityEngine.AI;

public class AIAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _navmeshAgent;

    private AnimState _currentState = AnimState.iddle;
    private bool _isIddle = true;


    private void Update()
    {
        if (!_navmeshAgent.isStopped)
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
    }
}
