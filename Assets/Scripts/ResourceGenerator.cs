using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private BuildingTypeSO _buildingType;
    private float _timer;
    private float _timeToGenerateResource;

    private void Awake()
    {
        _buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        _timeToGenerateResource = _buildingType.resourceGeneratorData.timeToGenerateResource;
        _timer = 1f;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            ResourceManager.Instance.AddResource(_buildingType.resourceGeneratorData.resourceType, 1);
            _timer += _timeToGenerateResource;
        }
    }
}
