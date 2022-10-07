using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public PlayerUI playerUI;
    private List<ItemSocket> listEquipSocket = new List<ItemSocket>();
    public ItemSocket[] equipSockets;

    void Start()
    {

    }
    public void SetSocketList(List<ItemSocket> list)
    {
        this.listEquipSocket = list;
        this.equipSockets = this.listEquipSocket.ToArray();
        foreach(ItemSocket socket in this.listEquipSocket)
        {
            socket.type = ItemSocket.eType.Equipment;
        }
    }
    public void SetEquipItem(Item item, ItemSocket socket)
    {
        if(((int)item.itemType == (int)socket.part))
        {
            item.socket.SetItem(null);
            socket.SetItem(item);
            this.playerUI.OnEquipment(item);
        }
        else
        {
            item.gameObject.transform.SetParent(item.socket.transform);
            RectTransform rect = (RectTransform)item.transform;
            rect.anchoredPosition = new Vector2(0, 0);
        }
    }
    public void ChangeEquipItem(Item item, ItemSocket socket1, ItemSocket socket2)
    {
        if((int)socket2.Item.itemType != (int)socket1.type)
        {
            return;
        }
        else
        {
            Item temp = socket2.Item;
            socket2.SetItem(item);
            socket1.SetItem(temp);
            foreach (ItemSocket socket in this.equipSockets)
            {
                if((int)item.itemType == (int)socket.type)
                {
                    temp = socket.Item;
                }
            }
            this.playerUI.OnEquipment(temp);
        }
    }
}
