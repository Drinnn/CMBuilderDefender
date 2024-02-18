using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private GameObject woodHarvesterPrefab;

    private Camera _camera;
    
    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceBuilding();
        }
    }
    
    private void PlaceBuilding()
    {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        Instantiate(woodHarvesterPrefab, mouseWorldPosition, Quaternion.identity);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}
