using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    #region SerializedField
    [SerializeField] private GameObject _enemyBody;
    [SerializeField] private float _moveSpeed = 1f;
    #endregion SerialilzedField

    #region private変数
    private NavMeshAgent _navMeshAgent = null;
    /// <summary>
    /// ノックバックする方向
    /// </summary>
    private Vector3 _nockBackVec;

    /// <summary>
    /// ノックバック中trueになるフラグ
    /// </summary>
    private bool _duringKnockBack = false;

    /// <summary>
    /// プレイヤーのTransform
    /// </summary>
    private Transform _player;

    private GameObject _weapon;
    private GameStateManager _gameStateManager;
    #endregion private変数

    #region 定数
    private const string PLAYER_TAG = "Player";
    private const string WEAPON_TAG = "Weapon";
    private const string MANAGER_TAG = "Manager";
    /// <summary>
    /// プレイヤーの攻撃アニメーション名
    /// </summary>
    private const string PLAYER_ATTACK = "Attack02_SwordAndShiled";
    #endregion 定数
    private void Start()
    {
        _player = GameObject.FindWithTag(PLAYER_TAG).transform;
        _weapon = GameObject.FindWithTag(WEAPON_TAG);
        _gameStateManager = GameObject.FindWithTag(MANAGER_TAG).GetComponent<GameStateManager>();
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        var enemyAnimation = this.GetComponent<EnemyAnimation>();
        var playerAnimator = _player.GetComponent<Animator>();

        // 攻撃に当たったとき、NavMeshAgentを止め、攻撃を受けた方向を取得
        _enemyBody.OnTriggerEnterAsObservable()
            .Where(x => x.gameObject.name == _weapon.name)
            .Where(_ => playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(PLAYER_ATTACK) == true)
            .Subscribe(_ => 
            {
                _navMeshAgent.isStopped = true;
                _nockBackVec = Vector3.back;
            });
        // ノックバックのアニメーションが終了したらNavMeshAgentを再開
        this.UpdateAsObservable()
            .Where(_ => _duringKnockBack == true)
            .Where(_ => enemyAnimation.AnimationGetHit.Value == false)
            .Subscribe(_ => 
            {
                _navMeshAgent.isStopped = false;
                _duringKnockBack = false;
            });
        // ノックバックのアニメーションが流れている間、ノックバックし続ける
        this.UpdateAsObservable()
            .Where(_ => enemyAnimation.AnimationGetHit.Value == true)
            .Subscribe(_ => KnockBack());
        // リザルト画面が表示されたらNavMeshAgentを止める
        _gameStateManager.State
            .Where(state => state == GameState.Result)
            .Subscribe(_ => 
            {
                _navMeshAgent.isStopped = true;
            })
            .AddTo(this);
    }


    private void FixedUpdate()
    {
        if(_navMeshAgent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            _navMeshAgent.SetDestination(_player.position);
		}
    }

    /// <summary>
    /// ノックバック
    /// </summary>
    private void KnockBack()
	{
        this.transform.Translate(_moveSpeed * Time.deltaTime * _nockBackVec);
        _duringKnockBack = true;
    }
}
