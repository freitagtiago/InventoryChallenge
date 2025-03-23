using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Canvas rootCanvas;

    private void Start()
    {
        _healthBar.maxValue = _playerHealth.GetMaxHealthPoints();
    }
    void Update()
    {
        if(_playerHealth != null)
        {
            _healthBar.value = _playerHealth.GetHealthPoints();
        }

        FaceCamera();
    }

    private void FaceCamera()
    {
        Vector3 directionToCamera = Camera.main.transform.position - transform.position;
        directionToCamera.y = 0;
        if (directionToCamera != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(-directionToCamera);
        }
    }
}

