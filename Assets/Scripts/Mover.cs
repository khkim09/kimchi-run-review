using Unity.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    void Update()
    {
        transform.position += Vector3.left * GameManager.GM.ModifyGameSpeed() * 0.8f * Time.deltaTime; // 시간 단위 움직임
    }
}
