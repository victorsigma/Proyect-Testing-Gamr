using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
	private Slider slider;

	void Start()
	{
		slider = gameObject.GetComponent<Slider>();
	}

	public void ChangeMaxLife(float maxLife)
	{
		slider.maxValue = maxLife;
	}

	public void ChangeCurrentLife(float amountLife)
	{
		slider.value = amountLife;

		Transform Amount = transform.Find("Amount");
		if (Amount != null)
		{
			TMP_Text textMeshPro = Amount.GetComponent<TMP_Text>();
			if (textMeshPro != null)
			{
				textMeshPro.text = "" + amountLife;
			}
		}
	}

	public void InitLifeBar(int amountLife)
	{
		ChangeMaxLife(GameManager.instance.playerHealthMax);
		ChangeCurrentLife(amountLife);
		Transform Amount = transform.Find("Amount");
		if (Amount != null)
		{
			TMP_Text textMeshPro = Amount.GetComponent<TMP_Text>();
			if (textMeshPro != null)
			{
				textMeshPro.text = "" + amountLife;
			}
		}
	}
}
