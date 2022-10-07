using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public GameObject itemPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            Item item = Instantiate<GameObject>(this.itemPrefab).GetComponent<Item>();
            item.Init();
            item.SetInfo(Resources.Load<Sprite>(this.gameObject.name));
            player.GetItem(item);
        }
    }
}
