using Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float zoomAmount;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    [SerializeField] private float zoomSpeed;
    
    private float _ortographicSize;
    private float _targetOrtographicSize;
    
    private void Start()
    {
        _ortographicSize = virtualCamera.m_Lens.OrthographicSize;
        _targetOrtographicSize = _ortographicSize;
    }

    private void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(x, y).normalized;
        transform.position += moveDirection * (moveSpeed * Time.deltaTime);
    }

    private void HandleZoom()
    {
        _targetOrtographicSize += Input.mouseScrollDelta.y * zoomAmount;
        _targetOrtographicSize = Mathf.Clamp(_targetOrtographicSize, minZoom, maxZoom);
        _ortographicSize = Mathf.Lerp(_ortographicSize, _targetOrtographicSize, Time.deltaTime * zoomSpeed);
        virtualCamera.m_Lens.OrthographicSize = _ortographicSize;
    }
}
