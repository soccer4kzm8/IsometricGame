using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private GameObject _sword;
    [SerializeField] private GameObject _attackRange;
    [SerializeField] private GameObject _body;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private HPModel _hPModel;
    #endregion SerializeField

    #region private
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
	private const string PLAYER = "Player";
    private const float SIGHTANGLE = 30f;
    private const string GET_HIT = "GetHit";
    #endregion const

    private void Start()
    {
        _enemyAnimator = this.GetComponent<Animator>();
        _prePos = this.transform.position;
        _attackRange.OnTriggerStayAsObservable()
                    .Where(x => InSight(x, SIGHTANGLE))
                    .Subscribe(_ => _enemyAnimator.SetBool(HashInRange, true));
        _attackRange.OnTriggerStayAsObservable()
                    .Where(x => OutSight(x, SIGHTANGLE))
                    .Subscribe(_ => _enemyAnimator.SetBool(HashInRange, false));
        _attackRange.OnTriggerExitAsObservable()
           .Where(x => x.gameObject.name == PLAYER)
           .Subscribe(_ => _enemyAnimator.SetBool(HashInRange, false));
        _body.OnTriggerEnterAsObservable()
            .Where(x => x.gameObject.name == _sword.name)
            .Where(_ => _playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack02_SwordAndShiled") == true)
            .Subscribe(_ => _enemyAnimator.SetBool(HashGetHit, true));
        _body.OnTriggerExitAsObservable()
            .Where(x => x.gameObject.name == _sword.name)
            .Subscribe(_ => _enemyAnimator.SetBool(HashGetHit, false));
        _hPModel.HP
            .Skip(1)
            .Where(hp => hp <= 0)
            .Subscribe(_ =>
            {
                _enemyAnimator.SetBool(HashIsDead, true);
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

    /// <summary>
    /// 当たったオブジェクトが視界内かどうか
    /// </summary>
    /// <param name="collider">当たったオブジェクトのcollider</param>
    /// <param name="sightAngle">視界角度</param>
    /// <returns></returns>
    private bool InSight(Collider collider, float sightAngle)
    {
        if (collider.gameObject.name == PLAYER)
        {
            Vector3 posDelta = collider.transform.position - this.transform.position;
            float targetAngle = Vector3.Angle(this.transform.forward, posDelta);
            if (targetAngle <= sightAngle)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 当たったオブジェクトが視界外かどうか
    /// </summary>
    /// <param name="collider">当たったオブジェクトのcollider</param>
    /// <param name="sightAngle">視界角度</param>
    /// <returns></returns>
    private bool OutSight(Collider collider, float sightAngle)
    {
        if (collider.gameObject.name == PLAYER)
        {
            Vector3 posDelta = collider.transform.position - this.transform.position;
            float targetAngle = Vector3.Angle(this.transform.forward, posDelta);
            if (targetAngle > sightAngle)
            {
                return true;
            }
        }
        return false;
    }
}
