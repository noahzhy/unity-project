using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseActors : MonoBehaviour
{
    public GameObject actor;
    string[] actors = new string[5] { "actor1", "actor2", "actor1", "actor2", "actor1" };
    string path_actor = "";
    // Start is called before the first frame update
    void Start()
    {
        path_actor =  "actors/" + actors[1];
        actor.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load(path_actor, typeof(Sprite)) as Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            path_actor = "actors/" + actors[0];
            actor.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load(path_actor, typeof(Sprite)) as Sprite;
        }
    }
}
