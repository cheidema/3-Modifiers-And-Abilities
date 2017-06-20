﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Weapons
{
    [CreateAssetMenu(menuName = ("RPG/Weapon"))]
    public class Weapon : ScriptableObject
    {
        public Transform gripTransform;

        [SerializeField] GameObject weaponPrefab;
        [SerializeField] AnimationClip attackAnimation;
        [SerializeField] float minTimeBetweenHits = .5f;
        [SerializeField] float maxAttackRange = 2f;

        const string HIT_EVENT_NAME = "Hit"; // Also change method name on charater(s)

        public float GetMinTimeBetweenHits()
        {
            return minTimeBetweenHits;
        }

        public float GetMaxAttackRange()
        {
            return maxAttackRange;
        }

        public GameObject GetWeaponPrefab()
        {
            return weaponPrefab;
        }
        
        public AnimationClip GetAttackAnimClip()
        {
            FindHitEvent();
            return attackAnimation;
        }

        // So that asset packs cannot cause crashes
        private void FindHitEvent()
        {
            foreach(AnimationEvent animEvent in attackAnimation.events)
            {
                if (animEvent.functionName == HIT_EVENT_NAME)
                {
                    attackAnimation.events = new AnimationEvent[0];
                    attackAnimation.AddEvent(animEvent);
                    break;
                }
            }
        }
    }
}