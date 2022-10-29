using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _sword;
    [SerializeField] private GameObject _body;
    [SerializeField] private float _moveSpeed = 4f;

    private NavMeshAgent _navMeshAgent = null;
    private Animator _animator;
    /// <summary>
    /// 剣に当たったか
    /// </summary>
    private bool _getHit = false;

    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _animator = this.GetComponent<Animator>();
        _body.OnTriggerEnterAsObservable()
            .Where(x => x.gameObject.name == _sword.name)
            .Subscribe(_ => 
            {
                _getHit = true;
                _navMeshAgent.isStopped = true;
            });
        //_body.OnTriggerExitAsObservable()
        //    .Where(x => x.gameObject.name == _sword.name)
        //    .Subscribe(_ => _navMeshAgent.isStopped = false);
        this.UpdateAsObservable()
            .Where(_ => _getHit == true)
            .TakeWhile(_ => _animator.GetBool(EnemyAnimation.GetHashGetHit) == true)
            .Subscribe(_ => KnockBack());
    }

    void FixedUpdate()
    {
        if(_navMeshAgent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
		}
    }

    private void KnockBack()
	{
        //いきなり位置移動させるんじゃなくて、Update()内で位置移動するようにする。UniRx使える？
        // ノックバックが終わったら、SetGetHit(true)
        this.transform.Translate(_moveSpeed * Time.deltaTime * -this.transform.forward);
    }
}
