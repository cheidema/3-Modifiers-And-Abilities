using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Characters;

namespace RPG.CameraUI
{
    public class PlayerHealthUI : MonoBehaviour
    {
        Image healthOrb;
        Player player;

        // Use this for initialization
        void Start()
        {
            healthOrb = GetComponent<Image>();
            player = FindObjectOfType<Player>();
        }

        // Update is called once per frame
        void Update()
        {
            healthOrb.fillAmount = player.healthAsPercentage;
        }
    }
}