using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

namespace RPG.Core
{
    public class MagicPoints : MonoBehaviour
    {
        [SerializeField] float magicPoints;
        [SerializeField] float maxMagicPoints;
        HealBar bar;
        [SerializeField] string magicBarName;
        PlayerMinMaxQuantityText magicBarText;
        [SerializeField] string magicBarTextName;
        // Start is called before the first frame update
        void Start()
        {
            // SetStartingMagicPointsSettings();
        }

        public void SetStartingMagicPointsSettings()
        {
            magicPoints = maxMagicPoints;
            HealBar[] healbars = GameObject.FindObjectsOfType<HealBar>();
            for(int i = 0; i < healbars.Length; i++)
            {
                if(healbars[i].gameObject.name == magicBarName)
                {
                    bar = healbars[i];
                    break;
                }
            }
            PlayerMinMaxQuantityText[] playerMinMaxQuantityTexts = GameObject.FindObjectsOfType<PlayerMinMaxQuantityText>();
            for (int i = 0; i < playerMinMaxQuantityTexts.Length; i++)
            {
                if (playerMinMaxQuantityTexts[i].gameObject.name == magicBarTextName)
                {
                    magicBarText = playerMinMaxQuantityTexts[i];
                    break;
                }
            }
            bar.ChangeBarFiller(magicPoints, maxMagicPoints);
            magicBarText.SetQuantityText(magicPoints, maxMagicPoints);
        }

        public float GetMagicPoints()
        {
            return magicPoints;
        }

        public void ConsumeMagicPoints(float mpToConsume)
        {
            magicPoints = Mathf.Max(magicPoints - mpToConsume, 0);
            if(bar != null) bar.ChangeBarFiller(magicPoints, maxMagicPoints);
            magicBarText.SetQuantityText(magicPoints, maxMagicPoints);
        }

        public void RestoreMagicPoints(float mptToRestore)
        {
            magicPoints = Mathf.Min(magicPoints + mptToRestore, maxMagicPoints);
            bar.ChangeBarFiller(magicPoints, maxMagicPoints);
        }
    }
}
