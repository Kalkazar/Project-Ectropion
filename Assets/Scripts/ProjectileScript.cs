using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour // TODO: consider renaming to just "Projectile"
{
    private Vector2 screenBounds;
    private Rigidbody2D rb;
    public float xDir = 0f; // is this right? consider later
    public float yDir = 0f; // is this right? consider later
    public float speed = 7f; // consider making private. Also consider changing speed

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)
        );
        rb = this.GetComponent<Rigidbody2D>();
        // TODO: should probably do the rest of the "SetDirection" stuff here
        rb.velocity = new Vector2(xDir * speed, yDir * speed); // TODO: consider moving elsewhere, or deleting
    }

    // Update is called once per frame
    void Update()
    {
        if (OutOfBounds()) { // TODO: add something for when a bullet hits a wall or an enemy
            Destroy(this.gameObject);
        }
    }

    private bool OutOfBounds() {
        return transform.position.x < -screenBounds.x * 2
            || transform.position.y < -screenBounds.y * 2
            || transform.position.x > screenBounds.x * 2
            || transform.position.y > screenBounds.y * 2;
    }

    public void SetDirection(float xDirNew, float yDirNew) {
        xDir = xDirNew;
        yDir = yDirNew;
        float rotation = Mathf.Rad2Deg * Mathf.Atan(yDir/xDir);
        if (xDir < 0) {
            rotation += 180;
        }
        transform.localRotation = Quaternion.Euler(0, 0, rotation);
        rb.velocity = new Vector2(xDir * speed, yDir * speed);
    }
}
