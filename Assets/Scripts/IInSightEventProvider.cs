using UniRx;

public interface IInSightEventProvider
{
    /// <summary>
    /// ���E�����ǂ���
    /// </summary>
    IReadOnlyReactiveProperty<bool> InSight { get; }
}
