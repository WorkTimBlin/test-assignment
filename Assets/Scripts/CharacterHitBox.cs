using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterHitBox : MonoBehaviour, IDamagable
{
	[SerializeField]
	MonoBehaviour damagable;

	public IDamagable Damagable =>
		(IDamagable)((IDamagable)damagable != null && damagable != this ?
		damagable :
		(damagable = 
		transform.parent.GetComponentInParent<IDamagable>() as MonoBehaviour));

	public void TakeDamage(IDamager damager)
	{
		Damagable.TakeDamage(damager);
	}

	private void OnValidate()
	{
		damagable = Damagable as MonoBehaviour;
	}
}
