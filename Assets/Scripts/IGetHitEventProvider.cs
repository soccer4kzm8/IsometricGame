using UniRx;

public interface IGetHitEventProvider
{
	/// <summary>
	/// �U������������
	/// </summary>
	IReadOnlyReactiveProperty<bool> GetHit { get; }
}
