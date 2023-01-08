using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    #region private
    private IGetHitEventProvider _getHitEventProvider;
    private IInSightEventProvider _inSightEventProvider;
    private Vector3 _prePos;
    private Animator _enemyAnimator;
    private ReactiveProperty<bool> _animationGetHit = new ReactiveProperty<bool>();
    private ReactiveProperty<bool> _animationDie = new ReactiveProperty<bool>();
    private static readonly int HashSpeed = Animator.StringToHash("Speed");
    private static readonly int HashInRange = Animator.StringToHash("InRange");
    private static readonly int HashGetHit = Animator.StringToHash("GetHit");
    private static readonly int HashIsDead = Animator.StringToHash("IsDead");
    #endregion private

    #region public
    public IReadOnlyReactiveProperty<bool> AnimationGetHit => _animationGetHit;
    public IReadOnlyReactiveProperty<bool> AnimationDie => _animationDie;
    #endregion public

    #region const
    private const string GET_HIT = "GetHit";
    private const string DIE = "Die";
    #endregion const

    private void Start()
    {
        _getHitEventProvider = this.GetComponent<IGetHitEventProvider>();
        _inSightEventProvider = this.GetComponent<IInSightEventProvider>();
        _enemyAnimator = this.GetComponent<Animator>();
        _prePos = this.transform.position;

        var enemyStateManager = this.GetComponent<EnemyStateManager>();
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
        enemyStateManager.State
            .Where(state => state == EnemyState.Dead)
            .Subscribe(_ =>
            {
                _enemyAnimator.SetBool(HashIsDead, true);
            })
            .AddTo(this);
        ObservableStateMachineTrigger trigger = _enemyAnimator.GetBehaviour<ObservableStateMachineTrigger>();
        trigger.OnStateUpdateAsObservable()
            .Where(onStateInfo => onStateInfo.Animator.GetCurrentAnimatorStateInfo(0).IsName(DIE))
            .Where(onStateInfo => onStateInfo.Animator.IsInTransition(0) == false)
            .Where(onStateInfo => onStateInfo.StateInfo.normalizedTime > 1)
            .Take(1)
            .Subscribe(_ =>
            {
                _animationDie.Value = true;
            })
            .AddTo(this);
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
