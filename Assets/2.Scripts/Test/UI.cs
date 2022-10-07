using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public InputField inputFieldId;
    public InputField inputFieldName;
    public InputField inputFieldprice;

    public Button btnSend;
    // Start is called before the first frame update
    void Start()
    {
        this.btnSend.onClick.AddListener(() =>
        {
            StartCoroutine(FindObjectOfType<Django>().Res(CreateProduct()));
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Product CreateProduct()
    {
        int id = int.Parse(this.inputFieldId.text);
        string name = this.inputFieldName.text;
        int price = int.Parse(this.inputFieldprice.text);
        string created_at = DateTime.Now.ToString();

        Product product = new Product();
        product.id = id;
        product.name = name;
        product.price = price;
        product.created_at = created_at;
        return product;
    }
}
