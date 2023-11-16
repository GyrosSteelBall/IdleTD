using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
            }
            return _instance;
        }
    }

    [SerializeField]
    private TextMeshProUGUI goldText;
    [SerializeField]
    private Button upgradeButton;
    [SerializeField]
    private TextMeshProUGUI upgradeButtonText;

    private Unit selectedUnit;
    [SerializeField] private Transform hotbarPanelTransform;
    [SerializeField] private GameObject hotbarItemPrefab;
    [SerializeField] private List<Unit> availableUnits;
    public UnitPlacementManager unitPlacementManager;

    private List<HotbarItem> hotbarItems = new List<HotbarItem>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        upgradeButton.onClick.AddListener(OnUpgradeButtonClick);

        PopulateHotbar(availableUnits);
    }

    private void Update()
    {
        goldText.text = $"Gold: {GameManager.Instance.Gold}";

        var canUpgrade = selectedUnit?.CanUpgrade() == true;
        upgradeButton.interactable = canUpgrade;

        if (canUpgrade)
        {
            upgradeButtonText.text = $"Upgrade ({selectedUnit.GetUpgradeCost()} Gold)";
        }
        else
        {
            upgradeButtonText.text = "Upgrade";
        }

        UpdateHotbarItems();
    }

    private void UpdateHotbarItems()
    {
        foreach (var hotbarItem in hotbarItems)
        {
            hotbarItem.Button.interactable = GameManager.Instance.Gold >= hotbarItem.Unit.PlacementCost;
        }
    }

    public void PopulateHotbar(List<Unit> availableUnits)
    {
        // Clear existing hotbar items
        hotbarItems.ForEach(item => Destroy(item.GameObject));
        hotbarItems.Clear();

        var sortedUnits = availableUnits.OrderBy(unit => unit.PlacementCost);

        foreach (Unit unit in sortedUnits)
        {
            GameObject hotbarItemObject = Instantiate(hotbarItemPrefab, hotbarPanelTransform);

            var hotbarItem = new HotbarItem
            {
                GameObject = hotbarItemObject,
                Button = hotbarItemObject.GetComponent<Button>(),
                Unit = unit
            };

            hotbarItem.Button.onClick.AddListener(() => unitPlacementManager.StartPlacingUnit(unit));
            hotbarItemObject.transform.Find("UnitSprite").GetComponent<Image>().sprite = unit.Icon;
            hotbarItemObject.transform.Find("GoldText").GetComponent<TextMeshProUGUI>().text = unit.PlacementCost.ToString();

            hotbarItems.Add(hotbarItem);
        }
    }

    public void SetSelectedUnit(Unit unit)
    {
        selectedUnit?.ShowRange(false);
        selectedUnit = unit;
        selectedUnit?.ShowRange(true);
        UpdateUIForSelection(unit);
    }

    public void UpdateUIForSelection(Unit unit)
    {
        // Update UI
    }

    public void OnUpgradeButtonClick()
    {
        if (selectedUnit?.CanUpgrade() == true)
        {
            selectedUnit.UpgradeUnit();
            UpdateUIForSelection(selectedUnit);
        }
    }

    public void DeselectUnit()
    {
        selectedUnit?.ShowRange(false);
        selectedUnit = null;

        upgradeButton.interactable = false;
        upgradeButtonText.text = "Upgrade";
    }

    private class HotbarItem
    {
        public GameObject GameObject { get; set; }
        public Button Button { get; set; }
        public Unit Unit { get; set; }
    }
}