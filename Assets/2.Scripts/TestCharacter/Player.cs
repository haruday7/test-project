using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rigid;
    public float moveSpeed = 50f;

    public PlayerUI ui;
    // Start is called before the first frame update
    void Start()
    {
        this.rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 inputDir = new Vector3(h, 0, v).normalized;

        this.rigid.velocity = inputDir * this.moveSpeed * Time.deltaTime;

        this.transform.LookAt(this.transform.position + inputDir);
    }
    public void GetItem(Item item)
    {
        this.ui.inven.PutItem(item);
    }
}
