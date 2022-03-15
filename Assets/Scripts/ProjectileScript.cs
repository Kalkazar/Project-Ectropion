using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Vector2 screenBounds;
    private Rigidbody2D rb;
    public float xDir = 0f; // is this right? consider later
    public float yDir = 0f; // is this right? consider later

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)
        );
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(xDir, yDir);
    }

    // Update is called once per frame
    void Update()
    {
        if(OutOfBounds()) { // add something for when a bullet hits a wall
            Destroy(this.gameObject);
        }
    }

    private bool OutOfBounds() {
        return transform.position.x < -screenBounds.x * 2
            || transform.position.y < -screenBounds.y * 2
            || transform.position.x > screenBounds.x * 2
            || transform.position.y > screenBounds.y * 2;
    }

    // public void SetDirection(float xDir, float yDir) { // TODO: CONSIDER USING C# SHORTCUTS
    //     //
    // }
}
