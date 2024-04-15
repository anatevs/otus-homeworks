using UnityEngine;

public class HeroModel : MonoBehaviour
{
    public int HP => _initHP;
    public int Damage => _initDamage;

    [SerializeField]
    private int _initHP;

    [SerializeField]
    private int _initDamage;
}