using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    private bool inAir = false;
    private bool dead = false;

    private Vector2 size = new Vector2(0.8f,0.2f);

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float gravity;
    private Vector3 respawnPoint;
    public Animator animator;
    private float resP;
    public GameObject Target;

    // Update is called once per frame
    void Update()
    {
        resP = Dialogue.index;
        respawnPoint = new Vector3(resP * 150f, gravity * 4f,0f);
        if(dead){
            Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime<=0.9f);
            return;
        }
        else if(gravity == 1){
            animator.Play("Player_Idle");
        }
        else{
            animator.Play("Blayer_Idle");
        }
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && (IsGrounded()||MirroredProperties.grounded||MirroredProperties.invGrounded))
        {
            rb.velocity = new Vector2(rb.velocity.x, gravity*jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && gravity*rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {  
        if(!IsGrounded()&&(MirroredProperties.grounded||MirroredProperties.invGrounded) == false)
            inAir = true;

        if(IsGrounded()&&gravity == 1f){

            MirroredProperties.grounded = true;
        }
        else if(IsGrounded()&&gravity == -1f){
            MirroredProperties.invGrounded = true;
        }
        else if(gravity == 1f)
            MirroredProperties.grounded = false;
        else
            MirroredProperties.invGrounded = false;
    
        Vector3 vec = new Vector3(Target.transform.position.x, -1f*Target.transform.position.y, Target.transform.position.z);
        Vector2 tmp = new Vector2(Target.transform.position.x, -1f*Target.transform.position.y);
        if(transform.position != vec && !IsGrounded()){
            transform.position = Vector2.MoveTowards(transform.position, tmp, 8 * Time.deltaTime);
        }

        if((vec != transform.position)){
            transform.position = Vector2.MoveTowards(transform.position, tmp, 2 * Time.deltaTime);
        }
        
        if(MirroredProperties.grounded||MirroredProperties.invGrounded){
            if(inAir){
                inAir = false;
                rb.velocity = new Vector2(horizontal * speed, 0f);
            }
            rb.gravityScale = 0f;
        }
        else{
            rb.gravityScale = gravity*4f;
        }
        
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, size, 0f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private IEnumerator isDead()
    {
        dead = true;
        Time.timeScale = 0;
        if(gravity == 1){
            animator.Play("Player_Death");
        }
        else{
            animator.Play("Blayer_Death");
        }
        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.9f);
        Time.timeScale = 1; 
        dead = false;
        transform.position = respawnPoint;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.layer == 8)
        {
            StartCoroutine(isDead());
        }
    }
}
