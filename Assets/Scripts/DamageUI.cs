using System.Collections.Generic;
using UnityEngine;

public class DamageUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> _damageTexts = new List<GameObject>();
    public static DamageUI Instance { get; private set;}
    private DamageUI() {
        Instance = this;
    }
    public void ShowDamageOnEnemy(Vector2 enemyTransform)
    {
        var gameObj = GetDisabledText();
        gameObject.transform.position = enemyTransform;
        //tex.text = "100";

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
