using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;


[CustomEditor(typeof(BikerItemsManager))]
public class BikerItemsManagerInspector : Editor
{
    private BikerItemsManager itemsManager;
    private ItemsList allItems;

    private Item item;
    private List<Item> helmets;
    private string[] helmetNames = new string[]{};
    private int selectedHelmetIndex;
    private List<Item> suits;
    private string[] suitNames = new string[]{};
    private int selectedSuitIndex;
    private List<Item> spears;
    private string[] spearNames = new string[]{};
    private int selectedSpearIndex;

    void OnEnable() {
        itemsManager = (BikerItemsManager)target;
        UpdateItems();
        selectedHelmetIndex = GetItemIndex(itemsManager.Helmet,helmets)!=-1?GetItemIndex(itemsManager.Helmet,helmets)+1:0;
        selectedSuitIndex = GetItemIndex(itemsManager.Suit, suits)!=-1?GetItemIndex(itemsManager.Suit, suits)+1:0;
        selectedSpearIndex = GetItemIndex(itemsManager.Spear, spears)!=-1?GetItemIndex(itemsManager.Spear, spears)+1:0;
    }

    public override void OnInspectorGUI()
    {
        UpdateItems();
        base.OnInspectorGUI();
        itemsManager.items = EditorGUILayout.ObjectField("Предметы",itemsManager.items, typeof(ItemsList), true) as ItemsList;
        EditorGUILayout.Space();
        selectedHelmetIndex = EditorGUILayout.Popup("Шлем",selectedHelmetIndex, helmetNames);
        selectedSuitIndex = EditorGUILayout.Popup("Костюм", selectedSuitIndex, suitNames);
        selectedSpearIndex = EditorGUILayout.Popup("Копьё", selectedSpearIndex, spearNames);
        if (GUILayout.Button("Сохронил", GUILayout.ExpandWidth(false)))
            SaveItemsData();
    }


    private void SetHelmet(Item item)
    {
        if(item ==null)
            return;
        itemsManager.Helmet = (HelmetItem)item;
    }

    private void SetSuit(Item item)
    {
        if (item == null)
            return;
        itemsManager.Suit = (SuitItem)item;
    }

    private void SetSpear(Item item)
    {
        if (item == null)
            return;
        itemsManager.Spear = (SpearItem)item;
    }

    private Item GETItemByIndex(int index, List<Item> items)
    {
        if (items.Count == 0)
            return null;
        if (index >= items.Count || index < 0)
            return null;
        return items[index];
    }

    private void UpdateItems()
    {
        if (itemsManager == null)
            return;
        allItems = itemsManager.items;
        if (allItems == null)
            return;
        helmets = new List<Item>();
        suits = new List<Item>();
        spears = new List<Item>();
        foreach(Item item in allItems.AllItems)
        {
            switch(item.type)
            {
                case Enums.ItemTypes.Helmet:
                    helmets.Add(item);
                    break;
                case Enums.ItemTypes.Suit:
                    suits.Add(item);
                    break;
                case Enums.ItemTypes.Spear:
                    spears.Add(item);
                    break;
            }
        }
        UpdateSelector(helmets,ref helmetNames);
        UpdateSelector(suits, ref suitNames);
        UpdateSelector(spears, ref spearNames);
    }

    private void UpdateSelector(List<Item> items,ref string[] names) 
    {
        names = new string[items.Count+1];
        names[0] = "Нету";
        for (int i = 0; i < items.Count; i++)
            names[i+1] = items[i].name;
    }

    private int GetItemIndex(Item item, List<Item> list) 
    {
        if (item == null)
            return -1;
        for (int i = 0; i < list.Count; i++)
            if (list[i].Id == item.Id)
                return i;
        return -1;
    }

    private void SaveItemsData()
    {
        SetHelmet(GETItemByIndex(selectedHelmetIndex - 1, helmets));
        SetSuit(GETItemByIndex(selectedSuitIndex - 1, suits));
        SetSpear(GETItemByIndex(selectedSpearIndex - 1, spears));
       
    }



}
