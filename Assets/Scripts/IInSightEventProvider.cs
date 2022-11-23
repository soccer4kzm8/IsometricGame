using UniRx;

public interface IInSightEventProvider
{
    IReadOnlyReactiveProperty<bool> InSight { get; }
}
