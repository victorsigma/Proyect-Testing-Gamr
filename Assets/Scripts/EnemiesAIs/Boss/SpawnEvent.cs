using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class EnemySpawnData
{
	public GameObject enemyPrefab;
	public int amount;
}

public enum SpawnActivationType
{
	HealthPercentage,
	TimeElapsed,
	OnDamageTaken,
	PlayerProximity
}

[CreateAssetMenu(fileName = "NewSpawnEvent", menuName = "Spawn Event")]
public class SpawnEvent : ScriptableObject
{
	[Header("Enemy Spawn Configuration")]
	public List<EnemySpawnData> enemiesToSpawn;

	[Header("Activation Parameters")]
	public SpawnActivationType activationType;

	// Parámetros para activación por porcentaje de vida
	[Range(0, 100)] public float activationHealthPercentage = 50f;
	public bool useActivationHealthPercentage = true;

	// Parámetros para activación por tiempo transcurrido
	public float timeToActivate = 10f;

	// Parámetros para activación al recibir daño
	public bool activateOnDamageTaken = false;

	// Parámetros para activación por proximidad del jugador
	public float activationDistance = 5f;

	[Header("Usage Settings")]
	public bool isUsed = false;
	public bool allowMultipleActivations = false;

	[TextArea] public string customConditionDescription;

	/// <summary>
	/// Obtiene la cantidad total de enemigos a generar.
	/// </summary>
	public int GetTotalEnemiesToSpawn()
	{
		int total = 0;
		foreach (var enemy in enemiesToSpawn)
		{
			total += enemy.amount;
		}
		return total;
	}

	/// <summary>
	/// Verifica si el evento debe activarse según el porcentaje de vida.
	/// </summary>
	public bool ShouldActivateByHealth(float currentHealthPercentage)
	{
		Debug.Log((currentHealthPercentage <= activationHealthPercentage) + ": " +currentHealthPercentage + ", " + activationHealthPercentage);
		return useActivationHealthPercentage && currentHealthPercentage <= activationHealthPercentage;
	}

	/// <summary>
	/// Verifica si el evento debe activarse según el tiempo transcurrido.
	/// </summary>
	public bool ShouldActivateByTime(float elapsedTime)
	{
		return elapsedTime >= timeToActivate;
	}

	/// <summary>
	/// Verifica si el evento debe activarse al recibir daño.
	/// </summary>
	public bool ShouldActivateOnDamage()
	{
		return activateOnDamageTaken;
	}

	/// <summary>
	/// Verifica si el evento debe activarse según la proximidad del jugador.
	/// </summary>
	public bool ShouldActivateByProximity(Transform entityTransform, Transform playerTransform)
	{
		float distance = Vector2.Distance(entityTransform.position, playerTransform.position);
		return distance <= activationDistance;
	}

	/// <summary>
	/// Verifica si el evento puede activarse.
	/// </summary>
	public bool CanActivate()
	{
		if (isUsed && !allowMultipleActivations)
		{
			return false;
		}
		return true;
	}

	/// <summary>
	/// Marca el evento como usado.
	/// </summary>
	public void MarkAsUsed()
	{
		isUsed = true;
	}

	/// <summary>
	/// Devuelve una lista con todos los prefabs de enemigos a generar.
	/// </summary>
	public List<GameObject> GetEnemyPrefabs()
	{
		List<GameObject> prefabs = new List<GameObject>();
		foreach (var enemy in enemiesToSpawn)
		{
			prefabs.Add(enemy.enemyPrefab);
		}
		return prefabs;
	}
	private void OnEnable()
    {
        // Suscribirse al evento de escena cargada
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
	
	private void OnDisable()
	{
		// Restablecemos los valores al salir del modo de juego
		isUsed = false;
	}
	
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		// Reiniciar el estado al cargar una nueva escena
		isUsed = false;
	}
}
