using UnityEngine;
using UniRx;

public class EnemyStateManager : MonoBehaviour
{
    #region private変数
    /// <summary>
    /// 敵ステートを監視
    /// </summary>
    private readonly ReactiveProperty<EnemyState> _state = new ReactiveProperty<EnemyState>(EnemyState.Alive);
    #endregion private変数

    #region public変数
    /// <summary>
    /// 敵ステート
    /// </summary>
    public IReadOnlyReactiveProperty<EnemyState> State => _state;
    #endregion public変数

    private void Start()
    {
        _state.AddTo(this);

        var hpModel = this.GetComponent<HPModel>();
        // 体力が0以下になったら、敵ステートをDeadに変更
        hpModel.HP
            .Where(hp => hp <= 0)
            .Skip(1)
            .Take(1)
            .Subscribe(_ =>
            {
                _state.Value = EnemyState.Dead;
            })
            .AddTo(this);
    }
}
