using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
    private ObjectPooler op;
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
        op = ObjectPooler.instance;
        shaker = CameraShake.instance;
        onShoot += ShootBullet;
    }

    // Initiates a "shoot"
    public void Shoot(float angle)
    {
        shaker.ShakeCameraDirectional(0.06f, angle);

        currAngle = angle;
        onShoot.Invoke();

        // Create shot effect
        GameObject eff = op.Create("ShootEffect", barrelTip.position, Quaternion.AngleAxis(angle, Vector3.forward));

        StartCoroutine(Disable(eff, 0.03f));
    }

    private void ShootBullet()
    {
        // Get the x and y velocities of the bullet
        float xVel = bulletSpeed * Mathf.Cos((currAngle + 90) * Mathf.Deg2Rad);
        float yVel = bulletSpeed * Mathf.Sin((currAngle + 90) * Mathf.Deg2Rad);

        GameObject shot = op.Create("Bullet", barrelTip.position, Quaternion.AngleAxis(currAngle, Vector3.forward));
        shot.GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel);

        StartCoroutine(Disable(shot, .6f));
    }

    private IEnumerator Disable(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
}
