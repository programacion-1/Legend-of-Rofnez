using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Item
{
    public class ManaPickup : ItemPickup
    {
        public override void UseItem(GameObject player)
        {
            ItemInventory itemInventory = player.GetComponent<ItemInventory>();
            if(itemInventory.GetCurrentMPpotions() < itemInventory.GetMaxMPpotions())
            {
                int newQuantity = itemInventory.GetCurrentMPpotions() + 1;
                itemInventory.SetCurrentMPpotions(newQuantity);
                itemInventory.SetMPPotQuantity();
            }
        }
    }

}