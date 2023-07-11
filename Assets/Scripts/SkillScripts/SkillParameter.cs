using UnityEngine;

public class SkillParameter : MonoBehaviour
{
    public enum SkillType { Weapon, Skill };

    public SkillType Type;
    [SerializeField] private Skill _name;
    public Skill Name
    {
        get => _name;
        private set => _name = value;
    }
    [SerializeField] private int _price;
    public int Price
    {
        get => _price;
        private set => _price = value;
    }

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
