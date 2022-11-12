using UniRx;

public class HPModel
{
    /// <summary>
    /// �ő�HP
    /// </summary>
    public readonly int hpMax = 100;

    public IntReactiveProperty hpRP = new IntReactiveProperty();

    public int HP
    {
        get { return hpRP.Value; }
        set { hpRP.Value = value; }
    }
}
