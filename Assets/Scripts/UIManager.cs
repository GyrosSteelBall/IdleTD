using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    private TextMeshProUGUI goldText; // UI text showing current gold.
    [SerializeField]
    private Button upgradeButton; // Button used to upgrade the selected unit.
    [SerializeField]
    private TextMeshProUGUI upgradeButtonText; // Text on the upgrade button displaying cost.

    private Unit selectedUnit; // The currently selected unit.
    [SerializeField] private Transform hotbarPanelTransform; // Parent transform of the hotbar items
    [SerializeField] private GameObject hotbarItemPrefab; // The UI prefab that represents a unit in the hotbar
    [SerializeField] private List<Unit> availableUnits; //Configure this for now, later needs to be dynamic based on player's team
    public UnitPlacementManager unitPlacementManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
        PopulateHotbar(availableUnits);
    }

    void Update()
    {
        // Update the UI for the gold display.
        goldText.text = "Gold: " + GameManager.instance.gold;

        // Update the interactability and text of the upgrade button.
        if (selectedUnit != null)
        {
            upgradeButtonText.text = $"Upgrade ({selectedUnit.GetUpgradeCost()} Gold)"; // Show the cost of the upgrade.
        }

        if (selectedUnit != null && selectedUnit.CanUpgrade())
        {
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeButton.interactable = false; // Make sure we can't interact with the button if we can't upgrade.
        }
    }

    public void PopulateHotbar(List<Unit> availableUnits)
    {
        foreach (Unit unit in availableUnits)
        {
            GameObject hotbarItem = Instantiate(hotbarItemPrefab, hotbarPanelTransform);
            hotbarItem.transform.Find("UnitSprite").GetComponent<Image>().sprite = unit.icon;
            hotbarItem.GetComponent<Button>().onClick.AddListener(() => unitPlacementManager.StartPlacingUnit(unit));
        }
    }

    public void SetSelectedUnit(Unit unit)
    {
        selectedUnit?.ShowRange(false); // Safe-call operator in case selectedUnit is null.
        selectedUnit = unit;
        selectedUnit.ShowRange(true);
        UpdateUIForSelection(unit);
    }

    public void UpdateUIForSelection(Unit unit)
    {
        // Here you update the whole UI with the information of the new selected unit.
        // Update the text or other UI elements you might have for your selection.
    }

    public void OnUpgradeButtonClick()
    {
        if (selectedUnit?.CanUpgrade() ?? false)
        {
            selectedUnit?.UpgradeUnit();
            UpdateUIForSelection(selectedUnit); // Refresh UI after upgrade to reflect changes.
        }
    }

    public void DeselectUnit()
    {
        if (selectedUnit != null)
        {
            selectedUnit.ShowRange(false); // Hide the range of the previously selected unit
            selectedUnit = null; // Clear the currently selected unit

            // Optionally clear the upgrade button or other selection-specific UI elements
            upgradeButton.interactable = false;
            upgradeButtonText.text = "Upgrade";
        }
    }
}