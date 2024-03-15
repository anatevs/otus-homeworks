using System;

[Serializable]
public sealed class ZombieInitParams
{
    public bool isAttacking;
    public bool canMove;
    public int hp;
    public bool isDead;
}