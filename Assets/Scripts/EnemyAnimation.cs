using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _sword;
    [SerializeField] private GameObject _attackRange;
    private Vector3 _prePos;
    private Animator _animator;
    private static readonly int HashSpeed = Animator.StringToHash("Speed");
    private static readonly int HashInRange = Animator.StringToHash("InRange");

    private void Start()
    {
        _animator = this.GetComponent<Animator>();
        _prePos = this.transform.position;
        _attackRange.OnTriggerEnterAsObservable()
                    .Where(x => x.gameObject.name == _player.name)
                    .Subscribe(_ => _animator.SetBool(HashInRange, true));
        _attackRange.OnTriggerExitAsObservable()
                    .Where(x => x.gameObject.name == _player.name)
                    .Subscribe(_ => _animator.SetBool(HashInRange, false));
        this.gameObject.OnTriggerEnterAsObservable()
            .Where(x => x.gameObject.name == _sword.name)
			.Subscribe(x => Debug.LogError("Œ•‚É“–‚½‚Á‚½"));
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
