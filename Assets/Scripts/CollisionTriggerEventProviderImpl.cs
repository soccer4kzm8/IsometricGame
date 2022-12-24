using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class CollisionTriggerEventProviderImpl : MonoBehaviour, IGetHitEventProvider, IInSightEventProvider
{
	#region SerializeField
	/// <summary>
	/// �U���͈�
	/// </summary>
	[SerializeField] private GameObject _attackRange;

	/// <summary>
	/// ����̍U���p�[�c��
	/// </summary>
	[SerializeField] private string _opponentAttackPartsName;

	/// <summary>
	/// ���E�p�x
	/// </summary>
	[SerializeField] private float _sightAngle;
	#endregion SerializeField

	private readonly ReactiveProperty<bool> _inSight = new ReactiveProperty<bool>();
	private readonly ReactiveProperty<bool> _getHit = new ReactiveProperty<bool>();

	public IReadOnlyReactiveProperty<bool> InSight => _inSight;
	public IReadOnlyReactiveProperty<bool> GetHit => _getHit;

	void Start()
	{
		_attackRange.OnTriggerStayAsObservable()
					.Where(x => x.gameObject.name == _opponentAttackPartsName)
					.Where(x => InSightCheck(x, _sightAngle) == true)
					.Subscribe(_ => _inSight.Value = true);
		_attackRange.OnTriggerStayAsObservable()
					.Where(x => x.gameObject.name == _opponentAttackPartsName)
					.Where(x => InSightCheck(x, _sightAngle) == false)
					.Subscribe(_ => _inSight.Value = false);
		_attackRange.OnTriggerExitAsObservable()
			.Where(x => x.gameObject.name == _opponentAttackPartsName)
			.Subscribe(_ => _inSight.Value = false);
		this.OnCollisionEnterAsObservable()
			.Where(x => x.gameObject.name == _opponentAttackPartsName)
			.Subscribe(_ => _getHit.Value = true);
		this.OnCollisionExitAsObservable()
			.Where(x => x.gameObject.name == _opponentAttackPartsName)
			.Subscribe(_ => _getHit.Value = false);
	}

	/// <summary>
	/// ���������I�u�W�F�N�g�����E�����ǂ���
	/// </summary>
	/// <param name="collider">���������I�u�W�F�N�g��collider</param>
	/// <param name="sightAngle">���E�p�x</param>
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
