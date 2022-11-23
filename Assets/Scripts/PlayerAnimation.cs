using UniRx;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	private IGetHitEventProvider _getHitEventProvider;
	private IInSightEventProvider _inSightEventProvider;
	private Vector3 _prePos;
	private Animator _animator;
	private static readonly int HashSpeed = Animator.StringToHash("Speed");
	private static readonly int HashInRange = Animator.StringToHash("InRange");
	private static readonly int HashGetHit = Animator.StringToHash("GetHit");

	private void Start()
	{
		_getHitEventProvider = this.GetComponent<IGetHitEventProvider>();
		_inSightEventProvider = this.GetComponent<IInSightEventProvider>();
		_animator = this.GetComponent<Animator>();
		_prePos = this.transform.position;
		_inSightEventProvider.InSight
			.Subscribe(inSight => 
			{
				if(inSight == true)
				{
					_animator.SetBool(HashInRange, true);
				}
				else
				{
					_animator.SetBool(HashInRange, false);
				}
			});
		_getHitEventProvider.GetHit
			.Subscribe(getHit =>
			{ 
				if(getHit == true)
				{
					_animator.SetBool(HashGetHit, true);
				}
				else
				{
					_animator.SetBool(HashGetHit, false);
				}
			});
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
