using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstScriptf : MonoBehaviour {
    public GameObject cube;
    public int score = 85;
    public char grade = 'F';

    public int[] sum_nums = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    public int total_sum = 0;
    // Start is called before the first frame update
    void Start() {


        //for (int i = 0; i<sum_nums.Length; i++) {
        //    total_sum += sum_nums[i];
        //}
        //Debug.Log("Sum is " + total_sum);

        //for (int i = 0; i < 9; i++) {
        //    for (int j = i; j < 9; j++) {
        //        Instantiate(cube, new Vector3(i*2, 0, j*2), Quaternion.identity);
        //    }
        //}

        //switch (score / 10) {
        //    case 10:
        //        grade = 'A';
        //        break;
        //    case 9:
        //        grade = 'A';
        //        break;
        //    case 8:
        //        grade = 'B';
        //        break;
        //    case 7:
        //        grade = 'C';
        //        break;
        //    case 6:
        //        grade = 'D';
        //        break;
        //    default:
        //        grade = 'F';
        //        break;
        //}
        //Debug.Log("Grade is " + grade);









    }

    // Update is called once per frame
    void Update() {

    }

    public class Prof {
        public string name;
        public string major;

        public void Talk(string sentence) {
            Debug.Log(name + '/' + major + '/' + sentence);

        }
    }
}
