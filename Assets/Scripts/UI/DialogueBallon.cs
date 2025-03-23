using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBallon : MonoBehaviour
{
    [SerializeField] private List<string> _sentences = new List<string>();
    [SerializeField] private GameObject _rootDialogue;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    public void EnableDialogue()
    {
        int sentenceIndex = Random.Range(0, _sentences.Count);
        _dialogueText.text = _sentences[sentenceIndex];
        _rootDialogue.SetActive(true);
    }

    public void DisableDialogue()
    {
        _rootDialogue.SetActive(false);
        _dialogueText.text = string.Empty;
    }
    void Update()
    {
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
