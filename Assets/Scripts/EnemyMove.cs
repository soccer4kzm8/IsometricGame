using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    #region SerializedField
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _sword;
    [SerializeField] private GameObject _body;
    [SerializeField] private float _moveSpeed = 4f;
    #endregion SerialilzedField

    #region private
    private NavMeshAgent _navMeshAgent = null;
    private Animator _animator;
    /// <summary>
    /// 剣に当たったか
    /// </summary>
    private bool _getHit = false;
    #endregion private

    #region const
    private const string GET_HIT = "GetHit";
    #endregion const

    private void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _animator = this.GetComponent<Animator>();
        var enemyAnimation = this.GetComponent<EnemyAnimation>();
        _body.OnTriggerEnterAsObservable()
            .Where(x => x.gameObject.name == _sword.name)
            .Subscribe(_ => 
            {
                _getHit = true;
                _navMeshAgent.isStopped = true;
            });
        _body.OnTriggerExitAsObservable()
            .Where(x => x.gameObject.name == _sword.name)
            .Where(_ => _animator.GetCurrentAnimatorStateInfo(0).IsName(GET_HIT) == false)
            .Subscribe(_ => _navMeshAgent.isStopped = false);
        this.UpdateAsObservable()
            .Where(_ => _animator.GetCurrentAnimatorStateInfo(0).IsName(GET_HIT) == true)
            .Subscribe(_ => KnockBack());
    }

    private void FixedUpdate()
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
