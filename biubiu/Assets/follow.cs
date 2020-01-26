using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour {
    public float speed;
    void Start()
    {
        speed = Random.Range(10, 20);
        float direction = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0, direction, 0);
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.forward*speed*Time.deltaTime);
    }

        void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Bullet") {
            Destroy(collision.gameObject);
        }
    }
}
