using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int minSpawnDelay;
    [SerializeField] private int maxSpawnDelay;
    [SerializeField] private int[] scoreThresholds = { 500, 1200, 2000 };
    [SerializeField] private int currentThresholdIndex = 0;

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

    void ChangeSpawnDelay(int threshold)
    {
        switch(threshold)
        {
            case 500:
                minSpawnDelay--;
                maxSpawnDelay--;
                break;
            
            case 1200:
                maxSpawnDelay--;
                break;
            
            case 2000:
                minSpawnDelay--;
                maxSpawnDelay--;
                break;
        }
    }

    void Update()
    {
        int score = GameManager.GM.CalculateScore();

        if (currentThresholdIndex < scoreThresholds.Length && score >= scoreThresholds[currentThresholdIndex]) // 현재 점수 기준 spawn delay 시간 조절
        {
            ChangeSpawnDelay(scoreThresholds[currentThresholdIndex]);
            currentThresholdIndex++;
        }
    }
}
