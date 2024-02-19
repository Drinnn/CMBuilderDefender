using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    public class OnActivebuildingTypeChangedEventArgs : EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }

    public event EventHandler<OnActivebuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

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
        Vector3 mouseWorldPosition = Utils.GetMouseWorldPosition();
        Instantiate(_buildingType.prefab, mouseWorldPosition, Quaternion.identity);
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

        OnActiveBuildingTypeChanged?.Invoke(this,
            new OnActivebuildingTypeChangedEventArgs { activeBuildingType = _buildingType });
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return _buildingType;
    }
}