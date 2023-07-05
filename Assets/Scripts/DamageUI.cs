using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageUI : MonoBehaviour
{
    private List<TMP_Text> _damageTexts = new List<TMP_Text>();

    private readonly float xOffset = 0.5f;
    private readonly float yOffset = 0.5f;

    public static DamageUI Instance { get; private set;}

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
    }

    private void Start()
    {
        _damageTexts = FindAllDamageTextBoxes();
    }

    public void ShowDamageOnEnemy(Vector2 enemyTransform, int damage)
    {
        var gameObj = GetDisabledText();
        gameObject.transform.position = enemyTransform + GetOffset();
        gameObj.text = damage.ToString();
    }

    private Vector2 GetOffset()
    {
        return new Vector2(Random.Range(-xOffset, xOffset), Random.Range(-yOffset, yOffset));
    }
    private List<TMP_Text> FindAllDamageTextBoxes()
    {
        List<TMP_Text> damageTextBoxes = new List<TMP_Text>();
        TMP_Text[] allGameObjects = FindObjectsOfType<TMP_Text>(true);
        foreach (var gameObj in allGameObjects)
        {
            if (gameObj.name.Contains("MainDamageTextBox"))
            {
                damageTextBoxes.Add(gameObj);
            }
        }
        return damageTextBoxes;
    }

    private TMP_Text GetDisabledText()
    {
        foreach (TMP_Text text in _damageTexts)
        {
            if (!text.gameObject.activeSelf)
            {
                text.gameObject.SetActive(true);
                return text;
            }
        }
        return null;
    }

}
