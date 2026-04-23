using UnityEngine;

public interface IPlayerSkill
{
    public bool Finished { get; }

    public void Set();

    public void Enter(GameObject go);
}