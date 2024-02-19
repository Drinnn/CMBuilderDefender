using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectionUI : MonoBehaviour
{
    [SerializeField] private Transform buttonTemplate;
    [SerializeField] private float offsetBetweenButtons;

    private Dictionary<BuildingTypeSO, Transform> _buttonTransformDictionary; 

    private void Awake()
    {
        _buttonTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();
        
        buttonTemplate.gameObject.SetActive(false);
        
        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));

        var index = 0;
        foreach (var buildingType in buildingTypeList.list)
        {
            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);

            buttonTransform.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(offsetBetweenButtons * index, 0);
            
            buttonTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;
            
            buttonTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });
            
            _buttonTransformDictionary[buildingType] = buttonTransform;

            index++;
        }
    }

    private void Start()
    {
        UpdateActiveBuildingTypeButton();
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActivebuildingTypeChangedEventArgs e)
    {
        UpdateActiveBuildingTypeButton();
    }

    private void UpdateActiveBuildingTypeButton()
    {
        foreach (var buildingType in _buttonTransformDictionary.Keys)
        {
            Transform buttonTransform = _buttonTransformDictionary[buildingType];
            buttonTransform.Find("selected").gameObject.SetActive(false);
        }

        var activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        if (activeBuildingType == null) return;
        _buttonTransformDictionary[activeBuildingType].Find("selected").gameObject.SetActive(true);
;    }
}
