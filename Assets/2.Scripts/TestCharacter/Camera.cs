using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject target;

    public float offsetX; 
    public float offsetY;
    public float offsetZ;

    public float DelayTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 FixedPos = new Vector3(this.target.transform.position.x + offsetX,
                                        this.target.transform.position.y + offsetY, 
                                        this.target.transform.position.z + offsetZ);
        this.transform.position = Vector3.Lerp(transform.position, FixedPos, 
                                                Time.deltaTime * this.DelayTime);
    }
}
