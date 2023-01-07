using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private HPModel _hPModel;
    #endregion SerializeField

    #region private
    private IGetHitEventProvider _getHitEventProvider;
    private IInSightEventProvider _inSightEventProvider;
    private Vector3 _prePos;
    private Animator _enemyAnimator;
    private ReactiveProperty<bool> _animationGetHit = new ReactiveProperty<bool>();
    private static readonly int HashSpeed = Animator.StringToHash("Speed");
    private static readonly int HashInRange = Animator.StringToHash("InRange");
    private static readonly int HashGetHit = Animator.StringToHash("GetHit");
    private static readonly int HashIsDead = Animator.StringToHash("IsDead");
    #endregion private

    #region public
    public IReadOnlyReactiveProperty<bool> AnimationGetHit => _animationGetHit;
	#endregion public

	#region const
    private const string GET_HIT = "GetHit";
    #endregion const

    private void Start()
    {
        _getHitEventProvider = this.GetComponent<IGetHitEventProvider>();
        _inSightEventProvider = this.GetComponent<IInSightEventProvider>();
        _enemyAnimator = this.GetComponent<Animator>();
        _prePos = this.transform.position;
        _inSightEventProvider.InSight
            .Subscribe(inSight =>
            {
                if (inSight == true)
                {
                    _enemyAnimator.SetBool(HashInRange, true);
                }
                else
                {
                    _enemyAnimator.SetBool(HashInRange, false);
                }
            });

        _getHitEventProvider.GetHit
            .Subscribe(getHit =>
            {
                if (getHit == true)
                {
                    _enemyAnimator.SetBool(HashGetHit, true);
                }
                else
                {
                    _enemyAnimator.SetBool(HashGetHit, false);
                }
            });
        _hPModel.HP
            .Skip(1)
            .Where(hp => hp <= 0)
            .Subscribe(_ =>
            {
                _enemyAnimator.SetBool(HashIsDead, true);
            })
            .AddTo(this);
        ObservableStateMachineTrigger trigger = _enemyAnimator.GetBehaviour<ObservableStateMachineTrigger>();
        trigger.OnStateUpdateAsObservable()
            .Where(onStateInfo => onStateInfo.Animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            .Where(onStateInfo => onStateInfo.Animator.IsInTransition(0) == false)
            .Where(onStateInfo => onStateInfo.StateInfo.normalizedTime > 1)
            .Take(1)
            .Subscribe(_ =>
            {
                Debug.LogError("死亡アニメーション終了");
            }).AddTo(this);
    }

    private void Update()
    {
        _animationGetHit.Value = _enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName(GET_HIT);

        if (Mathf.Approximately(Time.deltaTime, 0))
            return;

        float velocity = ((this.transform.position - _prePos) / Time.deltaTime).magnitude;

        _enemyAnimator.SetFloat(HashSpeed, velocity);
        _prePos = this.transform.position;
    }
}
