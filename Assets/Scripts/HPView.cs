using UnityEngine;
using UnityEngine.UI;

public class HPView : MonoBehaviour
{
    private Slider _hpGuage;
    private void Start()
    {
        _hpGuage = this.GetComponent<Slider>();
    }

    public void SetGuage(int maxHP, float hp, int damage)
    {
        _hpGuage.value = (hp - damage) / maxHP;
    }
}
