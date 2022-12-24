using UniRx;

public interface IInSightEventProvider
{
    /// <summary>
    /// Ž‹ŠE“à‚©‚Ç‚¤‚©
    /// </summary>
    IReadOnlyReactiveProperty<bool> InSight { get; }
}
