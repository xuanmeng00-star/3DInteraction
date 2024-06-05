using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Bag", menuName = "Bag/New Bag")]
public class BagItem : ScriptableObject
{
    public List<Item> itemList = new List<Item>();
}
