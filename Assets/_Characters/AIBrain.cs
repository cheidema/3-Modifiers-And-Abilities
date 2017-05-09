using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	public class AIBrain : MonoBehaviour {

		Character character;

		public void SetCharacter (Character character)
		{
			this.character = character;
		}

		// Update is called once per frame
		void Update () {
			character.Move (new Vector2 (1f, 0));
		}
	}
}