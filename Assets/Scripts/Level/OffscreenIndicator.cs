using UnityEngine;
using UnityEngine.UI;

public class OffscreenIndicator : MonoBehaviour
{
	public Camera mainCamera;
	public Image indicatorPrefab;
	public Color indicatorColor = Color.red;
	public Sprite indicatorSprite;
	public float borderOffset = 50f;
	
	public Canvas canvas;

	private Image indicatorInstance;
	//private bool isIndicatorActive = true;

	void Start()
	{
		if (!mainCamera) mainCamera = Camera.main;

		// Crear instancia del icono
		indicatorInstance = Instantiate(indicatorPrefab, canvas.transform);
		indicatorInstance.color = indicatorColor;
		if (indicatorSprite != null)
		{
			indicatorInstance.sprite = indicatorSprite;
		}
	}

	void Update()
	{
		Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position);
		Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

		bool isOffScreen = screenPos.z < 0 || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height;

		// Activar/desactivar el indicador según si está fuera de pantalla
		indicatorInstance.gameObject.SetActive(isOffScreen);

		if (isOffScreen)
		{
			// Mantener el icono dentro de los límites de la pantalla con un margen (borderOffset)
			screenPos.x = Mathf.Clamp(screenPos.x, borderOffset, Screen.width - borderOffset);
			screenPos.y = Mathf.Clamp(screenPos.y, borderOffset, Screen.height - borderOffset);

			// Ajustar la posición del indicador
			indicatorInstance.transform.position = screenPos;

			// Rotar el indicador para apuntar hacia el objeto usando la dirección desde el centro de la pantalla
			Vector2 direction = (screenPos - (Vector3)screenCenter).normalized;
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
			indicatorInstance.transform.rotation = Quaternion.Euler(0, 0, angle);
		}
	}
	
	public void SetIndicatorActive(bool isActive)
	{
		canvas.gameObject.SetActive(isActive);
	}

	private void OnDestroy()
	{
		if (indicatorInstance != null)
		{
			Destroy(indicatorInstance.gameObject);
		}
	}
}
