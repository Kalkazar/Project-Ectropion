using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ProjectileScript; // consider using a "Namespace", whatever that is

public class PlayerController : MonoBehaviour // TODO: consider renaming
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    public SpriteRenderer spriteRenderer;
    public GameObject bulletPrefab; // TODO: consider moving elsewhere

    [SerializeField] private LayerMask jumpableGround; // LEARN WHAT THIS IS LATER
    
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

    private float runSpeed = 3.0f; // TODO: consider changing
    private float crouchSpeed = 1.0f; // TODO: consider changing. Maybe make slower
    private bool crouching = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        // TODO: consider adding the following to a new method. Like "FireRifle()"
        float shootX = Input.GetAxisRaw("Fire Horizontal"); // TODO: consider not using axes for this
        float shootY = Input.GetAxisRaw("Fire Vertical"); // TODO: consider not using axes for this
        // TODO: ADD MORE STUFF LATER
        if (shootX != 0 || shootY != 0) {
            Debug.Log("shooting called: xDir=" + shootX + ", yDir=" + shootY); // TODO: delete later
            FireBullet(shootX, shootY);
        }
    }

    private bool IsGrounded() { // TODO: consider moving down
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private void Move() { // consider renaming "Movement()"
        float moveX = Input.GetAxisRaw("Move Horizontal");
        float moveY = Input.GetAxisRaw("Move Vertical");
        crouching = moveY < 0 && IsGrounded(); // TODO: maybe make it so you can flip in the air
        rb.velocity = new Vector2(moveX * (crouching ? crouchSpeed : runSpeed), rb.velocity.y);
        spriteRenderer.sprite = crouching ? idleCrouchingAnim : idleAnim;
        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, 7f);
            // TODO: make it so you can control jump height by holding down the jump button
            // ALSO TODO: consider changing fall speed while firing
        }
    }

    // TODO: consider changing fall speed while firing
    private void FireBullet(float xDir, float yDir) { // TODO: consider renaming. Maybe "FireRifle"
        GameObject obj = Instantiate(bulletPrefab) as GameObject;
        float aimHeight = transform.position.y;
        if (crouching) {
            aimHeight -= (coll.bounds.size.y / 4);
        } else {
            aimHeight += (coll.bounds.size.y / 4);
        }
        obj.transform.position = new Vector2(transform.position.x, aimHeight); 
        ProjectileScript projectile = obj.GetComponent<ProjectileScript>();
        projectile.SetDirection(xDir, yDir);
    }
}
