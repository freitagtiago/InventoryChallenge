using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMoverController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _pathToFollow;
    [SerializeField] private float _distanceTolerance = 1f;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxWaitTime;
    [SerializeField] private DialogueBallon dialogueBallon;
    
    private NavMeshAgent _naveMeshAgent;
    private int _pathPointIndex;
    private Vector3 _destination;
    private float _timeSinceArrived = 0;
    private bool _isWaiting = false;
    private float _intervalForCheckPosition = 2f;

    private void Awake()
    {
        _naveMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetNextPathPoint();
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, _destination) < _distanceTolerance)
        {
            if (!_isWaiting)
            {
                _isWaiting = true;
                _timeSinceArrived = 0;
                StartCoroutine(WaitBeforeNextPathPoint());
            }
            else
            {
                _timeSinceArrived += Time.deltaTime;
            }
        }
        else
        {
            _isWaiting = false;
            _timeSinceArrived = 0;
        }
    }

    private IEnumerator WaitBeforeNextPathPoint()
    {
        _naveMeshAgent.destination = transform.position;
        _naveMeshAgent.speed = 0f;
        _naveMeshAgent.isStopped = true;
        yield return new WaitForSecondsRealtime(Random.Range(0.5f, _maxWaitTime));
        SetNextPathPoint();
    }

    private void SetNextPathPoint()
    {
        if (_pathToFollow.Count == 0) 
        { 
            return;
        }

        _destination = _pathToFollow[_pathPointIndex].transform.position;
        MoveTo(_destination);
        _pathPointIndex++;
        if(_pathPointIndex >= _pathToFollow.Count)
        {
            _pathPointIndex = 0;
        }
    }

    public void MoveTo(Vector3 destination)
    {
        _naveMeshAgent.speed = _speed;
        _naveMeshAgent.destination = destination;
        _naveMeshAgent.isStopped = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            dialogueBallon.EnableDialogue();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            dialogueBallon.DisableDialogue();
        }
    }
}
