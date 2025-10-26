using UnityEngine;

public class ObstaclesController : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float spawnInterval = 2f; // Kaç saniyede bir engel çýkacak.
    public float spawnX = 12f;
    public float scrollSpeed = 5f;

    void Start()
    {
        InvokeRepeating("SpawnObstacle", 1f, spawnInterval);
    }


    void SpawnObstacle()
    {
        if (!GamePlayManager.gameStarted) return; //Son Eklenen Kod

        int index = Random.Range(0, obstaclePrefabs.Length);
        GameObject prefab = obstaclePrefabs[index];

        Vector3 spawnPosition = new Vector3(spawnX, prefab.transform.position.y, 0);

        GameObject obstacle = Instantiate(prefab, spawnPosition, Quaternion.identity);

        obstacle.AddComponent<ObstacleMove>().speed = scrollSpeed;

        Destroy(obstacle, 10f);
    }
}

public class ObstacleMove : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        if (!GamePlayManager.gameStarted) return; //Son Eklenen Kod

        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
