using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGenerator : MonoBehaviour
{


   public static DropObject generatDrop()
    {
        DropObject toReturn = new DropObject();
            for (int i = 0; i < 4; i++)
            {
                int rarity = Random.Range(0, 100);
                if(rarity >= 0 && rarity <= 50) { 
                    int amount = Random.Range(6, 20);
                    toReturn.commonElement = amount;
                }
                else if (rarity >= 51 && rarity <= 75) { 
                    int amount = Random.Range(4, 12);
                    toReturn.basicElement = amount;
                }
                else if (rarity >= 76 && rarity <= 94) { 
                    int amount = Random.Range(2, 10);
                    toReturn.rareElemnt = amount;
                }
                else { 
                    int amount = Random.Range(1, 2);
                    toReturn.elitElemnts = amount;
                }
            }

        return toReturn;
    }

}
