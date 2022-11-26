using UnityEngine;
using UnityEngine.UI;

public class HPView : MonoBehaviour
{
    /// <summary>
    /// HP�Q�[�W
    /// </summary>
    private Slider _hpGuage;

    private void Start()
    {
        _hpGuage = this.GetComponent<Slider>();
    }

    /// <summary>
    /// HP�Q�[�W�̐ݒ�
    /// </summary>
    /// <param name="maxHP">�ő�HP</param>
    /// <param name="hp">�c���Ă���HP</param>
    public void SetGuage(int maxHP, float hp)
    {
        _hpGuage.value = hp / maxHP;
    }
}
