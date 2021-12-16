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
    PlayerController controller;
    public LevelManager levelManager;



    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shotBarrel.position, Quaternion.identity);
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.velocity = Vector3.forward * (bulletSpeed + controller.playerForwardSpeed);
    }

    private void Start()
    {
        controller = FindObjectOfType<PlayerController>();
        StartCoroutine(Shoting());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        Debug.Log("Player hited");
        levelManager.StartCoroutine(levelManager.ChangeWorld());
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
