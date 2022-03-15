using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour // TODO: consider renaming
{
    private Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    
    // consider renaming the following. Maybe remove the "anim" from them
    // also consider making them something other than sprites
    public Sprite idleAnim;
    public Sprite idleCrouchingAnim;

    // Consider making this an array. Or a Map. Or dictionary, or whatever they call it in C#
    public Sprite shootForwardAnim;
    public Sprite shootBackwardsAnim;
    public Sprite shootUpAnim;
    public Sprite shootDownAnim;

    // CREATE MORE SPRITES FOR CROUCHING AND JUMPING

    private bool crouching = false; // is this necessary? Consider deleting

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Move Horizontal"); // TODO: make movement speed slower when crouching
        float moveY = Input.GetAxisRaw("Move Vertical");
        // Debug.Log(moveY);
        rb.velocity = new Vector2(moveX * 3.0f, rb.velocity.y); // TODO: consider changing speed
        crouching = moveY < 0; // consider turning into a ternary
        if (crouching) {
            spriteRenderer.sprite = idleCrouchingAnim; // consider simplifying. Also move it down
        } else {
            spriteRenderer.sprite = idleAnim;
        }
        // if (Input.GetKeyDown("space") || moveY > 0) {
        if (Input.GetButtonDown("Jump")) {
            rb.velocity = new Vector2(rb.velocity.x, 7f);
            // Make it so you can't jump in the air. Also control jump height by holding down button
        }
    }
}
