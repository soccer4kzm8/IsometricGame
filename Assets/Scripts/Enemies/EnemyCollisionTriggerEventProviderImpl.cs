﻿using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class EnemyCollisionTriggerEventProviderImpl : MonoBehaviour, IGetHitEventProvider, IInSightEventProvider
{
    #region SerializeField
    /// <summary>
	/// 攻撃範囲
	/// </summary>
	[SerializeField] private GameObject _attackRange;

	/// <summary>
	/// 相手の攻撃パーツ名
	/// </summary>
	[SerializeField] private GameObject _opponentAttackParts;

	/// <summary>
	/// 相手の攻撃を受けるパーツ
	/// </summary>
	[SerializeField] private GameObject _opponentDamagedParts;

	/// <summary>
	/// 視界角度
	/// </summary>
	[SerializeField] private float _sightAngle;

	/// <summary>
	/// 攻撃を受けるパーツ
	/// </summary>
	[SerializeField] private GameObject _damagedParts;

	/// <summary>
	/// 相手のアニメーター
	/// </summary>
	[SerializeField] private Animator _opponentAnimator;
	#endregion SerializeField
	private readonly ReactiveProperty<bool> _inSight = new ReactiveProperty<bool>();
    private readonly ReactiveProperty<bool> _getHit = new ReactiveProperty<bool>();

    public IReadOnlyReactiveProperty<bool> InSight => _inSight;
    public IReadOnlyReactiveProperty<bool> GetHit => _getHit;

    void Start()
    {
		_attackRange.OnTriggerStayAsObservable()
					.Where(x => x.gameObject == _opponentDamagedParts)
					.Where(x => InSightCheck(x, _sightAngle) == true)
					.Subscribe(_ => _inSight.Value = true);
		_attackRange.OnTriggerStayAsObservable()
					.Where(x => x.gameObject == _opponentDamagedParts)
					.Where(x => InSightCheck(x, _sightAngle) == false)
					.Subscribe(_ => _inSight.Value = false);
		_attackRange.OnTriggerExitAsObservable()
			.Where(x => x.gameObject == _opponentDamagedParts)
			.Subscribe(_ => _inSight.Value = false);
		_damagedParts.OnTriggerEnterAsObservable()
			.Where(x => x.gameObject == _opponentAttackParts)
			.Where(_ => _opponentAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack02_SwordAndShiled") == true)
			.Subscribe(_ => _getHit.Value = true);
		_damagedParts.OnTriggerExitAsObservable()
			.Where(x => x.gameObject == _opponentAttackParts)
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
}
