using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject a;
    public GameObject b;
    public Transform tr;

    void Start()
    {

        //Prof p = new Prof();
        //p.name = "Noah";
        //p.major = "CS Dept.";
        //p.Talk("good");

        //Switch(a, b);

        Student s = new Student();
        s.korean = 90;
        s.math = 85;
        s.english = 80;
        s.science = 70;
        s.GPA();
        
    }

    void Switch(GameObject a, GameObject b) {
        Vector3 temp = a.transform.position;
        a.transform.position = b.transform.position;
        b.transform.position = temp;

    }

    // Update is called once per frame
    void Update()
    {
        tr.Translate(Vector3.right*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        
    }
}
