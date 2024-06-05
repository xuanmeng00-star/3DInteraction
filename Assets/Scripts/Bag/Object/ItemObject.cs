using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Item myItem;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(!Player.instance.myBag.itemList.Contains(myItem))
                Player.instance.myBag.itemList.Add(myItem);
            myItem.itemNum += 1;
            Destroy(this.gameObject);
        }
    }
}
