using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Vector3 _prePos;
    private Animator _animator;
    private static readonly int HashSpeed = Animator.StringToHash("Speed");

    private void Start()
    {
        _animator = this.GetComponent<Animator>();
        _prePos = this.transform.position;
    }

    private void FixedUpdate()
    {
        if (Mathf.Approximately(Time.deltaTime, 0))
            return;

        float velocity = ((this.transform.position - _prePos) / Time.deltaTime).magnitude;
        _animator.SetFloat(HashSpeed, velocity);
        _prePos = this.transform.position;
    }
}
