using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "NewStoreItem", menuName = "Store/Store Item", order = 1)]
public class StoreItem : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public int Price;
}

