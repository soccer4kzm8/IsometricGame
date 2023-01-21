using UnityEngine;

public class TurtleShellFactory : EnemyFactory
{
    [SerializeField] private EnemyCore turtleShellPrefab;
    public override IEnemyCore GetEnemy(Vector3 position)
    {
        GameObject instance = Instantiate(turtleShellPrefab.gameObject, position, Quaternion.identity);
        EnemyCore slime = instance.GetComponent<EnemyCore>();
        slime.Initialize();
        return slime;
    }
}
