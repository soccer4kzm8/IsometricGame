using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private float _turnSpeed = 360f;
    private IInputEventProvider _inputEventProvider;
    private Rigidbody _rb;

    private void Start()
    {
        _inputEventProvider = GetComponent<IInputEventProvider>();
        _rb = GetComponent<Rigidbody>();
    }

	private void FixedUpdate()
	{
        Move();
        Look();
    }

    private void Look()
	{
        if (_inputEventProvider.MoveDirection.Value == Vector3.zero)
            return;

        var rot = Quaternion.LookRotation(Helpers.ToIso(_inputEventProvider.MoveDirection.Value), Vector3.up);
        this.transform.rotation = rot;
    }

    private void Move()
	{
        _rb.MovePosition(this.transform.position + this.transform.forward * _inputEventProvider.MoveDirection.Value.normalized.magnitude * _moveSpeed * Time.deltaTime);
    }
}

public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
