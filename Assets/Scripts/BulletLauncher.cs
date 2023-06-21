using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    public GameObject Bullet;
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(Bullet, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        }
    }
}
