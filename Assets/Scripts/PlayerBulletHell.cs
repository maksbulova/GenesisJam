using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletHell : MonoBehaviour, IShotable, IShoter
{
    [Header("Shot stats")]
    public float reloadTime;
    public float bulletSpeed;
    public GameObject bulletPrefab;
    public Transform shotBarrel;

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shotBarrel.position, Quaternion.identity, shotBarrel);
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.velocity = Vector3.forward * bulletSpeed;
    }

    private void Start()
    {
        StartCoroutine(Shoting());
    }

    public void TakeDamage()
    {

    }

    private IEnumerator Shoting()
    {
        while (Application.isPlaying)
        {
            Shoot();
            yield return new WaitForSeconds(reloadTime);
        }
    }
}
