using UnityEngine;
using System.Collections;

[System.Serializable]
public class Part {

    public Part()
    {
        partType = Enums.BikerParts.Head;
    }
    public Part(Enums.BikerParts part) 
    {
        partType = part;
    }

    public Enums.BikerParts partType;
    public Sprite image;
}
