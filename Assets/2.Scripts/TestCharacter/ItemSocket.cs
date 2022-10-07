using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSocket : MonoBehaviour
{
    public enum eType
    {
        Inventory,
        Equipment
    }
    public eType type;
    public Item Item
    {
        get;
        private set;
    }
    public void Init(eType type)
    {
        this.type = type;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public bool CheckItem()
    {
        return this.Item != null ? true : false;
    }
    public void SetItem(Item item)
    {
        this.Item = item;
        if (this.Item == null)
        {
            return;
        }
        this.Item.transform.SetParent(this.gameObject.transform);
        RectTransform rt = (RectTransform)this.Item.transform;
        rt.anchoredPosition = new Vector2(0, 0);
        this.Item.socketType = (Item.eType)((int)this.type);
        this.Item.socket = this;
    }
}
