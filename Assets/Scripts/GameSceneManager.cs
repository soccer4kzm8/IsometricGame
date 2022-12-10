using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _restartButton;

    private void Start()
    {
        _quitButton.onClick.AddListener(() => SceneManager.LoadScene("StartScene"));
        _restartButton.onClick.AddListener(() => SceneManager.LoadScene("GameScene"));
    }
}
