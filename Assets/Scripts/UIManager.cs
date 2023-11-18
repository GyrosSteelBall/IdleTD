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

    private Unit selectedUnit;
    [SerializeField] private Transform hotbarPanelTransform;
    [SerializeField] private GameObject hotbarItemPrefab;
    [SerializeField] private List<Unit> availableUnits;
    public UnitPlacementManager unitPlacementManager;

    private List<HotbarItem> hotbarItems = new List<HotbarItem>();
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private Button[] upgradeButtons; // Assume you've set this up to match the upgrade paths in the unit.
    [SerializeField] private TextMeshProUGUI[] upgradeNamesTexts;      // Array of UI Texts to show upgrade names.
    [SerializeField] private TextMeshProUGUI[] upgradeDescriptionsTexts; // Array of UI Texts to show upgrade descriptions.
    [SerializeField] private TextMeshProUGUI[] upgradeCostTexts;          // Array of UI Texts to show costs.

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
        upgradePanel.SetActive(false);
        PopulateHotbar(availableUnits);
    }

    private void Update()
    {
        goldText.text = $"Gold: {GameManager.Instance.Gold}";
        UpdateHotbarItems();

        UpdateUpgradeButtonInteractability();
    }

    public void DisplayUpgradesForUnit(Unit unit)
    {
        // Hide the upgrade panel if there's no selected unit
        if (unit == null)
        {
            upgradePanel.SetActive(false);
            return;
        }

        selectedUnit = unit;
        upgradePanel.SetActive(true);

        // Loop through all potential upgrade buttons
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            // Remove any existing onClick listeners to prevent stacking listeners
            upgradeButtons[i].onClick.RemoveAllListeners();

            // Check if there is a corresponding upgrade path and level
            if (i < unit.UpgradePaths.Count && unit.UpgradeLevelsPerPath[i] < unit.UpgradePaths[i].upgradeSteps.Count)
            {
                UpgradeEffect currentStep = unit.UpgradePaths[i].upgradeSteps[unit.UpgradeLevelsPerPath[i]];
                UpgradeEffect effect = currentStep;

                upgradeButtons[i].gameObject.SetActive(true);

                // Update UI Elements with the current upgrade's information
                upgradeNamesTexts[i].text = effect.effectName;
                upgradeDescriptionsTexts[i].text = currentStep.description; // Assumes description is in UpgradeStep
                upgradeCostTexts[i].text = effect.cost.ToString();

                // Set the button interactability based on upgrade availability
                upgradeButtons[i].interactable = unit.IsUpgradeAvailable(i);

                // Bind the OnUpgradeButtonClicked method to the button with the correct path index
                int pathIndex = i;
                upgradeButtons[i].onClick.AddListener(() => OnUpgradeButtonClicked(pathIndex));
            }
            else
            {
                // Maximum upgrades reached for this path
                upgradeNamesTexts[i].text = "Upgrade Complete";
                upgradeDescriptionsTexts[i].text = "All upgrades purchased for this path";
                upgradeCostTexts[i].text = ""; // Clear the cost text
                upgradeButtons[i].interactable = false; // Button no longer needs to be interactable
            }
        }

        UpdateUpgradeButtonInteractability();
    }

    public void OnUpgradeButtonClicked(int pathIndex)
    {
        // Call ApplyUpgrade from Unit and pass the path index.
        if (selectedUnit != null && selectedUnit.IsUpgradeAvailable(pathIndex))
        {
            selectedUnit.ApplyUpgrade(pathIndex);
            DisplayUpgradesForUnit(selectedUnit);
        }
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
        DisplayUpgradesForUnit(unit);
    }

    public void UpdateUpgradeButtonInteractability()
    {
        if (selectedUnit == null) return;

        // Update the interactability of the upgrade buttons for the selected unit
        for (int i = 0; i < selectedUnit.UpgradePaths.Count; i++)
        {
            if (i < upgradeButtons.Length)
            {
                upgradeButtons[i].interactable = selectedUnit.IsUpgradeAvailable(i);
            }
        }
    }

    public void DeselectUnit()
    {
        selectedUnit?.ShowRange(false);
        selectedUnit = null;
        upgradePanel.SetActive(false);
    }

    private class HotbarItem
    {
        public GameObject GameObject { get; set; }
        public Button Button { get; set; }
        public Unit Unit { get; set; }
    }
}