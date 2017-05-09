using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Characters
{
	[CreateAssetMenu(menuName = ("RPG/Character"))]
	public class CharacterConfig : ScriptableObject { // NOTE rename SO in Unity to keep track
		
		public GameObject animatedModel;

	}
}