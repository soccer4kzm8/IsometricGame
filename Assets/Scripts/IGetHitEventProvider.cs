using UniRx;

public interface IGetHitEventProvider
{
	/// <summary>
	/// UŒ‚‚ğ‹ò‚ç‚Á‚½‚©
	/// </summary>
	IReadOnlyReactiveProperty<bool> GetHit { get; }
}
