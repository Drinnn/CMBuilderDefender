using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    [SerializeField] private Transform resourceTemplate;
    [SerializeField] private float offsetBetweenResources = 170f;
    
    private ResourceTypeListSO _resourceTypeList;
    private Dictionary<ResourceTypeSO, Transform> _resourceTransformDictionary;
    
    private void Awake()
    {
        resourceTemplate.gameObject.SetActive(false);
        
        _resourceTypeList = Resources.Load<ResourceTypeListSO>(nameof(ResourceTypeListSO));
        _resourceTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();
        
        var index = 0;
        foreach (var resourceType in _resourceTypeList.list)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);

            resourceTransform.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(offsetBetweenResources * index, 0);
            
            resourceTransform.Find("image").GetComponent<Image>().sprite = resourceType.sprite;

            _resourceTransformDictionary[resourceType] = resourceTransform;
            
            index++;
        }
    }

    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
        UpdateResourcesAmount();
    }

    private void ResourceManager_OnResourceAmountChanged(object sender, EventArgs e)
    {
        UpdateResourcesAmount();
    }

    private void UpdateResourcesAmount()
    {
        foreach (ResourceTypeSO resourceType in _resourceTypeList.list)
        {
            Transform resourceTransform = _resourceTransformDictionary[resourceType];
            
            int resourcesAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
            resourceTransform.Find("amount").GetComponent<TextMeshProUGUI>().text = resourcesAmount.ToString();
        }
    }
}
