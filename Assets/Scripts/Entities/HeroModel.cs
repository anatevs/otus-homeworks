using UnityEngine;

public class HeroModel : MonoBehaviour
{
    public int HP => _hp;
    public int Damage => _damage;

    [SerializeField]
    private int _hp;

    [SerializeField]
    private int _damage;
}