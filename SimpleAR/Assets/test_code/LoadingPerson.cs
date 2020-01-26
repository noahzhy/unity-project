using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPerson : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject actorImage;

    void Start() {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log(">>>> space pressed");
            actorImage.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("001", typeof(Sprite)) as Sprite;
        }
        
    }
}
