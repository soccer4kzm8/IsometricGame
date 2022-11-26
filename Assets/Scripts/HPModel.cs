using UnityEngine;
using UniRx;

public class HPModel: MonoBehaviour
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
    /// �_���[�W���󂯂��Ƃ��̏���
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
