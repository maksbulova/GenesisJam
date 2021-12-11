using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IShoter, IShotable
{
    
    [Header("Shot stats")]
    public float shotRate;
    public float bulletSpeed;
    public GameObject bulletPrefab;
    public Vector3 moveZoneWidth, moveZoneHeight;

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gameObject.transform);
    }

    public void TakeDamage()
    {
        // TODO 
        Destroy(gameObject);
    }

    IEnumerator Movement()
    {
        Vector3 ChooseMovePosition()
        {
            float rndX = Random.Range(-moveZoneWidth.x, moveZoneWidth.x);
        }
        Vector3 movePosition;

        yield return null;
    }
    
}
