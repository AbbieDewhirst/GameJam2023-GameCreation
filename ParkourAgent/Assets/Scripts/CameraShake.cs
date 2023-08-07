using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public static CameraShake instance;
	public Transform camTransform;
	
	// How long the object should shake for.
	public float shakeDuration = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;
	public float shakePower;
	
	Vector3 originalPos;
	
	void Awake()
	{
		instance = this;
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}
	

	public void shake(float time)
	{
		shakeDuration = time;
	}
	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	Vector3 velocityRef;
	void Update()
	{
		if (shakeDuration > 0)
		{
			camTransform.localPosition = Vector3.SmoothDamp(camTransform.localPosition, originalPos + Random.insideUnitSphere * shakeAmount, ref velocityRef, shakePower);
			
			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			camTransform.localPosition = originalPos;
		}
	}
}
