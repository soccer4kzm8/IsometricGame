using UnityEngine;
using UniRx;

public class HPPresenter : MonoBehaviour
{
    private HPModel _hPModel;
    private HPView _hpView;
    void Start()
    {
        _hPModel = new HPModel(100);
        _hpView = this.GetComponent<HPView>();
        _hPModel.hpRP.Subscribe(damage => _hpView.SetGuage(_hPModel.hpMax, _hPModel.HP, damage));
    }
}
