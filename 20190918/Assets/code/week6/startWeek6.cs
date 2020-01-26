using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startWeek6 : MonoBehaviour
{
    //public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < 10; i++) {
        //    Instantiate(cube, new Vector3(i, 0, 0), Quaternion.identity);
        //}


    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float m_speed = 10;

        transform.Translate(Vector3.forward * vertical * m_speed * Time.deltaTime);
        transform.Translate(Vector3.right * horizontal * m_speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "cube") {
            Destroy(collision.gameObject);

        }
    }
}
