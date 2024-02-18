using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private Camera _camera;
    private BuildingTypeListSO _buildingTypeList;
    private BuildingTypeSO _buildingType;
    
    private void Start()
    {
        _camera = Camera.main;
        
        _buildingTypeList = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));
        
        _buildingType = _buildingTypeList.list[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceBuilding();
        }
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            _buildingType = _buildingTypeList.list[0];
        }
        
        if (Input.GetKeyDown(KeyCode.Y))
        {
            _buildingType = _buildingTypeList.list[1];
        }
    }
    
    private void PlaceBuilding()
    {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        Instantiate(_buildingType.prefab, mouseWorldPosition, Quaternion.identity);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}
