using UnityEngine;
using UniRx;

public class EnemyHPPresenter : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    private HPModel _hPModel;
    private IGetHitEventProvider _getHitEventProvider;

    void Start()
    {
        _getHitEventProvider = _enemy.GetComponent<IGetHitEventProvider>();
        _hPModel = this.GetComponent<HPModel>();

        _getHitEventProvider.GetHit
            .Where(getHit => getHit == true)
            .Subscribe(_ => _hPModel.GetDamage(10));
        
    }
}
