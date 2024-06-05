using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compound : MonoBehaviour
{
    [SerializeField] private GameObject itemCell, img,open;
    [SerializeField] private Text talkItem;
    [SerializeField] private GameObject[] comImg;
    [SerializeField] private Sprite back;
    private Item[] comItem;
    private Item nowItem;
    private BagItem myBag;
    private bool isOpening;
    private void Start()
    {
        myBag = Player.instance.myBag;
        comItem = new Item[comImg.Length];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            isOpening = !isOpening;
            SetImg(isOpening);
        }
    }
    private void SetImg(bool isOpen)
    {
        Text[] des = img.GetComponentsInChildren<Text>();
        foreach (var item in des)
            Destroy(item.transform.parent.gameObject);
        GameObject i;
        foreach (var item in myBag.itemList)
        {
            i = Instantiate(itemCell, img.transform);
            if (item != null)
            {
                SetImg(i, item);
                i.GetComponent<Button>().onClick.AddListener(() =>
                {
                    nowItem = item;
                    talkItem.text = item.itemInfo;
                    for (int i = 0; i < 3; i++)
                    {
                        if (comItem[i] == null)
                        {
                            SetImg(comImg[i], nowItem);
                            comItem[i] = nowItem;
                            i = 3;
                        }
                    }
                });
            }
            else
                i.GetComponentInChildren<Text>().text = "";
        }
        open.SetActive(isOpen);
    }
    public void UseObject()
    {

    }
    public void DownObject(int _number)
    {
        SetImg(comImg[_number]);
        comItem[_number] = null;
    }
    private void SetImg(GameObject i,Item _item)
    {
        i.GetComponent<Image>().sprite = _item.itemImage;
        if (i.GetComponentInChildren<Text>())
            i.GetComponentInChildren<Text>().text = string.Format("{0:D2}", _item.itemNum);
    }
    private void SetImg(GameObject i)
    {
        i.GetComponent<Image>().sprite = back;
        if (i.GetComponentInChildren<Text>())
            i.GetComponentInChildren<Text>().text = "";
    }
}
