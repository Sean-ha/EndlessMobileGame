using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchShoot : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform barrel;

    private Shooter shooter;

    private void Start()
    {
        shooter = barrel.GetComponent<Shooter>();
    }

    private void Update()
    {
        // If the player clicks / taps
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(pos);
            RaycastHit2D hitInfo = Physics2D.Raycast(worldPos, Vector2.zero);

            // Check hitInfo to see which collider has been hit, and act appropriately.
            if (hitInfo)
            {
                // If tapped area is on shooting area
                if(hitInfo.transform.gameObject.GetComponent<TouchShoot>())
                {
                    // Get angle between player and tapped location
                    Vector2 diff = worldPos - (Vector2)player.position;
                    float hyp = Mathf.Sqrt((diff.x * diff.x) + (diff.y * diff.y));

                    float angle = Mathf.Rad2Deg * Mathf.Acos(diff.x / hyp) - 90;

                    barrel.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    shooter.Shoot(angle);
                }
            }
        }
    }
}
