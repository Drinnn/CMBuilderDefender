using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    [SerializeField] private float maxConstructionRadius;

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
            Vector3 mouseWorldPosition = Utils.GetMouseWorldPosition();
            if (CanSpawnBuilding(_buildingType, mouseWorldPosition))
            {
                PlaceBuilding(mouseWorldPosition);
            }
        }
        else if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject() && _buildingType != null)
        {
            SetActiveBuildingType(null);
        }
    }

    private void PlaceBuilding(Vector3 position)
    {
        Instantiate(_buildingType.prefab, position, Quaternion.identity);
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

    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position)
    {
        var boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

        var collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

        bool isAreaClear = collider2DArray.Length == 0;
        if (!isAreaClear)
        {
            return false;
        }
        
        

        collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minConstructionInBetweenRadius);
        foreach (var collider2D in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                if (buildingTypeHolder.buildingType == buildingType)
                {
                    return false;
                }
            }
        }
        
        collider2DArray = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
        foreach (var collider2D in collider2DArray)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                return true;
            }
        }

        return false;
    }
}