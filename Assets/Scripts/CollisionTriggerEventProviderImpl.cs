using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class CollisionTriggerEventProviderImpl : MonoBehaviour, ICollisionTriggerEventProvider
{
	#region SerializeField
	/// <summary>
	/// 攻撃範囲
	/// </summary>
	[SerializeField] private GameObject _attackRange;
	#endregion SerializeField

	private readonly ReactiveProperty<bool> _inSight = new ReactiveProperty<bool>();
	private readonly ReactiveProperty<bool> _getHit = new ReactiveProperty<bool>();

	public IReadOnlyReactiveProperty<bool> InSight => _inSight;
	public IReadOnlyReactiveProperty<bool> GetHit => _getHit;

	#region const
	/// <summary>
	/// 敵の当たり判定部分
	/// </summary>
	private const string ENEMY = "Body";
	/// <summary>
	/// 視界角度
	/// </summary>
	private const float SIGHTANGLE = 45f;

	#endregion const
	void Start()
	{
		_attackRange.OnTriggerStayAsObservable()
					.Where(x => x.gameObject.name == ENEMY)
					.Where(x => InSightCheck(x, SIGHTANGLE) == true)
					.Subscribe(_ => _inSight.Value = true);
		_attackRange.OnTriggerStayAsObservable()
					.Where(x => x.gameObject.name == ENEMY)
					.Where(x => InSightCheck(x, SIGHTANGLE) == false)
					.Subscribe(_ => _inSight.Value = false);
		_attackRange.OnTriggerExitAsObservable()
			.Where(x => x.gameObject.name == ENEMY)
			.Subscribe(_ => _inSight.Value = false);
		this.OnCollisionEnterAsObservable()
			.Where(x => x.gameObject.name == ENEMY)
			.Subscribe(_ => _getHit.Value = true);
		this.OnCollisionExitAsObservable()
			.Where(x => x.gameObject.name == ENEMY)
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
