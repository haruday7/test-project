using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

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
        if(!((int)item.itemType != (int)socket.type))
        {
            Debug.Log("Fail");
            return;
        }
        else
        {
            item.socket.SetItem(null);
            socket.SetItem(item);
            this.playerUI.OnEquipment(item);
        }
    }
    public void ChangeEquipItem(Item item, ItemSocket socket1, ItemSocket socket2)
    {
        Item temp = socket2.Item;
        socket2.SetItem(item);
        socket1.SetItem(temp);
    }
}
