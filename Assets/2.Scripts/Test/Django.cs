using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Django : MonoBehaviour
{
    private Dictionary<int, Product> dicProducts = new Dictionary<int, Product>();
    private Text[] txtTest;
    private string jsonProduct = "api/product/";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Req(this.jsonProduct));
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator Req(string type)
    {
        Debug.Log("Req 시작");
        using (UnityWebRequest req = UnityWebRequest.Get("http://localhost:8000/" + type))
        {
            yield return req.SendWebRequest();
            try
            {
                string json = req.downloadHandler.text;

                this.dicProducts = JsonConvert.DeserializeObject<Product[]>(json).ToDictionary(x => x.id);
                foreach (KeyValuePair<int, Product> pair in this.dicProducts)
                {
                    Debug.LogFormat("id: {0}\nname: {1}\nprice: {2}\ncreated_at: {3}", pair.Value.id, pair.Value.name, pair.Value.price);
                }
            }catch(Exception e)
            {

            }
        }
    }
    public IEnumerator Res(Product product)
    {
        using (UnityWebRequest res = new UnityWebRequest("http://localhost:8000/rest/", "POST"))
        {
            try
            {
                Debug.Log("res시작");
                string json = JsonConvert.SerializeObject(product);
                Debug.Log("json " + json);
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
                res.uploadHandler = new UploadHandlerRaw(bodyRaw);
                res.downloadHandler = new DownloadHandlerBuffer();
                res.SetRequestHeader("Content-Type", "application/json");
                Debug.Log("res완료");
            }
            catch (Exception e)
            {

            }
            yield return res.SendWebRequest();
        }
    }
}
