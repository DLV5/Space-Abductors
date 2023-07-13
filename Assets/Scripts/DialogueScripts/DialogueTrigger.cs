using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

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
            if (DialogueManager.Instance.DisplayNextSentence())
                return;
            else
            {
                if (DialogueManager.Instance.Index + 1 < DialogueArray.Length)
                {
                    ++DialogueManager.Instance.Index;
                    _isStarted = false;
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            return;
        }
        _isStarted = true;
        DialogueManager.Instance.StartDialogue(DialogueArray[DialogueManager.Instance.Index]);
    }
}
