using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

public class ItemsEditorWindow : EditorWindow {


    private const int ListCreationView = 0;
    private const int ItemsEditView = 1;

    [MenuItem("Window/Items Editor")]
	// Use this for initialization
	static void  Create() {
        EditorWindow.GetWindow<ItemsEditorWindow>();
	}

    private ItemsList mItemsList;

    private string filename = "";

    private string[] tullbarLables = {"List Creation","Items Edit"};
    private int selectedViewIndex = 0;

    private string[] itemNames = { };
    private int selectedItemIndex;

    private string[] partNames = { };
    private int selectedPartIndex;

    private Enums.ItemTypes creatinItemType;

    private System.Object selectedSprite;
    void OnGUI() {

        selectedViewIndex = GUILayout.Toolbar(selectedViewIndex, tullbarLables, GUILayout.ExpandWidth(false));
        switch(selectedViewIndex)
        {
            case ListCreationView:
                DrawListCreationView();
                break;
            case ItemsEditView:
                DrawItemsEditView();
                break;
        }
    
    }

    void DrawListCreationView() {
        GUILayout.BeginVertical();
        GUILayout.Label("Name", "label");
        GUILayout.BeginHorizontal();

        filename = GUILayout.TextField(filename, 100, GUILayout.MaxWidth(150));
        if (GUILayout.Button("Create", GUILayout.ExpandWidth(false)))
            CreateList();

        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    
    }

    void DrawItemsEditView()
    {
        if (GUILayout.Button("Select List",GUILayout.ExpandWidth(false)))
            OpenItemsList();
        if (mItemsList == null)
            return;
        if (GUILayout.Button("Save", GUILayout.ExpandWidth(false)))
            EditorUtility.SetDirty(mItemsList);
        itemNames = new string[mItemsList.AllItems.Count];
        for(int i = 0; i<mItemsList.AllItems.Count;i++)
            itemNames[i] = mItemsList.AllItems[i].name;
        GUILayout.Space(10);
        GUILayout.Label(mItemsList.name, EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create New", GUILayout.ExpandWidth(false)))
            AddNewItem();
        if (GUILayout.Button("Remove Selected", GUILayout.ExpandWidth(false)))
            RemoveSelectedItem();
        GUILayout.EndHorizontal();
        creatinItemType = (Enums.ItemTypes)EditorGUILayout.EnumPopup(creatinItemType, GUILayout.MaxWidth(100));
        if (selectedItemIndex >= mItemsList.AllItems.Count)
            selectedItemIndex = mItemsList.AllItems.Count - 1;
        selectedItemIndex = GUILayout.SelectionGrid(selectedItemIndex, itemNames, 10,GUILayout.ExpandWidth(false));
        DrawSelectedItem();
    }

    private void DrawSelectedItem()
    {
        if(!mItemsList || mItemsList.AllItems.Count == 0 || selectedItemIndex ==-1)
            return;
        Item item = mItemsList.AllItems[selectedItemIndex];
        if (item==null)
            return;
        GUILayout.Space(20);
        GUILayout.Label(item.name+":  "+item.type,EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        item.name = GUILayout.TextField(item.name, 100, GUILayout.MaxWidth(150));
        GUILayout.Label("Name");
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        item.image = EditorGUILayout.ObjectField(item.image, typeof(Sprite), true, GUILayout.MaxWidth(150)) as Sprite;
        GUILayout.Label("Image in shop");
        //if(item.image)
        // {
        //    Texture t = item.image.texture;
        //    Rect tr = item.image.textureRect;
        //    Rect r = new Rect(tr.x / t.width, tr.y / t.height, tr.width / t.width, tr.height / t.height);
        //    GUI.DrawTextureWithTexCoords(new Rect(0, 0, tr.width, tr.height), t, r);
        //}
        GUILayout.EndHorizontal();
        GUILayout.Label("Parts", EditorStyles.boldLabel);
        //GUILayout.BeginHorizontal();
        //if (GUILayout.Button("Create New", GUILayout.ExpandWidth(false)))
        //    AddNewPart(item);
        //if (GUILayout.Button("Remove Selected", GUILayout.ExpandWidth(false)))
        //    RemoveSelectedPart();
        //GUILayout.EndHorizontal();
        DrawParts(item);
    }

    void DrawParts(Item item) 
    {
        if (item == null || item.parts == null)
            return;

        partNames = new string[item.parts.Count];
        for (int i = 0; i < item.parts.Count; i++)
            partNames[i] = item.parts[i].partType.ToString();
        selectedPartIndex = GUILayout.SelectionGrid(selectedPartIndex, partNames, 10, GUILayout.ExpandWidth(false));
        if (selectedPartIndex >= item.parts.Count)
            selectedPartIndex = item.parts.Count - 1;
        if (item.parts.Count == 0)
            return;
        if(selectedPartIndex == -1)
            selectedPartIndex = 0;
        DrawPart(item.parts[selectedPartIndex]);
    }

    void DrawPart(Part part) 
    {
        //part.partType = (Enums.BikerParts)EditorGUILayout.EnumPopup(part.partType, GUILayout.MaxWidth(100));
        part.image = EditorGUILayout.ObjectField(part.image, typeof(Sprite), true, GUILayout.MaxWidth(150)) as Sprite;
        if (part.image == null)
            return;
        Rect selPosition = GUILayoutUtility.GetLastRect();
        Texture t = part.image.texture;
        Rect tr = part.image.textureRect;
        Rect r = new Rect(tr.x / t.width, tr.y / t.height, tr.width / t.width, tr.height / t.height);
        GUI.DrawTextureWithTexCoords(new Rect(selPosition.x, selPosition.y + selPosition.height +5, tr.width, tr.height), t, r);
    }

    private void CreateList()
    {
        ItemsList itemsList = ScriptableObject.CreateInstance<ItemsList>();
        AssetDatabase.CreateAsset(itemsList, "Assets/Data/" + filename + ".asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = itemsList;
    }

    private void OpenItemsList() {
        string absPath = EditorUtility.OpenFilePanel("SelectItemsList", "Assets/Data/", "asset");
        if(absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "assets".Length);
            UpdateItemsList(AssetDatabase.LoadAssetAtPath(relPath, typeof(ItemsList)) as ItemsList);
            if (mItemsList)
            { 
                EditorPrefs.SetString("Object Path",relPath);
                Debug.Log(relPath);
            }
        }
    }


    void AddNewItem()
    {
        //DateTime.Now.Millisecond
        if (!mItemsList)
            return;
        Item item = Item.Create(creatinItemType);
        item.name = "New Item";
        mItemsList.AddNewItem(item);
    }


    void RemoveSelectedItem()
    {
        if (!mItemsList)
            return;
        Item item = mItemsList.AllItems[selectedItemIndex];

        mItemsList.RemoveItem(item.Id);
    }


    private void UpdateItemsList(ItemsList value)
    {
        if(!value)
            return;
        Debug.Log(value.name);
        mItemsList = value;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
