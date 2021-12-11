using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IShoter, IShotable
{
    [Header("Shot stats")]
    public float shotRate;
    public float bulletSpeed;
    public GameObject bulletPrefab;

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gameObject.transform);

    }

    public void TakeDamage()
    {
        // TODO 
        Destroy(gameObject);

    }

}
