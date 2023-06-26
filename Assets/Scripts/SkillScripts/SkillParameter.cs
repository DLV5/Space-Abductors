using UnityEngine;

public class SkillParameter : MonoBehaviour
{
    public string SkillName;
    public int Price;

    private bool _isBought = false;
    public bool IsBought { 
        get => _isBought; 
        set
        {
            _isBought = value;
            GetComponent<UIActivator>().EnableImage();
        }
    }
}
