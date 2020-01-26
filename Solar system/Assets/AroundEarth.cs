using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundEarth : MonoBehaviour
{
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(0, 270 * Time.deltaTime, 0);
        transform.RotateAround(obj.transform.position, Vector3.up, 540 * Time.deltaTime);

    }
}
