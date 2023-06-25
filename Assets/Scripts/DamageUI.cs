using System.Collections.Generic;
using UnityEngine;

public class DamageUI : MonoBehaviour
{
    public static DamageUI Instance;
    DamageUI() {
        Instance = this;
    }
    [SerializeField]
    List<GameObject> damageTexts = new List<GameObject>();
    public void ShowDamageOnEnemy(Vector2 enemyTransform)
    {
        GameObject gameObj = GetDisabledText();
        gameObject.transform.position = enemyTransform;
        //tex.text = "100";

    }

    private GameObject GetDisabledText()
    {
        foreach (GameObject text in damageTexts)
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
