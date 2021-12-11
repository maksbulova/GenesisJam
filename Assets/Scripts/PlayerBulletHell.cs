using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletHell : MonoBehaviour, IDamageble, IShoter
{
    [Header("Shot stats")]
    public float shotRate;
    public float bulletSpeed;
    public GameObject bulletPrefab;

    public void Shoot()
    {

    }

    public void TakeDamage()
    {

    }
}
