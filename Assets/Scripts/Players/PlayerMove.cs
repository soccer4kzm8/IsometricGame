using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region SerializeField
    /// <summary>
    /// 速度
    /// </summary>
    [SerializeField] private float _moveSpeed = 4f;

    /// <summary>
    /// 回転速度
    /// </summary>
    [SerializeField] private float _turnSpeed = 360f;
    #endregion SerializeField

    #region private変数
    private IInputEventProvider _inputEventProvider;
    private Rigidbody _rb;
    private PlayerStateManager _playerStateManager;
    #endregion private変数

    private void Start()
    {
        _inputEventProvider = GetComponent<IInputEventProvider>();
        _rb = GetComponent<Rigidbody>();
        _playerStateManager = GetComponent<PlayerStateManager>();
    }

    private void FixedUpdate()
	{
        if(_playerStateManager.State.Value == PlayerState.Alive)
        {
            Move();
            Look();
        }
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
