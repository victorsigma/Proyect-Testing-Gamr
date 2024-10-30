using System.Collections.Generic;
using UnityEngine;

public class TipsManager : MonoBehaviour
{
	// Estructura de datos para almacenar los tips
	[System.Serializable]
	public class InventoryControls
	{
		public ControllerTips controller;
		public KeyboardMouseTips keyboardMouse;
	}

	[System.Serializable]
	public class ControllerTips
	{
		public string openCloseInventory;
		public string interactSlots;
		public string changeSection;
		public string confirmAction;
		public string hotbarNavigation;
		public string useItem;
	}

	[System.Serializable]
	public class KeyboardMouseTips
	{
		public string openCloseInventory;
		public string interactSlots;
		public string changeSection;
		public string confirmAction;
		public string hotbarNavigation;
		public string switchHotbarSlot;
		public string useItem;
	}

	[System.Serializable]
	public class LoadingScreenTips
	{
		public InventoryControls inventoryControls;
	}

	[System.Serializable]
	public class TipsData
	{
		public LoadingScreenTips loadingScreenTips;
	}

	// JSON con los consejos
	public TextAsset jsonFile;

	private TipsData tipsData;

	// Al iniciar, deserializa el JSON
	void Start()
	{
		tipsData = JsonUtility.FromJson<TipsData>(jsonFile.text);
	}

	// Función para obtener un tip aleatorio
	public string GetRandomTip()
	{
		List<string> tips = new List<string>();

		if (GameGlobals.lastInput == "joystick")
		{
			tips.Add(tipsData.loadingScreenTips.inventoryControls.controller.openCloseInventory);
			tips.Add(tipsData.loadingScreenTips.inventoryControls.controller.interactSlots);
			tips.Add(tipsData.loadingScreenTips.inventoryControls.controller.changeSection);
			tips.Add(tipsData.loadingScreenTips.inventoryControls.controller.confirmAction);
			tips.Add(tipsData.loadingScreenTips.inventoryControls.controller.hotbarNavigation);
			tips.Add(tipsData.loadingScreenTips.inventoryControls.controller.useItem);
		}
		else if (GameGlobals.lastInput == "keyboard")
		{
			tips.Add(tipsData.loadingScreenTips.inventoryControls.keyboardMouse.openCloseInventory);
			tips.Add(tipsData.loadingScreenTips.inventoryControls.keyboardMouse.interactSlots);
			tips.Add(tipsData.loadingScreenTips.inventoryControls.keyboardMouse.changeSection);
			tips.Add(tipsData.loadingScreenTips.inventoryControls.keyboardMouse.confirmAction);
			tips.Add(tipsData.loadingScreenTips.inventoryControls.keyboardMouse.hotbarNavigation);
			tips.Add(tipsData.loadingScreenTips.inventoryControls.keyboardMouse.switchHotbarSlot);
			tips.Add(tipsData.loadingScreenTips.inventoryControls.keyboardMouse.useItem);
		}

		// Selecciona un tip aleatorio
		if (tips.Count > 0)
		{
			int randomIndex = Random.Range(0, tips.Count);
			return tips[randomIndex];
		}

		return "No tips available.";
	}
}
