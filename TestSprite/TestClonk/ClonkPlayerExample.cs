using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer), typeof(Animator), typeof(Rigidbody2D))]
public class ClonkPlayerExample : MonoBehaviour
{
    // This is an Example Unity Clonk Controller, it imitates the Clonk Controlls from the original Clonk Game but in a very basic way.
    public float walkSpeed = 1f;
    public float jumpForce = 1f;
    public bool jumpAndRun = false; // if true, the player is stop moving when the direction buttons are released, if false we use the classic clonk controls where the player keeps moving
    public float afterJumpRecoverTime = 0.5f;

    //Calculations
    [ShowInInspector]
    private int _moveDirection = 0; // Store the Current move direction, 0 = no movement, 1 = right, -1 = left
    [ShowInInspector]
    private float _aboveGround = 0f; // Store the distance to the ground, if this is 0 we are on the ground if not we are in the air.
    [ShowInInspector]
    private bool _isJumping = false; // Store if we are initiating a jump, if true and we are in the air we are jumping, if not and we are in the air we are falling
    private bool _isJumpRecovering = false; // if true we are in the after jump recover time, means we cant controll

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;


    // Use the folowing Controlls. While we making a Retro Style game we use buttons instead of axises to controll movement
    // Controlls: Y=Left, X=Stop/Down, C=Right, S=Jump, A=Throw Item, D=Dig

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        GroundCheck();
        if (_isJumpRecovering) return;
        Movement();
        Jump();
    }

    private void Movement()
    {
        if (_aboveGround != 0) return;
        if (Input.GetKey(KeyCode.Y))
        {
            _moveDirection = -1;
            _animator.SetFloat("WalkSpeed", 1f);
            _spriteRenderer.flipX = false;
        }
        if (Input.GetKey(KeyCode.X))
        {
            _moveDirection = 0;
            _animator.SetFloat("WalkSpeed", 0f);
        }
        if (Input.GetKey(KeyCode.C))
        {
            _moveDirection = 1;
            _animator.SetFloat("WalkSpeed", 1f);
            _spriteRenderer.flipX = true;
        }
        // Move the Player to the left by walkSpeed
        _rigidbody2D.velocity = new Vector2(walkSpeed * _moveDirection, _rigidbody2D.velocity.y);
    }
    private void Jump()
    {

        if(Input.GetKey(KeyCode.S) && _aboveGround == 0) {
            // Jump player up and in the current standing direction forward
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
            _isJumping= true;
            _isJumpRecovering = true;
            StartCoroutine(AfterJumpRecover());
        }
    }
    private IEnumerator AfterJumpRecover()
    {
        yield return new WaitForSeconds(afterJumpRecoverTime);
        _isJumpRecovering = false;
    }

    private void GroundCheck()
    {

        // making a Physics2D.OverlapCircle on the Layer "WalkableCollider" and return a bool true if there is overlap
        bool isGrounded = Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("WalkableCollider"));
        if (isGrounded)
        {
        // if we are grounded we set the _aboveGround to 0
            _aboveGround = 0f;
            _animator.SetBool("IsGrounded", true);
            _animator.SetBool("IsJumping", false);
            
        }
        else
        {
            _animator.SetBool("IsGrounded", false);
            if (_isJumping)
            {
                _animator.SetBool("IsJumping", true);
            } else
            {
                _animator.SetBool("IsJumping", false);
            }
            _aboveGround = Mathf.Abs(Physics2D.Raycast(transform.position, Vector2.down, 100f, LayerMask.GetMask("WalkableCollider")).distance);
        }
    }

}
