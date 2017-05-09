using UnityEngine;
using UnityEngine.Assertions;

public class Character {

	public void Move (Vector2 relativelyVelocity)
	{
		Assert.IsTrue (relativelyVelocity.magnitude <= 1f); // Like gamepad


	}
}