using UnityEngine;
public interface IFakeShadowSource
{
    public SpriteRenderer SpriteRender { get; }

    public Vector3 TargetPosition { get; }

    public Vector3 InitialPosition { get; }

    public Vector3 CurrentPosition { get; }
}