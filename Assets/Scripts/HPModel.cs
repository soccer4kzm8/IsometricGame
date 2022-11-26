using UnityEngine;
using UniRx;

public class HPModel: MonoBehaviour
{
    /// <summary>
    /// 最大HP
    /// </summary>
    public readonly int maxHP = 100;

    /// <summary>
    /// 残っているHP
    /// </summary>
    public IReadOnlyReactiveProperty<int> HP => _hp;

    private readonly IntReactiveProperty _hp = new IntReactiveProperty();

    private void Start()
    {
        _hp.Value = maxHP;
    }
    /// <summary>
    /// ダメージを受けたときの処理
    /// </summary>
    public void GetDamage()
	{
        _hp.Value -= 10;
	}

	private void OnDestroy()
	{
        _hp.Dispose();
	}
}
