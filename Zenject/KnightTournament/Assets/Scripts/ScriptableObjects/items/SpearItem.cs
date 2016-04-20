using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SpearItem : Item {

    private int attack = 100;

    public SpearItem() 
    {
        type = Enums.ItemTypes.Spear;
        parts = new List<Part>(new Part[] { new Part(Enums.BikerParts.Spear) });
    }
}
