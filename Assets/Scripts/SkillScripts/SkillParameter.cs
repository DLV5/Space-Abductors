using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SkillParameter : MonoBehaviour
{
    public string SkillName;
    public int Price;

    private bool isBought = false;
    public bool IsBought { 
        get => isBought; 
        set
        {
            isBought = value;
            GetComponent<UIActivator>().EnableImage();
        }
            }
}
