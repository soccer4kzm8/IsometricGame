using UniRx;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private HPModel _hPModel;
    #endregion SerializeField

    #region private
    private IGetHitEventProvider _getHitEventProvider;
	private IInSightEventProvider _inSightEventProvider;
	private Vector3 _prePos;
	private Animator _playerAnimator;
	private static readonly int HashSpeed = Animator.StringToHash("Speed");
	private static readonly int HashInRange = Animator.StringToHash("InRange");
	private static readonly int HashGetHit = Animator.StringToHash("GetHit");
	private static readonly int HashIsDead = Animator.StringToHash("IsDead");
    #endregion private

    private void Start()
	{
		_getHitEventProvider = this.GetComponent<IGetHitEventProvider>();
		_inSightEventProvider = this.GetComponent<IInSightEventProvider>();
		_playerAnimator = this.GetComponent<Animator>();
		_prePos = this.transform.position;
		_inSightEventProvider.InSight
			.Subscribe(inSight => 
			{
				if(inSight == true)
				{
					_playerAnimator.SetBool(HashInRange, true);
				}
				else
				{
					_playerAnimator.SetBool(HashInRange, false);
				}
			});
		_getHitEventProvider.GetHit
			.Subscribe(getHit =>
			{ 
				if(getHit == true)
				{
					_playerAnimator.SetBool(HashGetHit, true);
				}
				else
				{
					_playerAnimator.SetBool(HashGetHit, false);
				}
			});
		_hPModel.HP
			.Skip(1)
			.Where(hp => hp <= 0)
			.Subscribe(_ => _playerAnimator.SetBool(HashIsDead, true))
			.AddTo(this);
	}

	private void FixedUpdate()
	{
		if (Mathf.Approximately(Time.deltaTime, 0))
			return;

		float velocity = ((this.transform.position - _prePos) / Time.deltaTime).magnitude;
		_playerAnimator.SetFloat(HashSpeed, velocity);
		_prePos = this.transform.position;
	}
}
