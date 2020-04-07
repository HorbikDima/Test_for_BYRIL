using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawn : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float timeShoot = 1f;
    // Update is called once per frame
    private void Update()
    {
        timeShoot -= Time.deltaTime;
        if(timeShoot < 0)
        {
            Shoot();
            timeShoot = 1f;
        }
    }
    private void Shoot()
    {
        Instantiate(laserPrefab, gameObject.transform.position,gameObject.transform.rotation);
    }
}
