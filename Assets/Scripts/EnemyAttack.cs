using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _attackRange;
    void Start()
    {
        _attackRange.OnTriggerEnterAsObservable()
                    .Where(x => x.gameObject.name == _player.name)
                    .Subscribe(_ => Debug.LogError("”ÍˆÍ‚É“ü‚Á‚½"));
        _attackRange.OnTriggerExitAsObservable()
                    .Where(x => x.gameObject.name == _player.name)
                    .Subscribe(_ => Debug.LogError("”ÍˆÍ‚©‚ço‚½"));
    }
}
