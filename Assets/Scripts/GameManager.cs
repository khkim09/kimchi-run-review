using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Start,
    Playing,
    Finish
}

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public static GameManager GM;
    [SerializeField] public GameState gameState;
    [SerializeField] public int lives = 3; // Player.cs 에서 GameManager.cs로 lives 변수 이동
    [SerializeField] public float gameStartTime; // Player 생존 시간 계산을 위해 지정
    [SerializeField] private int currentScore;

    [Header("References")]
    [SerializeField] public GameObject StartUI;
    [SerializeField] public GameObject PlayingUI;
    [SerializeField] public GameObject FinishUI;
    [SerializeField] private TMP_Text newScore;
    [SerializeField] private TMP_Text highScore;
    [SerializeField] public GameObject enemySpawner;
    [SerializeField] public GameObject foodSpawner;
    [SerializeField] public GameObject goldSpawner;

    void Awake()
    {
        if (GM == null) // GameManager 지정
        {
            GM = this;
        }
    }

    void Start()
    {
        gameState = GameState.Start; // 게임 시작 시 gameState = GameState.Start로 지정
        StartUI.SetActive(true); // StartUI 활성화
    }

    public int CalculateScore() // 현재 점수 계산
    {
        return (int)(10 * (Time.time - gameStartTime)); // (현재 시간 - 게임 시작 시간) x 10 으로 현재 점수 계산
    }

    void SaveHighScore() // 최고 점수 사용자 disk에 저장
    {
        int score = CalculateScore();
        currentScore = score;
        int currentHighScore = PlayerPrefs.GetInt("highScore");

        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("highScore", score); // 사용자 hard drive에 직접 저장
            PlayerPrefs.Save();
        }
    }

    public int GetHighScore() // 최고 점수 가져오기
    {
        return PlayerPrefs.GetInt("highScore");
    }

    public float ModifyGameSpeed() // game speed 조절
    {
        if (gameState != GameState.Playing) // 게임 실행 중이 아니라면 속도 5 고정
            return 5f;

        float speed = 8f + 0.7f * Mathf.Floor(CalculateScore() / 100f); // 현재 점수에 따른 속도 상승

        return Mathf.Min(speed, 30f); // 최대 속도 30으로 지정
    }

    void Update()
    {
        if (gameState == GameState.Start && Input.GetKeyDown(KeyCode.Space)) // StartUI에서 space 키 입력 시, 게임 시작
        {
            gameState = GameState.Playing; // game 상태 갱신 (Start -> Playing)
            StartUI.SetActive(false); // StartUI 비활성화
            PlayingUI.SetActive(true); // PlayingUI 활성화 (Player 체력, 현재 점수 표기)
            gameStartTime = Time.time; // 게임 시작 시간 지정

            // 각 Spawner 활성화
            enemySpawner.SetActive(true);
            foodSpawner.SetActive(true);
            goldSpawner.SetActive(true);
        }

        if (gameState == GameState.Playing && lives == 0) // gameState = Playing에서 Player가 죽을 경우
        {
            gameState = GameState.Finish; // game 상태 갱신 (Playing -> Finish)
            PlayingUI.SetActive(false);
            FinishUI.SetActive(true); // FinishUI 활성화
            SaveHighScore();
            newScore.text += currentScore; // 현재 점수 갱신
            highScore.text += GetHighScore(); // 최고 점수 기록

            // 각 Spawner 비활성화
            enemySpawner.SetActive(false);
            foodSpawner.SetActive(false);
            goldSpawner.SetActive(false);
        }

        if (gameState == GameState.Finish && Input.GetKeyDown(KeyCode.Space)) // 죽은 상태에서 space 키 입력 시, main scene으로 복귀
        {
            SceneManager.LoadScene("main");
        }
    }

}
