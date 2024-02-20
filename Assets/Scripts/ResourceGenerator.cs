using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private ResourceGeneratorData _resourceGeneratorData;
    private float _timer;
    private float _timeToGenerateResource;

    private void Awake()
    {
        _resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
        _timeToGenerateResource = _resourceGeneratorData.timeToGenerateResource;
        _timer = 1f;
    }

    private void Start()
    {
        int nearbyResourceAmount = 0;

        var collider2DArray =
            Physics2D.OverlapCircleAll(transform.position, _resourceGeneratorData.resourceNodeDetectionRadius);
        foreach (var collider2D in collider2DArray)
        {
            var resourceNode = collider2D.GetComponent<ResourceNode>();
            if (resourceNode != null)
            {
                if (resourceNode.resourceType == _resourceGeneratorData.resourceType)
                {
                    nearbyResourceAmount++;
                }
            }
        }

        nearbyResourceAmount =
            Mathf.Clamp(nearbyResourceAmount, 0, _resourceGeneratorData.maxResourceNodeEffectiveAmount);

        if (nearbyResourceAmount == 0)
        {
            enabled = false;
        }
        else
        {
            _timeToGenerateResource = (_resourceGeneratorData.timeToGenerateResource / 2f) +
                                      _resourceGeneratorData.timeToGenerateResource *
                                      (1 - (float)nearbyResourceAmount /
                                          _resourceGeneratorData.maxResourceNodeEffectiveAmount);
        }
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            ResourceManager.Instance.AddResource(_resourceGeneratorData.resourceType, 1);
            _timer += _timeToGenerateResource;
        }
    }
}