using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int minSpawnDelay;
    [SerializeField] private int maxSpawnDelay;

    [Header("References")]
    [SerializeField] private GameObject[] gameObjects;

    void OnEnable()
    {
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void Spawn()
    {
        GameObject randomObject = gameObjects[Random.Range(0, gameObjects.Length)]; // spawn할 object, gameObjects list에서 랜덤 선택
        Instantiate(randomObject, transform.position, Quaternion.identity); // randomObject 인스턴스화
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay)); // 주기 random 생성
    }
}
