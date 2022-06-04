using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
	[SerializeField]
	bool _enabled = false;
	[SerializeField]
	float damageValue;

	public bool Enabled
	{
		get => _enabled;
		set => _enabled = value;
	}

	public float DamageValue
	{
		get => damageValue;
		set => damageValue = value;
	}
}
