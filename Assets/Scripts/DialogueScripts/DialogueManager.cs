using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public TextMeshProUGUI NameText;
    public Image DialoguePortrait;
    public TextMeshProUGUI DialogueText;
    public int Index = 0;
    private Queue<string> _sentences;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        _sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        NameText.text = dialogue.Name;
        DialoguePortrait.sprite = dialogue.Portrait;
        _sentences.Clear();
        foreach (string sentence in dialogue.Sentences)
        {
            _sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public bool DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            return false; // The boolean is needed to check if it's possible to switch to the next character
        }
        string sentence = _sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        return _sentences.Count > 0; // Solves the problem with count being 0 after taking a sentence but returning true
    }

    private IEnumerator TypeSentence(string sentence)
    {
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            //yield return null;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
