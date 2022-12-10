using UnityEngine;
using UniRx;

public class HPModel: MonoBehaviour, IHPModel
{
    /// <summary>
    /// �ő�HP
    /// </summary>
    [SerializeField] private int _maxHP = 0;

    public int MaxHP => _maxHP;

    /// <summary>
    /// �c���Ă���HP
    /// </summary>
    public IReadOnlyReactiveProperty<int> HP => _hp;

    private readonly IntReactiveProperty _hp = new IntReactiveProperty();

    private void Start()
    {
        _hp.Value = _maxHP;
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
