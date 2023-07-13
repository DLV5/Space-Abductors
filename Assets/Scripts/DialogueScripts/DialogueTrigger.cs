using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] DialogueArray;
    private bool _isStarted = false;

    private void Start()
    {
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        if (_isStarted)
        {
            if(!DialogueManager.Instance.DisplayNextSentence() && DialogueManager.Instance.Index + 1 < DialogueArray.Length)
            {
                ++DialogueManager.Instance.Index;
                _isStarted = false;
            }
            return;
        }
        _isStarted = true;
        DialogueManager.Instance.StartDialogue(DialogueArray[DialogueManager.Instance.Index]);
    }
}
