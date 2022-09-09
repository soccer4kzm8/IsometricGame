using UnityEngine;
using UniRx;

public class InputEventProviderImpl : MonoBehaviour, IInputEventProvider
{
    private readonly ReactiveProperty<Vector3> _moveDirection = new ReactiveProperty<Vector3>();

    public IReadOnlyReactiveProperty<Vector3> MoveDirection => _moveDirection;

    private void Start()
    {
        _moveDirection.AddTo(this);
    }

    private void Update()
    {
        _moveDirection.SetValueAndForceNotify(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
    }
}
