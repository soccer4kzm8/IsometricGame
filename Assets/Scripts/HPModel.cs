using UnityEngine;
using UniRx;

public class HPModel: MonoBehaviour, IHPModel
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
    /// ダメージ受けた時の処理
    /// </summary>
    /// <param name="attackPoint">相手の攻撃力</param>
    public void GetDamage(int attackPoint)
	{
        _hp.Value -= attackPoint;
	}

	private void OnDestroy()
	{
        _hp.Dispose();
	}
}
