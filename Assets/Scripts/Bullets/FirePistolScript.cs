using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePistolScript : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float bulletSpeed = 10.0f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip fireSound;

    public void FirePistolBullet()
    {
        GameObject spawnBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        spawnBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * bulletSpeed;
        audioSource.PlayOneShot(fireSound);
        Destroy(spawnBullet, 5.0f);
    }
}