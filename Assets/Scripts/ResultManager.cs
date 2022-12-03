using UniRx;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private GameStateManager _gameStateManager;
    private Canvas _resultCanvas;

    private void Start()
    {
        _resultCanvas = this.GetComponent<Canvas>();
        _gameStateManager
            .State
            .Where(state => state == GameState.Result)
            .Subscribe(_ => _resultCanvas.enabled = true)
            .AddTo(this);
    }
}
