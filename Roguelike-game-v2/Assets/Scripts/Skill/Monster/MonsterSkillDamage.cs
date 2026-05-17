using System;
public class MonsterSkillDamage : MonsterSkill, IDamage
{
    private Func<float> _damage = null;

    public Func<float> Damage { get { return _damage; } set { _damage = value; } }
    public float DamageAmount { get { return _damage.Invoke(); } }
    protected override void Enable() { }
}