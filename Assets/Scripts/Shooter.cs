using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shotEffect;

    private CameraShake shaker;
    private UnityAction onShoot;
    private Transform barrelTip;

    private float currAngle;
    private float bulletSpeed = 30;

    private void Awake()
    {
        barrelTip = transform.Find("BarrelTip");
    }

    private void Start()
    {
        shaker = CameraShake.instance;
        onShoot += ShootBullet;
    }

    // Initiates a "shoot"
    public void Shoot(float angle)
    {
        shaker.ShakeCamera(0.075f);

        currAngle = angle;
        onShoot.Invoke();

        // Create shot effect
        GameObject eff = Instantiate(shotEffect, barrelTip.position, Quaternion.AngleAxis(angle, Vector3.forward));
        Destroy(eff, 0.05f);
    }

    private void ShootBullet()
    {
        // Get the x and y velocities of the bullet
        float xVel = bulletSpeed * Mathf.Cos((currAngle + 90) * Mathf.Deg2Rad);
        float yVel = bulletSpeed * Mathf.Sin((currAngle + 90) * Mathf.Deg2Rad);

        GameObject shot = Instantiate(bullet, barrelTip.position, Quaternion.AngleAxis(currAngle, Vector3.forward));
        shot.GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);

        Destroy(shot, 1);
    }
}
