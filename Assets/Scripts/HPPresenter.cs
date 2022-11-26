using UnityEngine;
using UniRx;

public class HPPresenter : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private HPModel _hPModel;
    private HPView _hpView;
    private IGetHitEventProvider _getHitEventProvider;

    void Start()
    {
        _getHitEventProvider = _player.GetComponent<IGetHitEventProvider>();
        _hPModel = this.GetComponent<HPModel>();
        _hpView = this.GetComponent<HPView>();

        _getHitEventProvider.GetHit
            .Where(getHit => getHit == true)
            .Subscribe(_ => _hPModel.GetDamage());
        _hPModel.HP
            .Subscribe(hp => _hpView.SetGuage(_hPModel.maxHP, hp)).AddTo(this);
        // test
    }
}
