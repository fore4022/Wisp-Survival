public interface IMoveable
{
    public float SpeedAmount { get; }

    public float SlowDownAmount { get; }

    public void OnMove();

    public void SetSlowDown(float slowDown, float duration);
}