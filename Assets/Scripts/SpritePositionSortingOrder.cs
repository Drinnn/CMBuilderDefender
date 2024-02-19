using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour
{
    [SerializeField] private float precisionMultiplier;
    [SerializeField] private bool runOnlyOnce;
    
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        _spriteRenderer.sortingOrder = (int) (-transform.position.y * precisionMultiplier);
        
        if (runOnlyOnce)
        {
            Destroy(this);
        }
    }
}
