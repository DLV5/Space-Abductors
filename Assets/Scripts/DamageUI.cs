using System.Collections.Generic;
using UnityEngine;

public class DamageUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> _damageTexts = new List<GameObject>();
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

    public void ShowDamageOnEnemy(Vector2 enemyTransform)
    {
        var gameObj = GetDisabledText();
        gameObject.transform.position = enemyTransform;
        //tex.text = "100";

    }
    private List<GameObject> FindAllDamageTextBoxes()
    {
        List<GameObject> damageTextBoxes = new List<GameObject>();
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>(true);
        foreach (var gameObj in allGameObjects)
        {
            if (gameObj.name.Contains("MainDamageTextBox"))
            {
                damageTextBoxes.Add(gameObj);
            }
        }
        return damageTextBoxes;
    }

    private GameObject GetDisabledText()
    {
        foreach (GameObject text in _damageTexts)
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
