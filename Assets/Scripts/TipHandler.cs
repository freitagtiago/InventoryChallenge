using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TipHandler : MonoBehaviour
{
    [SerializeField] private List<string> _tipList = new List<string>();
    [SerializeField] private float _timeToChangeTip = 5f;
    [SerializeField] private TextMeshProUGUI _tipText;
    private int _currentIndex = 0;

    private void Start()
    {
        StartCoroutine(AdvanceTip());
    }

    private IEnumerator AdvanceTip()
    {
        _tipText.text = _tipList[_currentIndex];
        yield return new WaitForSecondsRealtime(_timeToChangeTip);
        _currentIndex++;
        if(_currentIndex >= _tipList.Count)
        {
            _currentIndex = 0;
        }
        StartCoroutine(AdvanceTip());
    }

}
