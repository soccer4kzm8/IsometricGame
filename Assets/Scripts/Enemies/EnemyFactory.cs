using UnityEngine;

public abstract class EnemyFactory : MonoBehaviour
{
    public abstract IEnemyCore GetEnemy(Vector3 positon);
}
