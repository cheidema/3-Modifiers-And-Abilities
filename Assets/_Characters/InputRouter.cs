﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
	public class InputRouter : MonoBehaviour {

		Character character;

		public void SetCharacter (Character character)
		{
			this.character = character;
		}

		void Update()
		{
			ProcessDirectMovement ();
		}

		// TODO make this get called again
		void ProcessDirectMovement()
		{
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");

			// calculate camera relative direction to move:
			Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
			Vector3 movement = (v * cameraForward + h * Camera.main.transform.right).normalized;

			character.Move (new Vector2(movement.x, movement.z));
		}

	}

}