using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

	public Sprite hoverImageLeft; // Imagen para el estado hover
	public Sprite hoverImageCenter; // Imagen para el estado hover
	public Sprite hoverImageRight; // Imagen para el estado hover
	public Sprite defaultImageLeft; // Imagen por defecto
	public Sprite defaultImageCenter; // Imagen por defecto
	public Sprite defaultImageRight; // Imagen por defecto
	
	public Color hoverTextColor;
	public Color defaultTextColor;

	// Start is called before the first frame update
	void Start()
	{
		// Asigna la imagen por defecto a los hijos
		Transform ButtonLeft = transform.Find("ButtonLeft");
		if (ButtonLeft != null)
		{
			Image image = ButtonLeft.GetComponent<Image>();
			if (image != null)
			{
				image.sprite = defaultImageLeft; // Cambia la imagen
			}
		}
		else
		{
			Debug.LogWarning("No se encontró el hijo con el nombre: " + "ButtonLeft");
		}


		Transform ButtonCenter = transform.Find("ButtonCenter");
		if (ButtonCenter != null)
		{
			Image image = ButtonCenter.GetComponent<Image>();
			if (image != null)
			{
				image.sprite = defaultImageCenter; // Cambia la imagen
			}
		}
		else
		{
			Debug.LogWarning("No se encontró el hijo con el nombre: " + "ButtonCenter");
		}

		Transform ButtonRight = transform.Find("ButtonRight");
		if (ButtonRight != null)
		{
			Image image = ButtonRight.GetComponent<Image>();
			if (image != null)
			{
				image.sprite = defaultImageRight; // Cambia la imagen
			}
		}
		else
		{
			Debug.LogWarning("No se encontró el hijo con el nombre: " + "ButtonRight");
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		// Cambia las imágenes al hover
		// Asigna la imagen por defecto a los hijos
		Transform ButtonLeft = transform.Find("ButtonLeft");
		if (ButtonLeft != null)
		{
			Image image = ButtonLeft.GetComponent<Image>();
			if (image != null)
			{
				image.sprite = hoverImageLeft; // Cambia la imagen
			}
		}
		else
		{
			Debug.LogWarning("No se encontró el hijo con el nombre: " + "ButtonLeft");
		}


		Transform ButtonCenter = transform.Find("ButtonCenter");
		if (ButtonCenter != null)
		{
			Image image = ButtonCenter.GetComponent<Image>();
			if (image != null)
			{
				image.sprite = hoverImageCenter; // Cambia la imagen
			}
		}
		else
		{
			Debug.LogWarning("No se encontró el hijo con el nombre: " + "ButtonCenter");
		}

		Transform ButtonRight = transform.Find("ButtonRight");
		if (ButtonRight != null)
		{
			Image image = ButtonRight.GetComponent<Image>();
			if (image != null)
			{
				image.sprite = hoverImageRight; // Cambia la imagen
			}
		}
		else
		{
			Debug.LogWarning("No se encontró el hijo con el nombre: " + "ButtonRight");
		}

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
		// Restaura las imágenes por defecto al salir del hover
		// Asigna la imagen por defecto a los hijos
		Transform ButtonLeft = transform.Find("ButtonLeft");
		if (ButtonLeft != null)
		{
			Image image = ButtonLeft.GetComponent<Image>();
			if (image != null)
			{
				image.sprite = defaultImageLeft; // Cambia la imagen
			}
		}
		else
		{
			Debug.LogWarning("No se encontró el hijo con el nombre: " + "ButtonLeft");
		}


		Transform ButtonCenter = transform.Find("ButtonCenter");
		if (ButtonCenter != null)
		{
			Image image = ButtonCenter.GetComponent<Image>();
			if (image != null)
			{
				image.sprite = defaultImageCenter; // Cambia la imagen
			}
		}
		else
		{
			Debug.LogWarning("No se encontró el hijo con el nombre: " + "ButtonCenter");
		}

		Transform ButtonRight = transform.Find("ButtonRight");
		if (ButtonRight != null)
		{
			Image image = ButtonRight.GetComponent<Image>();
			if (image != null)
			{
				image.sprite = defaultImageRight; // Cambia la imagen
			}
		}
		else
		{
			Debug.LogWarning("No se encontró el hijo con el nombre: " + "ButtonRight");
		}

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
