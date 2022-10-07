using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerUI : MonoBehaviour
{
    public Player player;

    private List<ItemSocket> listSockets = new List<ItemSocket>();
    private List<GameObject> listArmorObjs = new List<GameObject>();
    private GameObject armorObj;
    private List<GameObject> listMustacheObjs = new List<GameObject>();
    private GameObject mustacheObj;
    private List<GameObject> listHairObjs = new List<GameObject>();
    private GameObject hairObj;
    public Inventory inven;
    public Equipment equip;
    // Start is called before the first frame update
    void Start()
    {
        this.listSockets = GetComponentsInChildren<ItemSocket>().ToList();
        Setting();
    }

    private void Setting()
    {
        List<ItemSocket> temp = this.listSockets.Where(x => x.transform.root.GetChild(0).name == "Inventory")
                                                .ToList();
        this.inven.SetSocketList(temp);
        temp = this.listSockets.Where(x => x.transform.parent.name == "Equipment").ToList();
        this.equip.SetSocketList(temp);

        GameObject[] childObjs = new GameObject[this.player.gameObject.transform.childCount];
        for (int i = 0; i < childObjs.Length; i++)
        {
            childObjs[i] = this.player.gameObject.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < childObjs.Length; i++)
        {
            if (childObjs[i].name.Contains("Armor"))
            {
                this.listArmorObjs.Add(childObjs[i]);
            }
            else if (childObjs[i].name.Contains("mustache"))
            {
                this.listMustacheObjs.Add(childObjs[i]);
            }
            else if (childObjs[i].name.Contains("hair"))
            {
                this.listHairObjs.Add(childObjs[i]);
            }
        }
    }
    public ItemSocket FindNearSocket(Transform trans)
    {
        ItemSocket itemSocket = null;
        float dir = int.MaxValue;
        for (int i = 0; i < this.listSockets.Count; i++)
        {
            float dis = Vector3.Distance(this.listSockets[i].transform.position, trans.position);
            if (dir > dis)
            {
                dir = dis;
                itemSocket = this.listSockets[i];
            }
        }
        return itemSocket;
    }
    public void OnEquipment(Item item)
    {
        GameObject obj = null;
        if (item.name.Contains("Armor"))
        {
            for (int i = 0; i < this.listArmorObjs.Count; i++)
            {
                if(item.name == this.listArmorObjs[i].name)
                {
                    obj = this.listArmorObjs[i];
                    if (this.armorObj != null) this.armorObj.SetActive(false);
                    this.armorObj = obj;
                    break;
                }
            }
        }
        else if (item.name.Contains("mustache"))
        {
            for (int i = 0; i < this.listMustacheObjs.Count; i++)
            {
                if (item.name == this.listMustacheObjs[i].name)
                {
                    obj = this.listMustacheObjs[i];
                    if (this.mustacheObj != null) this.mustacheObj.SetActive(false);
                    this.mustacheObj = obj;
                    break;
                }
            }
        }
        else if (item.name.Contains("hair"))
        {
            for (int i = 0; i < this.listHairObjs.Count; i++)
            {
                if (item.name == this.listHairObjs[i].name)
                {
                    obj = this.listHairObjs[i];
                    if (this.hairObj != null) this.hairObj.SetActive(false);
                    this.hairObj = obj;
                    break;
                }
            }
        }
        Debug.Log(item.gameObject.name);
        Debug.Log(obj.name);
        obj.SetActive(true);
    }
    public void BeginDrag()
    {

    }
    public void Drag()
    {

    }
    public void EndDrag()
    {

    }
}
