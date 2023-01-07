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
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private Animator _playerAnimator;
    #endregion SerialilzedField

    #region private
    private NavMeshAgent _navMeshAgent = null;
    private Vector3 _nockBackVec;
    /// <summary>
    /// ノックバック中trueになるフラグ
    /// </summary>
    private bool _duringKnockBack = false;
    #endregion private


    private void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        var enemyAnimation = this.GetComponent<EnemyAnimation>();
        _body.OnTriggerEnterAsObservable()
            .Where(x => x.gameObject.name == _sword.name)
            .Where(_ => _playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack02_SwordAndShiled") == true)
            .Subscribe(_ => 
            {
                _navMeshAgent.isStopped = true;
                _nockBackVec = Vector3.back;
            });
        this.UpdateAsObservable()
            .Where(_ => _duringKnockBack == true)
            .Where(_ => enemyAnimation.AnimationGetHit.Value == false)
            .Subscribe(_ => 
            {
                _navMeshAgent.isStopped = false;
                _duringKnockBack = false;
            });
        this.UpdateAsObservable()
            .Where(_ => enemyAnimation.AnimationGetHit.Value == true)
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
        this.transform.Translate(_moveSpeed * Time.deltaTime * _nockBackVec);
        _duringKnockBack = true;
    }
}
