using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour
{
    [SerializeField] private bool runOnlyOnce;
    [SerializeField] private float positionOffsetY;
    
    private SpriteRenderer _spriteRenderer;
    private float _precisionMultiplier;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _precisionMultiplier = 5f;
    }

    private void LateUpdate()
    {
        _spriteRenderer.sortingOrder = (int) (-(transform.position.y + positionOffsetY) * _precisionMultiplier);
        
        if (runOnlyOnce)
        {
            Destroy(this);
        }
    }
}
