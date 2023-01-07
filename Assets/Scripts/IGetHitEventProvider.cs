using UniRx;

public interface IGetHitEventProvider
{
	/// <summary>
	/// 攻撃を喰らったか
	/// </summary>
	IReadOnlyReactiveProperty<bool> GetHit { get; }
}
