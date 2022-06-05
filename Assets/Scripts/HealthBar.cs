using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IHealthBar
{
	[SerializeField]
	Transform _camera;
	Transform Camera =>
		_camera != null ?
		_camera :
		UnityEngine.Camera.main.transform;
	[SerializeField]
	float fillerFullLocalScale;
	[SerializeField]
	Image barFiller;
	public float Value
	{
		get => barFiller.fillAmount * MaxValue;
		set => barFiller.fillAmount = value / MaxValue;
	}
	public float MaxValue { get; set; }


	// Update is called once per frame
	void Update()
	{
		transform.LookAt(Camera);
		transform.Rotate(0, 180, 0);
	}
}
