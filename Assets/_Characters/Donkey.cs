using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	[ExecuteInEditMode]
	[SelectionBase]
	public class Donkey : MonoBehaviour {

		[SerializeField] CharacterConfig characterConfig;

		// Use this for initialization
		void Start () {
			RebuildChildren ();
			if (Application.isPlaying)
			{
				//
			}
		}

		void DestroyChildren ()
		{
			foreach (Transform child in transform)
			{
				DestroyImmediate (child.gameObject);
			}
		}

		void RebuildChildren ()
		{
			if (characterConfig == null)
			{
				// TODO consider making show only once
				Debug.LogWarning ('"' + gameObject.name + '"' + " has no character config");
				return;
			}
			DestroyChildren ();
			var model = Instantiate (characterConfig.animatedModel, gameObject.transform);
			if (!Application.isPlaying) // TODO move to animation shim
			{
				model.transform.RotateAround (Vector3.up, Mathf.PI);
			}
		}
		
		// Update is called once per frame
		void Update () {
			if (!Application.isPlaying)
			{
				RebuildChildren ();
			}
		}
	}
}