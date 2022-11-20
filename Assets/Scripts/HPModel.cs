using UnityEngine;
using UniRx;

public class HPModel: MonoBehaviour
{
    /// <summary>
    /// �̗�
    /// </summary>
    public IReadOnlyReactiveProperty<int> Health => _health;

    private readonly IntReactiveProperty _health = new IntReactiveProperty(100);

    /// <summary>
    /// �_���[�W���󂯂��Ƃ��̏���
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
