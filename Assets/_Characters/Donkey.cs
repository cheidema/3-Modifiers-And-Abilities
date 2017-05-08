using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	public class Donkey : MonoBehaviour {

		[SerializeField] Character characterConfig;

		// Use this for initialization
		void Start () {
			var characterModel = characterConfig.animatedModel;
			Instantiate (characterModel, gameObject.transform);
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}