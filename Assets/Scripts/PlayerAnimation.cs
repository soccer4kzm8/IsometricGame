using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Vector3 _prePos;
    private Animator _animator;
    private static readonly int HashSpeed = Animator.StringToHash("Speed");
    private static readonly int HashGetHit = Animator.StringToHash("GetHit");

    #region const
    private const string ENEMY = "Enemy";
    #endregion const

    private void Start()
    {
        _animator = this.GetComponent<Animator>();
        _prePos = this.transform.position;
        this.OnCollisionEnterAsObservable()
            .Where(x => x.gameObject.name == ENEMY)
            .Subscribe(x => _animator.SetBool(HashGetHit, true));
        this.OnCollisionExitAsObservable()
            .Where(x => x.gameObject.name == ENEMY)
            .Subscribe(x => _animator.SetBool(HashGetHit, false));
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
