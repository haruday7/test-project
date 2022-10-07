using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public PlayerUI playerUI;

    private List<ItemSocket> listInvenSocket = new List<ItemSocket>();
    public ItemSocket[] invenSockets;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetSocketList(List<ItemSocket> list)
    {
        this.listInvenSocket = list;
        this.invenSockets = this.listInvenSocket.ToArray();
        foreach (ItemSocket socket in this.listInvenSocket)
        {
            socket.type = ItemSocket.eType.Inventory;
        }
    }
    public void PutItem(Item item)
    {
        FindEmptySocket().SetItem(item);
    }
    public ItemSocket FindEmptySocket()
    {
        ItemSocket socket = null;
        for (int i = 0; i < this.invenSockets.Length; i++)
        {
            if (!this.invenSockets[i].CheckItem())
            {
                socket = this.invenSockets[i];
                break;
            }
        }
        return socket;
    }
    public ItemSocket FindNearSocket(Transform trans)
    {
        ItemSocket itemSocket = null;
        float dir = int.MaxValue;
        for (int i = 0; i < this.listInvenSocket.Count; i++)
        {
            float dis = Vector3.Distance(this.listInvenSocket[i].transform.position, trans.position);
            if (dir > dis)
            {
                dir = dis;
                itemSocket = this.listInvenSocket[i];
            }
        }
        return itemSocket;
    }
    public void ChangeItem(Item item, ItemSocket socket1, ItemSocket socket2)
    {
        Item temp = socket2.Item;
        socket2.SetItem(item);
        socket1.SetItem(temp);
    }
}
