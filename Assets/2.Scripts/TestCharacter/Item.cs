using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum eType
    {
        Mustache
    }
    public enum eSocketType
    {
        Inventory,
        Equipment
    }
    private ItemData iData;
    private Image imageItem;
    public eType itemType;
    public eType socketType;
    
    public ItemSocket socket;

    public void Init()
    {
        this.iData = new ItemData();
        this.iData.name = this.gameObject.name;
        this.imageItem = GetComponentInChildren<Image>();
        this.itemType = eType.Mustache;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetInfo(Sprite sprite)
    {
        this.imageItem.sprite = sprite;
        this.gameObject.name = this.imageItem.sprite.name;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        DropItem();
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        PlayerUI uI = FindObjectOfType<PlayerUI>();
        ItemSocket socket = uI.FindNearSocket(this.transform);
        float dir = Vector2.Distance(this.transform.position, socket.transform.position);
        if(socket.type == ItemSocket.eType.Inventory)
        {
            if (dir < 70f)
            {
                if (socket.CheckItem())
                {
                    uI.inven.ChangeItem(this, this.socket, socket);
                }
                else
                {
                    this.socket.SetItem(null);
                    socket.SetItem(this);
                }
            }
            else
            {
                this.gameObject.transform.SetParent(this.socket.transform);
                RectTransform rect = (RectTransform)this.transform;
                rect.anchoredPosition = new Vector2(0, 0);
            }
        }
        else if (socket.type == ItemSocket.eType.Equipment)
        {
            if (dir < 70f)
            {
                if (socket.CheckItem())
                {
                    Debug.Log("체인지");
                    uI.equip.ChangeEquipItem(this, this.socket, socket);
                }
                else
                {
                    Debug.Log("넣기");
                    this.socket.SetItem(null);
                    uI.equip.SetEquipItem(this, socket);
                }
            }
            else
            {
                this.gameObject.transform.SetParent(this.socket.transform);
                RectTransform rect = (RectTransform)this.transform;
                rect.anchoredPosition = new Vector2(0, 0);
            }
        }
    }
    private void DropItem()
    {
        if (this.socket != null)
        {
            this.gameObject.transform.SetParent(FindObjectOfType<PlayerUI>().gameObject.transform);
        }
    }
}
