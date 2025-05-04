using UnityEngine;

public class IsoSorting : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        void LateUpdate()
        {
            spriteRenderer.sortingOrder = Mathf.RoundToInt(-(transform.position.y) * 100);
        }
    }
}