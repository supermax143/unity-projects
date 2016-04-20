using UnityEngine;
using System.Collections;

public class BikerItemsManager : MonoBehaviour {

    public ItemsList items;

    [SerializeField]
    private HelmetItem helmet;
    [SerializeField]
    private SuitItem suit;
    [SerializeField]
    private SpearItem spear;

    public SuitItem Suit
    {
        get { return suit; }
        set 
        { 
            suit = value;
            UpdateItemView(suit);
        }
    }
    public SpearItem Spear
    {
        get { return spear; }
        set 
        { 
            spear = value;
            UpdateItemView(spear);
        }
    }

    public HelmetItem Helmet
    {
        get { return helmet; }
        set 
        { 
            helmet = value;
            UpdateItemView(helmet);
        }
    }

    private void UpdateItemView(Item item)
    {
        if (item == null)
            return;
        BikerPartIdenifyer[] partIdentifyers = gameObject.GetComponentsInChildren<BikerPartIdenifyer>();
        foreach (Part part in item.parts)
        {
            foreach (BikerPartIdenifyer identifyer in partIdentifyers)
            {
                if (identifyer.part == part.partType)
                {
                    SpriteRenderer r = identifyer.gameObject.GetComponent<SpriteRenderer>();
                    if (r == null)
                        continue;
                    r.sprite = part.image;
                }
            }
        }
    }

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
