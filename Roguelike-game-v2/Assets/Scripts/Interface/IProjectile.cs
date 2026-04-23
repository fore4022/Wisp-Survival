using System.Collections;

public interface IProjectile : IPlayerSkill
{
    public IEnumerator Moving();
}