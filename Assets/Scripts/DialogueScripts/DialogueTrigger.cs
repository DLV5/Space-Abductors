using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue DialogueObject;
    private bool _isStarted = false;

    private void Start()
    {
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        if (_isStarted)
        {
            DialogueManager.Instance.DisplayNextSentence();
            return;
        }
        _isStarted = true;
        DialogueManager.Instance.StartDialogue(DialogueObject);
    }
}
