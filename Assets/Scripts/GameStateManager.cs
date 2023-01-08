using UniRx;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private PlayerStateManager _playerStateManager;
    #endregion SerializeField

    #region private変数
    /// <summary>
    /// ゲームステートを監視
    /// </summary>
    private readonly ReactiveProperty<GameState> _state = new ReactiveProperty<GameState>(GameState.Playing);
    #endregion private変数

    #region public変数
    /// <summary>
    /// ゲームステート
    /// </summary>
    public IReadOnlyReactiveProperty<GameState> State => _state;
    #endregion public変数

    private void Start()
    {
        _state.AddTo(this);

        // プレイヤーが死亡したら、リザルト画面を表示
        _playerStateManager.State
            .Where(state => state == PlayerState.Dead)
            .Subscribe(_ =>
            {
                _state.Value = GameState.Result;
            })
            .AddTo(this);
    }
}
