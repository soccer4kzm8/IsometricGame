using UnityEngine;

public class HPController : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private void Update()
    {
        _canvas.transform.rotation = Camera.main.transform.rotation;
    }
}
