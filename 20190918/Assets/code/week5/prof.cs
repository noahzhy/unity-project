using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Prof {
    public string name;
    public string major;

    public void Talk(string sentence) {
        Debug.Log(name + '/' + major + '/' + sentence);

    }
}