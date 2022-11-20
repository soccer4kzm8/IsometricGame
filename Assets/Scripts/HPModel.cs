using UnityEngine;
using UniRx;

public class HPModel: MonoBehaviour
{
    /// <summary>
    /// 体力
    /// </summary>
    public IReadOnlyReactiveProperty<int> Health => _health;

    private readonly IntReactiveProperty _health = new IntReactiveProperty(100);

    /// <summary>
    /// ダメージを受けたときの処理
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
