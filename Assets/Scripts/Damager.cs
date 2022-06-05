using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour, IDamager
{
	[SerializeField]
	bool _enabled = false;
	[SerializeField]
	float damageValue;

	public bool Enabled
	{
		get => _enabled;
		set
		{
			if(!_enabled && value)
				damagedColliders = new List<Collider>();
			_enabled = value;
		}
	}

	public float DamageValue
	{
		get => damageValue;
		set => damageValue = value;
	}

	private List<Collider> damagedColliders;

	private void OnTriggerEnter(Collider other)
	{
		if (!_enabled) return;
		if (damagedColliders.Contains(other)) return;
		other.GetComponent<IDamagable>()?.TakeDamage(this);
		damagedColliders.Add(other);
	}
}
