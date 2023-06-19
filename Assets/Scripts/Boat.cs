using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour {
    public int keyNumber;
    string[] keys = {"q", "a", "r"};
    Vector3 speed = new Vector3 (0, 0.1f, 0);
    void Update() {
        if (Input.GetKeyDown(keys[keyNumber]))
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + speed, 0.75f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log($"Team {keyNumber} won!");
        }
    }
}