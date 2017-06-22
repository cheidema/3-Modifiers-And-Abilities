﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
    public class SelfHealBehaviour : MonoBehaviour, ISpecialAbility
    {
        SelfHealConfig config;

        Player player;

        public void SetConfig(SelfHealConfig configToSet)
        {
            this.config = configToSet;
        }

        // Use this for initialization
        void Start()
        {
            print("Self Heal behaviour attached to " + gameObject.name);
            player = FindObjectOfType<Player>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Use(AbilityUseParams useParams)
        {
            print("Self heal used by: " + gameObject.name);
            player.TakeDamage(-config.GetExtraHealth());
        }
    }
}