using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    public GameObject bulletPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            GameObject bullet = 
                Instantiate(
                    bulletPrefab,
                    transform.position,
                    new Quaternion()
                ) as GameObject;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 worldDir = ray.direction;
            bullet.GetComponent<BulletController>().Shoot(
                worldDir.normalized * 2000
            );
        }
    }
}
