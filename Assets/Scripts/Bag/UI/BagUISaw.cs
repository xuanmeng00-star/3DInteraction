using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagUISaw : MonoBehaviour
{
    [SerializeField] private GameObject itemCell,img;
    [SerializeField] private Text talkItem;
    private Item nowItem;
    private BagItem myBag;
    private bool isOpening;
    private void Start()
    {
        myBag = Player.instance.myBag;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isOpening = !isOpening;
            SetImg(isOpening);
        }
    }
    private void SetImg(bool isOpen)
    {
        Text[] des = img.GetComponentsInChildren<Text>();
        foreach (var item in des)       
            Destroy(item.transform .parent.gameObject);
        GameObject i;
        foreach (var item in myBag.itemList)
        {
            i = Instantiate(itemCell, img.transform);
            if (item != null)
            {
                i.GetComponent<Image>().sprite = item.itemImage;
                i.GetComponentInChildren<Text>().text = string.Format("{0:D2}", item.itemNum);
                i.GetComponent<Button>().onClick.AddListener(() =>
                {
                    nowItem = item;
                    talkItem.text = item.itemInfo;
                });
            }else
                i.GetComponentInChildren<Text>().text = "";
        }
        img.transform .parent.gameObject.SetActive(isOpen);
    }
    public void UseObject()
    {
        if (!nowItem)
            return;
        nowItem.itemNum -=1;
        if (nowItem.itemNum <= 0)
        {
            myBag.itemList.Remove(nowItem);
            nowItem = null;
        }
        SetImg(true);
    }
}
