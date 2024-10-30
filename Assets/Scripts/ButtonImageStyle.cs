using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{

	public Color hoverTextColor;
	public Color defaultTextColor;

	// Start is called before the first frame update
	void Start()
	{
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Transform ButtonText = transform.Find("ButtonText");

		if (ButtonText != null)
		{
			TMP_Text textMeshPro = ButtonText.GetComponent<TMP_Text>();
			if (textMeshPro != null)
			{
				textMeshPro.color = hoverTextColor;
			}
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Transform ButtonText = transform.Find("ButtonText");

		if (ButtonText != null)
		{
			TMP_Text textMeshPro = ButtonText.GetComponent<TMP_Text>();
			if (textMeshPro != null)
			{
				textMeshPro.color = defaultTextColor;
			}
		}
	}
	
	
	public void OnSelect(BaseEventData eventData)
    {
		Transform ButtonText = transform.Find("ButtonText");

		if (ButtonText != null)
		{
			TMP_Text textMeshPro = ButtonText.GetComponent<TMP_Text>();
			if (textMeshPro != null)
			{
				textMeshPro.color = hoverTextColor;
			}
		}
    }

    public void OnDeselect(BaseEventData eventData)
    {
		Transform ButtonText = transform.Find("ButtonText");

		if (ButtonText != null)
		{
			TMP_Text textMeshPro = ButtonText.GetComponent<TMP_Text>();
			if (textMeshPro != null)
			{
				textMeshPro.color = defaultTextColor;
			}
		}
    }
}
