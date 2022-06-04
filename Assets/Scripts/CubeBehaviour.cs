using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
	[SerializeField]
	bool accelerateUp;

	PlayerInput inputActions;
	float verticalVelocity;

	// Update is called once per frame
	void Update()
	{
		verticalVelocity = 
			accelerateUp ? 
			verticalVelocity + 10 * Time.deltaTime :
			0;
		transform.position += Vector3.up * verticalVelocity * Time.deltaTime;
	}

	
}
