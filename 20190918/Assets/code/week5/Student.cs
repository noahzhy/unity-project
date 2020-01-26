using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student {
    public string name;
    public int korean, math, english, science;

    public void GPA() {
        float avg;
        avg = (korean+ math+ english+ science)/ 4.0f;
        Debug.Log("Your avg. score is "+avg);
    }
}
