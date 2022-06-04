using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterControl : MonoBehaviour
{
	abstract public Quaternion Facing { get; }
	abstract public Vector2 MoveHorizontal { get; }
	abstract public bool NeedToPerformAttack { get; }
	abstract public bool Blocking { get; }
}
