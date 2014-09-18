﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(LayerEnum.Ground), LayerMask.NameToLayer(LayerEnum.Weapon));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(LayerEnum.Void), LayerMask.NameToLayer(LayerEnum.Weapon));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(LayerEnum.Void), LayerMask.NameToLayer(LayerEnum.Player));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(LayerEnum.Void), LayerMask.NameToLayer(LayerEnum.Ground));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(LayerEnum.Void), LayerMask.NameToLayer(LayerEnum.Default));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(LayerEnum.Void), LayerMask.NameToLayer(LayerEnum.Void));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
