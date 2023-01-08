using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PlayerCollisionTriggerEventProviderImpl : MonoBehaviour, IGetHitEventProvider, IInSightEventProvider
{
	#region SerializeField
	/// <summary>
	/// プレイヤー攻撃範囲
	/// </summary>
	[SerializeField] private GameObject _playerAttackRange;

	/// <summary>
	/// 視界角度
	/// </summary>
	[SerializeField] private float _sightAngle;
    #endregion SerializeField

    #region private変数
    /// <summary>
    /// 視界内に敵が居るかを監視
    /// </summary>
    private readonly ReactiveProperty<bool> _inSight = new ReactiveProperty<bool>();
    
    /// <summary>
    /// 攻撃を受けたかを監視
    /// </summary>
	private readonly ReactiveProperty<bool> _getHit = new ReactiveProperty<bool>();
    #endregion private変数

    #region public変数
    /// <summary>
    /// 視界内に敵が居るか
    /// </summary>
    public IReadOnlyReactiveProperty<bool> InSight => _inSight;

    /// <summary>
    ///  攻撃を受けたか
    /// </summary>
	public IReadOnlyReactiveProperty<bool> GetHit => _getHit;
    #endregion public変数

    #region 定数
    /// <summary>
    /// 相手の攻撃パーツ
    /// </summary>
    private const string OPPONENT_ATTACK_PART = "Body";

    /// <summary>
    /// 相手の攻撃を受けるパーツ
    /// </summary>
    private const string OPPONENT_DAMADED_PART = "Body";
    #endregion 定数

    void Start()
	{
        // 敵の攻撃を受けるパーツが視界内にあるかどうか
        _playerAttackRange.OnTriggerStayAsObservable()
					.Where(collider => collider.gameObject.name == OPPONENT_DAMADED_PART)
					.Where(collider => InSightCheck(collider, _sightAngle) == true)
                    .Where(collider => IsDeadCheck(collider.transform.parent.GetComponent<EnemyStateManager>()) == false)
					.Subscribe(_ =>
                    {
                        _inSight.Value = true;
                    });
        _playerAttackRange.OnTriggerStayAsObservable()
					.Where(collider => collider.gameObject.name == OPPONENT_DAMADED_PART)
					.Where(collider => InSightCheck(collider, _sightAngle) == false)
					.Subscribe(_ => 
                    {
                        _inSight.Value = false;
                    });

        // 敵が死亡したら視界内に居ないことに
        _playerAttackRange.OnTriggerStayAsObservable()
            .Where(collider => collider.gameObject.name == OPPONENT_DAMADED_PART)
            .Where(collider => IsDeadCheck(collider.transform.parent.GetComponent<EnemyStateManager>()) == true)
            .Subscribe(_ => 
            {
                _inSight.Value = false;
            });

        // 敵の攻撃を受けるパーツが攻撃範囲から出て行った
        _playerAttackRange.OnTriggerExitAsObservable()
			.Where(collider => collider.gameObject.name == OPPONENT_DAMADED_PART)
			.Subscribe(_ =>
            {
                _inSight.Value = false;
            });

        // プレイヤー自身に敵の攻撃パーツがあったかどうか
		this.OnCollisionEnterAsObservable()
			.Where(collision => collision.gameObject.name == OPPONENT_ATTACK_PART)
            .Where(collision => IsDeadCheck(collision.transform.parent.GetComponent<EnemyStateManager>()) == false)
			.Subscribe(_ => 
            {
                _getHit.Value = true;
            });
		this.OnCollisionExitAsObservable()
			.Where(collision => collision.gameObject.name == OPPONENT_ATTACK_PART)
			.Subscribe(_ => _getHit.Value = false);
	}

	/// <summary>
	/// 当たったオブジェクトが視界内かどうか
	/// </summary>
	/// <param name="collider">当たったオブジェクトのcollider</param>
	/// <param name="sightAngle">視界角度</param>
	/// <returns></returns>
	private bool InSightCheck(Collider collider, float sightAngle)
	{
		Vector3 posDelta = collider.transform.position - this.transform.position;
		float targetAngle = Vector3.Angle(this.transform.forward, posDelta);
		if (targetAngle <= sightAngle)
		{
			return true;
		}
		return false;
	}


    /// <summary>
    /// 当たったオブジェクトが敵の場合、死んでいるかどうか
    /// </summary>
    /// <param name="enemyStateManager">当たったオブジェクトのenemyStateManager</param>
    /// <returns></returns>
    private bool IsDeadCheck(EnemyStateManager enemyStateManager) 
    {
        if(enemyStateManager != null)
        {
            var enemyState = enemyStateManager.State.Value;
            if(enemyState == EnemyState.Dead)
            {
                return true;
            }
        }
        return false;
    }
}
