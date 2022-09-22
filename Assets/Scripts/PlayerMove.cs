using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 4f;
    private IInputEventProvider _inputEventProvider;
    private Vector3 test;

    private Vector3 forward, right;
    void Start()
    {
        _inputEventProvider = GetComponent<IInputEventProvider>();
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    void Update()
    {
        Vector3 rightMovement = right * _moveSpeed * Time.deltaTime * _inputEventProvider.MoveDirection.Value.x;
        Vector3 upMovement = forward * _moveSpeed * Time.deltaTime * _inputEventProvider.MoveDirection.Value.z;
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
		this.transform.forward = heading;

		this.transform.position += rightMovement;
        this.transform.position += upMovement;
    }
}
