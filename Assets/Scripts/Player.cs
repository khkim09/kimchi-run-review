using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int jumpForce;
    [SerializeField] private bool isJumping;
    [SerializeField] private int jumpCnt = 0;
    [SerializeField] private bool isInvincible = false; // 무적 상태

    [Header("References")]
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Collider2D playerCollider;
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCnt < 2)
        {
            playerRigidBody.linearVelocity = Vector2.zero; // player의 현재 위치에서 점프 적용을 위해 velocity 초기화
            playerRigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpCnt++;
            isJumping = true;
            playerAnimator.SetInteger("state", 1); // animation state 변경 (Run -> Jump)
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") // 땅에 착지
        {
            if (isJumping)
            {
                playerAnimator.SetInteger("state", 2); // state 변경 (Jump -> Land)
            }
            jumpCnt = 0;
            isJumping = false;
        }
    }

    void Death()
    {
        playerAnimator.SetInteger("state", 1); // 마지막 jump animation 수행
        playerCollider.enabled = false; // 'Player' 캐릭터 추락을 위해 collider 해제
        playerAnimator.enabled = false; // 'Player'의 animator도 해제
        playerRigidBody.linearVelocity = Vector2.zero; // 현재 'player'위치에서 마지막 jump motion을 위해 velocity 초기화
        playerRigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // 마지막 jump motion
        
        if (transform.position.y < -7) // 추락 후 gameObject 삭제
        {
            Destroy(gameObject);
        }
    }

    void Heal()
    {
        if (GameManager.GM.lives >= 3) // lives -> GameManager.GM.lives
            return;
        GameManager.GM.lives++;
    }

    void Hit()
    {
        GameManager.GM.lives--;
        if (GameManager.GM.lives == 0)
            Death();
    }

    void StartInvincible()
    {
        isInvincible = true;
        Invoke("EndInvincible", 5.0f);
    }

    void EndInvincible()
    {
        isInvincible = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Food")
        {
            Destroy(collider.gameObject);
            Heal();
        }
        else if (collider.tag == "Enemy")
        {
            if (!isInvincible) // 무적 상태가 아니라면 Hit 호출
            {
                Destroy(collider.gameObject);
                Hit();
            }
        }
        else if (collider.tag == "Golden")
        {
            Destroy(collider.gameObject);
            StartInvincible();
        }
    }
}
