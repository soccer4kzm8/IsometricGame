using System.Collections.Generic;
using UnityEngine;

public class SlimeFactory : EnemyFactory
{
    [SerializeField] private EnemyCore slimePrefab;
    public override IEnemyCore GetEnemy(Vector3 position)
    {
        GameObject instance = Instantiate(slimePrefab.gameObject, position, Quaternion.identity);
        EnemyCore slime = instance.GetComponent<EnemyCore>();
        slime.Initialize();
        return slime;
    }
}
