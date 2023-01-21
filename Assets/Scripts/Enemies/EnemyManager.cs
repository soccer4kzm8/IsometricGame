using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    /// <summary>
    /// 敵生成場所
    /// </summary>
    [SerializeField] private Transform[] _enemySpawnPoints;

    /// <summary>
    /// 敵生成工場
    /// </summary>
    [SerializeField] private EnemyFactory[] _enemyFactories;

    /// <summary>
    /// 敵生成インターバル
    /// </summary>
    [SerializeField] private int _enemySpawnInterval;

    private void Start()
    {
        StartCoroutine(EnemySpawnCoroutine());
    }

    public void ResetEnemies()
    {
        StopAllCoroutines();
    }

    /// <summary>
    /// 定期的に敵を生成するコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnemySpawnCoroutine()
    {
        while (true)
        {
            var spawnPoint = _enemySpawnPoints[Random.Range(0, _enemySpawnPoints.Length)];
            var enemy = _enemyFactories[Random.Range(0, _enemyFactories.Length)];
            enemy.GetEnemy(spawnPoint.position);
            yield return new WaitForSeconds(_enemySpawnInterval);
        }
    }
}
