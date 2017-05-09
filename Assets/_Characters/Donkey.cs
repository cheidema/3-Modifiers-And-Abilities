using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	[ExecuteInEditMode]
	[SelectionBase]
	public class Donkey : MonoBehaviour {

		[SerializeField] Character characterConfig;

		// Use this for initialization
		void Start () {
			DestroyChildren ();
			Instantiate (characterConfig.animatedModel, gameObject.transform);

		}

		void DestroyChildren ()
		{
			foreach (Transform child in transform)
			{
				DestroyImmediate (child.gameObject);
			}
		}

		
		// Update is called once per frame
		void Update () {
			if (!Application.isPlaying)
			{
				DestroyChildren ();
				Instantiate (characterConfig.animatedModel, gameObject.transform);
			}
		}
	}
}