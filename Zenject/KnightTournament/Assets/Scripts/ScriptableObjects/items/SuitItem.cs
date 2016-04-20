using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SuitItem : Item {

    private int durability = 20;

    public SuitItem() 
    {
        type = Enums.ItemTypes.Suit;
        parts = new List<Part>(new Part[] { new Part(Enums.BikerParts.Body), new Part(Enums.BikerParts.Arm1), new Part(Enums.BikerParts.Arm2),
        new Part(Enums.BikerParts.LegLeft1),new Part(Enums.BikerParts.LegLeft2),new Part(Enums.BikerParts.LegRight1),new Part(Enums.BikerParts.LegRight2)});
    }
}
