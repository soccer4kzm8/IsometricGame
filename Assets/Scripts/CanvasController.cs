using UnityEngine;

public class CanvasController : MonoBehaviour
{
    private void Update()
    {
        this.transform.rotation = Camera.main.transform.rotation;
    }
}
