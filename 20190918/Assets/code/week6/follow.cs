using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    public GameObject parent;
    float delay = 1;
    float time = 0;

    List<Vector3> trace = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        trace.Add(parent.transform.position);
        if (time < delay) {
            time += Time.deltaTime;
        }
        else {
            transform.position = trace[0];
            trace.RemoveAt(0);
        }
        
    }
}
