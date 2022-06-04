using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCharacterControl : CharacterControl
{
	public override Vector2 MoveHorizontal => Vector2.zero;

	public override bool NeedToPerformAttack => false;

	public override bool Blocking => false;

	public override Quaternion Facing => Quaternion.identity;
}
