using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private GameObject _sword;
    [SerializeField] private GameObject _attackRange;
    [SerializeField] private GameObject _body;
    #endregion SerializeField

    private Vector3 _prePos;
    private Animator _animator;
    private static readonly int HashSpeed = Animator.StringToHash("Speed");
    private static readonly int HashInRange = Animator.StringToHash("InRange");
    private static readonly int HashGetHit = Animator.StringToHash("GetHit");

    #region const
    private const string PLAYER = "Player";
    private const float SIGHTANGLE = 30f;

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
        _body.OnTriggerEnterAsObservable()
            .Where(x => x.gameObject.name == _sword.name)
            .Subscribe(_ => _animator.SetBool(HashGetHit, true));
        _body.OnTriggerExitAsObservable()
            .Where(x => x.gameObject.name == _sword.name)
            .Subscribe(_ => _animator.SetBool(HashGetHit, false));
    }

    private void FixedUpdate()
    {
        if (Mathf.Approximately(Time.deltaTime, 0))
            return;

        float velocity = ((this.transform.position - _prePos) / Time.deltaTime).magnitude;
        if(velocity < 0.1f)
        {
            Debug.LogError($"position : {this.transform.position}");
            Debug.LogError($"_prePos : {_prePos}");

            Debug.LogError($"velocity : {velocity}");

        }
        _animator.SetFloat(HashSpeed, velocity);
        _prePos = this.transform.position;
    }

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
