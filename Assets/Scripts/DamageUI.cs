using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageUI : MonoBehaviour
{
    public static DamageUI instance;
    DamageUI() {
        instance = this;
    }
    [SerializeField]
    List<TMP_Text> damageTexts = new List<TMP_Text>();
    public void ShowDamageOnEnemy(Transform enemyTransform)
    {
        TMP_Text text = GetDisabledText();
        GameObject gameObject = text.gameObject;
        gameObject.transform.position = enemyTransform.position;
        text.text = "100";

    }

    private TMP_Text GetDisabledText()
    {
        foreach (var text in damageTexts)
        {
            Debug.Log(text.gameObject.transform.parent.gameObject.activeSelf);
            if (!text.gameObject.transform.parent.gameObject.activeSelf)
            {
                text.gameObject.transform.parent.gameObject.SetActive(true);
                return text;
            }
        }
        return null;
    }

}
