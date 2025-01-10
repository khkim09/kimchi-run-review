using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public float scrollSpeed;

    [Header("References")]
    [SerializeField] public MeshRenderer meshRenderer;

    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * GameManager.GM.ModifyGameSpeed() * 0.1f * Time.deltaTime, 0);
    }
}
