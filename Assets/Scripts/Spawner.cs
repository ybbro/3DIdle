using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // 해당 지역에서 스폰되는 적의 프리팹
    [SerializeField] GameObject enemyPrefab;

    // 스폰 범위
    [SerializeField] Vector2[] spawnRange;

    // 스폰 딜레이
    [SerializeField] float spawnDelay;

    // 해당 사냥터에 존재할 수 있는 최대 적의 수
    [SerializeField] int monsterMax;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, spawnDelay);
    }

    void SpawnEnemy()
    {
        // 해당 사냥터의 최대 적 수보다 적을 때만 생성
        if (transform.childCount < monsterMax)
        {
            // 스폰 지역 내에서 랜덤한 지역에서 스폰 >> 실제로는 플레이어 위치/오브젝트 위치 등을 고려하여 그 부분은 제하여야 함..
            Vector3 nextSpawnPoint = new Vector3(Random.Range(spawnRange[0].x, spawnRange[1].x), 0, Random.Range(spawnRange[0].y, spawnRange[1].y));
            // 적을 생성
            Instantiate(enemyPrefab, nextSpawnPoint, Quaternion.identity, transform);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 center = new Vector3(spawnRange[0].x + spawnRange[1].x, 0, spawnRange[0].y + spawnRange[1].y);
        Vector3 size = new Vector3(Mathf.Abs(spawnRange[0].x) + Mathf.Abs(spawnRange[1].x), 0.1f, Mathf.Abs(spawnRange[0].y) + Mathf.Abs(spawnRange[1].y));
        Gizmos.DrawCube(center, size);
    }
}
