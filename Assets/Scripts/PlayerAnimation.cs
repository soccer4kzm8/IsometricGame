using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	private ICollisionTriggerEventProvider _collisionTriggerEventProvider;
	private Vector3 _prePos;
	private Animator _animator;
	private static readonly int HashSpeed = Animator.StringToHash("Speed");
	private static readonly int HashInRange = Animator.StringToHash("InRange");
	private static readonly int HashGetHit = Animator.StringToHash("GetHit");

	private void Start()
	{
		_collisionTriggerEventProvider = this.GetComponent<ICollisionTriggerEventProvider>();
		_animator = this.GetComponent<Animator>();
		_prePos = this.transform.position;
		_collisionTriggerEventProvider.InSight
			.Where(x => x == true)
			.Subscribe(_ =>
			{
				_animator.SetBool(HashInRange, true);
			});
		_collisionTriggerEventProvider.InSight
			.Where(x => x == false)
			.Subscribe(_ =>
			{
				_animator.SetBool(HashInRange, false);
			});
		_collisionTriggerEventProvider.GetHit
			.Where(x => x == true)
			.Subscribe(_ => _animator.SetBool(HashGetHit, true));
		_collisionTriggerEventProvider.GetHit
			.Where(x => x == false)
			.Subscribe(_ => _animator.SetBool(HashGetHit, false));
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
