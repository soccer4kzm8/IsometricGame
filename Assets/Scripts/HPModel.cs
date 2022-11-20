using UnityEngine;
using UniRx;

public class HPModel: MonoBehaviour
{
    /// <summary>
    /// ‘Ì—Í
    /// </summary>
    public IReadOnlyReactiveProperty<int> Health => _health;

    private readonly IntReactiveProperty _health = new IntReactiveProperty(100);

    /// <summary>
    /// ƒ_ƒ[ƒW‚ğó‚¯‚½‚Æ‚«‚Ìˆ—
    /// </summary>
    public void GetDamage()
	{
        _health.Value -= 10;
	}

	private void OnDestroy()
	{
        _health.Dispose();
	}
}
