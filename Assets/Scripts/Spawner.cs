using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // �ش� �������� �����Ǵ� ���� ������
    [SerializeField] GameObject enemyPrefab;

    // ���� ����
    [SerializeField] Vector2[] spawnRange;

    // ���� ������
    [SerializeField] float spawnDelay;

    // �ش� ����Ϳ� ������ �� �ִ� �ִ� ���� ��
    [SerializeField] int monsterMax;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, spawnDelay);
    }

    void SpawnEnemy()
    {
        // �ش� ������� �ִ� �� ������ ���� ���� ����
        if (transform.childCount < monsterMax)
        {
            // ���� ���� ������ ������ �������� ���� >> �����δ� �÷��̾� ��ġ/������Ʈ ��ġ ���� ����Ͽ� �� �κ��� ���Ͽ��� ��..
            Vector3 nextSpawnPoint = new Vector3(Random.Range(spawnRange[0].x, spawnRange[1].x), 0, Random.Range(spawnRange[0].y, spawnRange[1].y));
            // ���� ����
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
