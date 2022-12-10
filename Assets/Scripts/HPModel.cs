using UnityEngine;
using UniRx;

public class HPModel: MonoBehaviour, IHPModel
{
    /// <summary>
    /// �ő�HP
    /// </summary>
    public readonly int maxHP = 100;

    /// <summary>
    /// �c���Ă���HP
    /// </summary>
    public IReadOnlyReactiveProperty<int> HP => _hp;

    private readonly IntReactiveProperty _hp = new IntReactiveProperty();

    private void Start()
    {
        _hp.Value = maxHP;
    }

    /// <summary>
    /// �_���[�W�󂯂����̏���
    /// </summary>
    /// <param name="attackPoint">����̍U����</param>
    public void GetDamage(int attackPoint)
	{
        _hp.Value -= attackPoint;
	}

	private void OnDestroy()
	{
        _hp.Dispose();
	}
}
