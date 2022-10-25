using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _sword;
    [SerializeField] private GameObject _body;

    private NavMeshAgent _navMeshAgent = null;
    /// <summary>
    /// ���ɓ���������
    /// </summary>
    private bool _getHit = false;

    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _body.OnTriggerEnterAsObservable()
            .Where(x => x.gameObject.name == _sword.name)
            .Subscribe(_ => 
            {
                _getHit = true;
                _navMeshAgent.isStopped = true;
            });
        _body.OnTriggerExitAsObservable()
            .Where(x => x.gameObject.name == _sword.name)
            .Subscribe(_ => _navMeshAgent.isStopped = false);
        this.UpdateAsObservable()
            .Where(_ => _getHit == true)
            .Subscribe(_ => KnockBack());
    }

    void FixedUpdate()
    {
        if(_navMeshAgent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
		}
    }

    private void KnockBack()
	{
        //�����Ȃ�ʒu�ړ�������񂶂�Ȃ��āAUpdate()���ňʒu�ړ�����悤�ɂ���BUniRx�g����H
        // �m�b�N�o�b�N���I�������ASetGetHit(true)

    }
}
