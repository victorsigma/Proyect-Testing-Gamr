using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectorAICircle : MonoBehaviour
{
	[field: SerializeField]
	public bool PlayerDetected {get; private set;}
	
	public Vector2 DirectionToTarget => target.transform.position - detectorOrigin.position;
	
	[Header("OverlapBox parameters")]
	[SerializeField]
	private Transform detectorOrigin;
	
	public float radius = 1.0f;
	public Vector2 detectorOriginOffset = Vector2.zero;
	
	public float detectionDelay = 0.3f;
	
	public LayerMask dectectorLayerMask;
	
	[Header("Gizmo parameters")]
	public Color gizmoIdleColor = Color.green;
	public Color gizmoDetectedColor = Color.red;
	public bool showGizmos = true;
	
	private GameObject target;
	
	public GameObject Target 
	{
		get => target;
		private set 
		{
			target = value;
			PlayerDetected = target != null;
		}
	}
	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(DetectionCoroutine());
	}
	
	IEnumerator DetectionCoroutine()
	{
		yield return new WaitForSeconds(detectionDelay);
		PerformDetection();
		StartCoroutine(DetectionCoroutine());
	}
	
	public void PerformDetection() 
	{
		Collider2D collider = Physics2D.OverlapCircle(detectorOriginOffset, radius, dectectorLayerMask);
		if(collider != null) 
		{
			Target =  collider.gameObject;
		} else
		{
			Target = null;	
		}
	}
	
	private void OnDrawGizmos() 
	{
		if(showGizmos && detectorOrigin != null)
		{
			Gizmos.color = gizmoIdleColor;
			if(PlayerDetected)
				Gizmos.color = gizmoDetectedColor;
			Gizmos.DrawSphere(transform.position, radius);
		}
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
