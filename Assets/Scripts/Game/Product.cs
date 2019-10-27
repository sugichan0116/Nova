using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu]
public class Product : ScriptableObject
{
    public enum Category
    {
        UNKNOWN,
        FOOD,
        DOCUMENT,
        RESOURCE,
        MEDICHINE,
        LUXURY,
    }

    [ShowAssetPreview]
    public Sprite icon;
    public Category category;
    public string productName;
    //[MinMaxSlider(0, 100000)]
    public Vector2 price;
}
