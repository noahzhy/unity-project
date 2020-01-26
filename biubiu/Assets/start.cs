using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class start : MonoBehaviour
{
    public float m_speed = 20f;
    public float timeCount = 0f;
    public float times;
    public float level = 1;

    public bool GAME_START = false;
    public bool GAME_OVER = false;

    public Text survival_time;
    public GameObject bullet;
    public GameObject targert = null;

    // Start is called before the first frame update
    void Start() {
        timeCount = Time.deltaTime;
    }

    // Update is called once per frame
    void Update() {
        if (Input.anyKey) {
            GAME_START = true;
        }

        if (GAME_START && !GAME_OVER) {
            MovePlayer();
            TimeCount();
            Biubiu();
            LevelUp();
        }

        if (GAME_OVER) {
            survival_time.text = "Game Over!";
            survival_time.color = Color.red;
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Bullet") {
            Debug.Log(collision.gameObject.tag);
            GAME_OVER = true;
        }
    }

    void TimeCount() {
        timeCount += Time.deltaTime;
        survival_time.text = timeCount.ToString();
    }

    void MovePlayer() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * vertical * m_speed * Time.deltaTime);
        transform.Translate(Vector3.right * horizontal * m_speed * Time.deltaTime);
    }

    void Biubiu() {
        times -= Time.deltaTime;
        if (times < 0) {
            for (int i = 0; i< level; i++) {
                int x = Random.Range(-25, 25);
                int z = Random.Range(-25, 25);
                Instantiate(bullet, new Vector3(x, 0, z), Quaternion.identity);
            }
            times = 3;
        }
    }

    void LevelUp() {
        level = timeCount / 5;
    }

}
