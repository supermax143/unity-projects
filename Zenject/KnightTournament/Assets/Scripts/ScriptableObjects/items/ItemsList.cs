using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ItemsList : ScriptableObject {

    //private List<Item> items = new List<Item>();
    [SerializeField]
    private List<HelmetItem> helmets = new List<HelmetItem>();
    [SerializeField]
    private List<SuitItem> suits = new List<SuitItem>();
    [SerializeField]
    private List<SpearItem> spears = new List<SpearItem>(); 

    public List<Item> AllItems
    {

        get 
        {
            List<Item> list = new List<Item>();
            foreach (Item item in helmets)
                list.Add(item);
            foreach (Item item in suits)
                list.Add(item);
            foreach (Item item in spears)
                list.Add(item);
            return list;
        }
        //set { items = value; }
    }

    public void AddNewItem(Item item) { 
        switch(item.type)
        {
            case Enums.ItemTypes.Helmet:
                helmets.Add(item as HelmetItem);
                break;
            case Enums.ItemTypes.Suit:
                suits.Add(item as SuitItem);
                break;
            case Enums.ItemTypes.Spear:
                spears.Add(item as SpearItem);
                break;
        }
    }

    public Item GetItemById(long id) 
    {
        foreach (Item item in AllItems)
            if (item.Id == id)
                return item;
        return null;
    }

    public void RemoveItem(long id)
    {

        Item item = GetItemById(id);
        switch(item.type)
        {
            case Enums.ItemTypes.Helmet:
                helmets.Remove(item as HelmetItem);
                break;
            case Enums.ItemTypes.Suit:
                suits.Remove(item as SuitItem);
                break;
            case Enums.ItemTypes.Spear:
                spears.Remove(item as SpearItem);
                break;
        }

       
    }
    
}
