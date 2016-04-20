using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class HelmetItem : Item {

    private int weight = 100;

    public HelmetItem()
    {
        type = Enums.ItemTypes.Helmet;
        parts = new List<Part>( new Part[] { new Part(Enums.BikerParts.Head) } );
	}
	
	
}
