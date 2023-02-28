using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Combat;

namespace RPG.Item
{
    public class MagicPickup : ItemPickup
    {
        [SerializeField] Magic magicToAdd;

        public override void UseItem(GameObject player)
        {
            player.GetComponent<MagicInventory>().AddNewMagicToInventory(magicToAdd);
        }
    }
}

