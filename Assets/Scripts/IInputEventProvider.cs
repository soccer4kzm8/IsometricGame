using UnityEngine;
using UniRx;

public interface IInputEventProvider
{
    IReadOnlyReactiveProperty<Vector3> MoveDirection { get; }
}
