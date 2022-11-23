using UniRx;

public interface IGetHitEventProvider
{
	IReadOnlyReactiveProperty<bool> GetHit { get; }
}
