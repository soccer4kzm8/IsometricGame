using UniRx;

public interface ICollisionTriggerEventProvider
{
	IReadOnlyReactiveProperty<bool> InSight { get; }
	IReadOnlyReactiveProperty<bool> GetHit { get; }
}
