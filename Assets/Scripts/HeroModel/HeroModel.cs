using UnityEngine;

public class HeroModel : MonoBehaviour
{
    public int HP => _initHP;
    public int Damage => _initDamage;
    public Ability HeroAbility => _ability;

    [SerializeField]
    private int _initHP;

    [SerializeField]
    private int _initDamage;

    [SerializeField]
    private Ability _ability;
}