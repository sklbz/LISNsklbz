using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour {
    public int keyNumber;
    string key;
    string[] keys = {"q", "a", "r"};
    Vector3 speed = new Vector3 (0, 0.1f, 0);
    void Start() {
        key = keys[keyNumber];
        Debug.Log(key);
        Debug.Log(keyNumber);
    }
    void Update() {
        if (Input.GetKeyDown(keys[keyNumber]))
        {
            transform.position = transform.position + speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "FinishLine")
        {
            Debug.Log("Team {key} won!");
        }
    }
}