using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    #region SerializeField
    /// <summary>
    /// 攻撃範囲
    /// </summary>
    [SerializeField] private GameObject _attackRange;
    #endregion SerializeField

    private Vector3 _prePos;
    private Animator _animator;
    private static readonly int HashSpeed = Animator.StringToHash("Speed");
    private static readonly int HashInRange = Animator.StringToHash("InRange");
    private static readonly int HashGetHit = Animator.StringToHash("GetHit");

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

    private void Start()
    {
        _animator = this.GetComponent<Animator>();
        _prePos = this.transform.position;
        _attackRange.OnTriggerStayAsObservable()
                    .Where(x => InSight(x, SIGHTANGLE))
                    .Subscribe(_ => _animator.SetBool(HashInRange, true));
        _attackRange.OnTriggerStayAsObservable()
                    .Where(x => OutSight(x, SIGHTANGLE))
                    .Subscribe(_ => _animator.SetBool(HashInRange, false));
        _attackRange.OnTriggerExitAsObservable()
            .Where(x => x.gameObject.name == ENEMY)
            .Subscribe(_ => _animator.SetBool(HashInRange, false));
        this.OnCollisionEnterAsObservable()
            .Where(x => x.gameObject.name == ENEMY)
            .Subscribe(_ => _animator.SetBool(HashGetHit, true));
        this.OnCollisionExitAsObservable()
            .Where(x => x.gameObject.name == ENEMY)
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

    /// <summary>
    /// 当たったオブジェクトが視界内かどうか
    /// </summary>
    /// <param name="collider">当たったオブジェクトのcollider</param>
    /// <param name="sightAngle">視界角度</param>
    /// <returns></returns>
    private bool InSight(Collider collider, float sightAngle)
    {
        if (collider.gameObject.name == ENEMY)
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
        if (collider.gameObject.name == ENEMY)
        {
            // プレイヤーから敵に向かうベクトル
            Vector3 posDelta = collider.transform.position - this.transform.position;
            // プレイヤーの前方方向ベクトルとプレイヤーから敵に向かうベクトルのなす角
            float targetAngle = Vector3.Angle(this.transform.forward, posDelta);
            if (targetAngle > sightAngle)
            {
                return true;
            }
        }
        return false;
    }
}
