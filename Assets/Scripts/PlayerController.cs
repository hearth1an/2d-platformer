using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Animator _animator;

    private float _horizontal;
    private float _movementSpeed = 8f;
    private float _jumpPower = 20f;
    private bool _isFacingRight = true;

    private const string Speed = nameof(Speed);
    private const string IsJumping = nameof(IsJumping);

    private void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");

        _animator.SetFloat(Speed, Mathf.Abs(_horizontal));
        _animator.SetBool(IsJumping, !IsGrounded());

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpPower);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(_horizontal * _movementSpeed, _rigidBody.velocity.y);
    }

    private bool IsGrounded()
    {
        float radius = 0.2f;

        _animator.SetBool(IsJumping, false);

        return Physics2D.OverlapCircle(_groundCheck.position, radius, _groundLayer);
    }

    private void Flip()
    {
        float minValue = 0f;
        float mirrorPositionValue = -1f;

        if (_isFacingRight && _horizontal < minValue || !_isFacingRight && _horizontal > minValue)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= mirrorPositionValue;
            transform.localScale = localScale;
        }
    }
}