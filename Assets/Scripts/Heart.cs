using UnityEngine;

public class Heart : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int heartIndex; // 각 하트를 좌측부터 index 1, 2, 3을 설정

    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite onHeart;
    [SerializeField] private Sprite offHeart;

    void Update()
    {
        if (GameManager.GM.lives >= heartIndex) // "현재 목숨 (lives)"과 "각 heartIndex"를 비교
        {
            spriteRenderer.sprite = onHeart;
        }
        else // lives가 줄어 들면 sprite를 변경 (목숨에 대한 UI 적용)
        {
            spriteRenderer.sprite = offHeart;
        }
    }
}
