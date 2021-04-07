using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    private Transform cam;

    private Vector3 defaultPos = new Vector3(0, 0, -10);
    private Quaternion defaultRot = Quaternion.identity;

    private void Awake()
    {
        instance = this;
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    public void ShakeCamera()
    {
        StopAllCoroutines();
        cam.position = defaultPos; //Reset to original postion
        cam.rotation = defaultRot; //Reset to original rotation

        StartCoroutine(Shake(0.1f));
    }

    public void ShakeCamera(float duration)
    {
        StopAllCoroutines();
        cam.position = defaultPos; //Reset to original postion
        cam.rotation = defaultRot; //Reset to original rotation

        StartCoroutine(Shake(duration));
    }

    public void ShakeCameraDirectional(float duration, float angle)
    {
        StopAllCoroutines();
        cam.position = defaultPos; //Reset to original postion
        cam.rotation = defaultRot; //Reset to original rotation

        StartCoroutine(ShakeDirectional(duration, -angle));
    }

    private IEnumerator Shake(float duration)
    {
        float counter = 0f;

        //Shake Speed
        const float speed = 0.08f;

        //Angle Rotation
        const float angleRot = 0.08f;

        float decreasePoint = duration / 2;

        //Do the actual shaking
        while (counter < duration)
        {
            counter += Time.deltaTime;
            float decreaseSpeed = speed;
            float decreaseAngle = angleRot;

            //Shake camera
            Vector3 tempPos = defaultPos + Random.insideUnitSphere * decreaseSpeed;
            tempPos.z = defaultPos.z;
            cam.position = tempPos;

            cam.rotation = defaultRot * Quaternion.AngleAxis(Random.Range(-angleRot, angleRot), new Vector3(0f, 0f, 1f));
            yield return null;


            //Check if we have reached the decreasePoint then start decreasing  decreaseSpeed value
            if (counter >= decreasePoint)
            {
                //Reset counter to 0 
                counter = 0f;
                while (counter <= decreasePoint)
                {
                    counter += Time.deltaTime;
                    decreaseSpeed = Mathf.Lerp(speed, 0, counter / decreasePoint);
                    decreaseAngle = Mathf.Lerp(angleRot, 0, counter / decreasePoint);

                    // Shake camera
                    tempPos = defaultPos + Random.insideUnitSphere * decreaseSpeed;
                    tempPos.z = defaultPos.z;
                    cam.position = tempPos;

                    cam.rotation = defaultRot * Quaternion.AngleAxis(Random.Range(-decreaseAngle, decreaseAngle), new Vector3(0f, 0f, 1f));

                    yield return null;
                }

                //Break from the outer loop
                break;
            }
        }
        cam.position = defaultPos; //Reset to original postion
        cam.rotation = defaultRot; //Reset to original rotation
    }

    private IEnumerator ShakeDirectional(float duration, float angle)
    {
        float counter = 0f;

        //Shake Speed
        const float speed = 0.08f;

        float decreasePoint = duration / 2;

        float xVal = Mathf.Sin(angle * Mathf.Deg2Rad);
        float yVal = Mathf.Cos(angle * Mathf.Deg2Rad);
        Vector3 dir = new Vector3(xVal, yVal, 0);

        //Do the actual shaking
        while (counter < duration)
        {
            counter += Time.deltaTime;
            float decreaseSpeed = speed;

            //Shake camera
            Vector3 tempPos = defaultPos + dir * Random.Range(-1f, 1f) * decreaseSpeed;
            tempPos.z = defaultPos.z;
            cam.position = tempPos;

            yield return null;


            //Check if we have reached the decreasePoint then start decreasing  decreaseSpeed value
            if (counter >= decreasePoint)
            {
                //Reset counter to 0 
                counter = 0f;
                while (counter <= decreasePoint)
                {
                    counter += Time.deltaTime;
                    decreaseSpeed = Mathf.Lerp(speed, 0, counter / decreasePoint);

                    // Shake camera
                    tempPos = defaultPos + dir * Random.Range(-1f, 1f) * decreaseSpeed;
                    tempPos.z = defaultPos.z;
                    cam.position = tempPos;

                    yield return null;
                }

                //Break from the outer loop
                break;
            }
        }
        cam.position = defaultPos; //Reset to original postion
        cam.rotation = defaultRot; //Reset to original rotation
    }
}