using System.Collections;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public string targetTag = "Spawner";

    [SerializeField]
    private string levelSound = "Exploration";

    [SerializeField]
    private string bossSound = "Boss";

	[SerializeField]
    private bool isSpawningEnd = false;
    private Coroutine checkSpawningCoroutine;

    void Start()
    {
        AudioManager.instance.PlayMusic(levelSound);
		gameObject.GetComponent<OffscreenIndicator>().SetIndicatorActive(false);
        // Iniciar la corrutina para comprobar el estado de los Spawner
        checkSpawningCoroutine = StartCoroutine(CheckSpawningEndCoroutine());
    }

    IEnumerator CheckSpawningEndCoroutine()
    {
        while (!isSpawningEnd)
        {
            if (AllObjectsHavePropertyTrue())
            {
                ExecuteEvent();
                yield break; // Salir de la corrutina una vez que se ejecuta el evento
            }

            // Esperar 0.5 segundos antes de volver a comprobar
            yield return new WaitForSeconds(0.5f);
        }
    }

    bool AllObjectsHavePropertyTrue()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(targetTag);

        foreach (GameObject obj in objects)
        {
            // Comprueba si el objeto tiene la propiedad deseada en true
            if (!obj.GetComponent<Spawner>().isSpawningEnd)
            {
                return false;
            }
        }
        return true;
    }

    void ExecuteEvent()
    {
        isSpawningEnd = true;
		gameObject.GetComponent<OffscreenIndicator>().SetIndicatorActive(true);
        Debug.Log("Todos los objetos tienen la propiedad en true. Evento ejecutado.");
        AudioManager.instance.PlayMusic(bossSound);

        // Detener la corrutina al finalizar
        if (checkSpawningCoroutine != null)
        {
            StopCoroutine(checkSpawningCoroutine);
            checkSpawningCoroutine = null;
        }
    }

    private void OnDestroy()
    {
        // Asegurarse de detener la corrutina si el objeto se destruye
        if (checkSpawningCoroutine != null)
        {
            StopCoroutine(checkSpawningCoroutine);
        }
    }
}
