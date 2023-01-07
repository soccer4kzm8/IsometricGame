using UniRx;

public interface IInSightEventProvider
{
    /// <summary>
    /// 視界内かどうか
    /// </summary>
    IReadOnlyReactiveProperty<bool> InSight { get; }
}
