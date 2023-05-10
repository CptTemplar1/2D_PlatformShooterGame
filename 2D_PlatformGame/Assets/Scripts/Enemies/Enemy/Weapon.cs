using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    float range;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Transform playerTransform;
    private bool isShooting = false;

    private void Start()
    {
        range = GetComponent<ShootingEnemy>().getDetectionRange();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // znalezienie gracza przez tag
        isShooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) <= range && isShooting == false)
        {
            isShooting = true;
            Shoot();
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.5f);
        // spawnujemy pocisk na scenie w miejscu przed luf¹
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        isShooting = false;
    }
}
