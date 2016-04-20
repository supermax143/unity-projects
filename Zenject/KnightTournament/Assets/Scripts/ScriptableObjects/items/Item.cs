using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class Item  {

    [SerializeField]
    private long id;

    public long Id
    {
        get 
        { 
            return id; 
        }
    }
    public Enums.ItemTypes type;
    public string name;
    public Sprite image;
    public List<Part> parts;

    public static Item Create(Enums.ItemTypes itemType) 
    {
        Item item = null;
        switch (itemType) { 
            case Enums.ItemTypes.Helmet:
                item = new HelmetItem();
                break;
            case Enums.ItemTypes.Suit:
                item = new SuitItem();
                break;
            case Enums.ItemTypes.Spear:
                item = new SpearItem(); 
                break;

        }
        if (item != null)
            item.id = DateTime.UtcNow.Ticks;
        return item;
    }
}
