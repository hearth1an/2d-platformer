using UnityEngine;

[RequireComponent (typeof(Rigidbody2D), typeof(Animator), typeof(PlayerInputReader))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerInputReader _inputReader;

    private float _horizontal;
    private float _movementSpeed = 8f;
    private float _jumpPower = 20f;
    private bool _isFacingRight = true;

    private const string Speed = nameof(Speed);
    private const string IsJumping = nameof(IsJumping);

    private void Awake()
    {
        _inputReader = GetComponent<PlayerInputReader>();
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {  
        _animator.SetFloat(Speed, Mathf.Abs(_inputReader.HorizontalInput));
        _animator.SetBool(IsJumping, !IsGrounded());

        if (_inputReader.JumpInput && IsGrounded())
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpPower);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(_inputReader.HorizontalInput * _movementSpeed, _rigidBody.velocity.y);
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

        if (_isFacingRight && _inputReader.HorizontalInput < minValue || !_isFacingRight && _inputReader.HorizontalInput > minValue)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= mirrorPositionValue;
            transform.localScale = localScale;
        }
    }
}