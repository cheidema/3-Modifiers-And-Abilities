using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Characters
{
	[ExecuteInEditMode]
	[SelectionBase]
	public class CharacterFactory : MonoBehaviour {

		[SerializeField] CharacterConfig characterConfig;
		[SerializeField] Waypoints wayPoints;

		// Use this for initialization
		void Start () {
			RebuildChildren ();
			if (Application.isPlaying)
			{
				gameObject.AddComponent<NavMeshAgent> ();
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
				model.transform.Rotate (Vector3.up, 180f);
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