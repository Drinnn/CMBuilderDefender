using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    public event EventHandler OnActiveBuildingTypeChanged;

    private Camera _camera;
    private BuildingTypeSO _buildingType;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && _buildingType != null)
        {
            PlaceBuilding();
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

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        if (buildingType == _buildingType)
        {
            _buildingType = null;
        }
        else
        {
            _buildingType = buildingType;
        }
        
        OnActiveBuildingTypeChanged?.Invoke(this, EventArgs.Empty);
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return _buildingType;
    }
}