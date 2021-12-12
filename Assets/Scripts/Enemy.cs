using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IShoter, IShotable
{
    public float turnSpeed;
    [Header("Shot stats")]
    public float reloadTime;
    public float bulletSpeed;
    public GameObject bulletPrefab;
    private static Transform shotBarrel;
    public AnimationCurve movementCurve;

    public static float[] moveLine = new float[] { 3, 4, 5, 6 };
    public static Enemy[] enemies = new Enemy[moveLine.Length];
    private int myIndex;

    private static LevelInfo levelInfo;

    private void Awake()
    {
        if (shotBarrel == null)
        {
            shotBarrel = new GameObject("Enemy bullet container").transform;
        }

        levelInfo = FindObjectOfType<LevelInfo>();
    }

    private void Start()
    {
        bool spaceAvaiable = false;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
            {
                spaceAvaiable = true;
                enemies[i] = this;
                myIndex = i;
            }
        }
        if (!spaceAvaiable)
        {
            Destroy(gameObject);
        }

        // enemy parametrs +- 20% differs (ux only)
        const float rndVariety = 0.2f;
        reloadTime = Random.Range(reloadTime * (1 - rndVariety), reloadTime * (1 + rndVariety));
        turnSpeed = Random.Range(turnSpeed * (1 - rndVariety), turnSpeed * (1 + rndVariety));

        StartCoroutine(Movement());
        StartCoroutine(Shoting());
    }


    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, shotBarrel);
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.velocity = Vector3.back * bulletSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        GetComponent<Collider>().enabled = false;
        StopAllCoroutines();
        // TODO falldown and effects
        enemies[myIndex] = null;
        Destroy(gameObject);
    }

    IEnumerator Movement()
    {
        float leftBorder = levelInfo.playerController.leftMovementBound;
        float rightBorder = levelInfo.playerController.rightMovementBound;
        float timer = 0;

        while (Application.isPlaying)
        {
            Vector3 playerPosition = levelInfo.playerController.transform.position;
            Vector3 movePosition = playerPosition + Vector3.forward * moveLine[myIndex];
            float t = movementCurve.Evaluate(timer * turnSpeed);
            float moveX = (t * (rightBorder - leftBorder)) + leftBorder;
            movePosition.x = moveX;

            transform.position = movePosition;
            yield return null;

            timer += Time.deltaTime;
        }
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
