using UnityEngine;

[RequireComponent (typeof(PlayerInputReader), typeof(PlayerAnimController))]
public class PlayerMovingController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private PlayerAnimController _playerAnimController;
    [SerializeField] private PlayerInputReader _inputReader;
    [SerializeField] private PlayerCollisionDetector _collisionDetector;
    [SerializeField] private Transform _visual;

    private float _movementSpeed = 8f;
    private float _jumpPower = 20f;
    private bool _isFacingRight = true;

    private void Awake()
    {
        _inputReader = GetComponent<PlayerInputReader>();
        _collisionDetector = GetComponent<PlayerCollisionDetector>();
        _rigidBody = GetComponent<Rigidbody2D>();

        _inputReader.JumpPressed += Jump;
    }

    private void OnDestroy()
    {
        _inputReader.JumpPressed -= Jump;
    }

    private void Update()
    {
        _playerAnimController.TriggerRun(_inputReader.HorizontalInput);
        _playerAnimController.TriggerJump(_collisionDetector.IsGrounded == false);

        Flip();
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(_inputReader.HorizontalInput * _movementSpeed, _rigidBody.velocity.y);
    }    

    private void Flip()
    {
        float minValue = 0f;
        float mirrorPositionValue = -1f;

        if (_isFacingRight && _inputReader.HorizontalInput < minValue || _isFacingRight == false && _inputReader.HorizontalInput > minValue)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 localScale = _visual.localScale;
            localScale.x *= mirrorPositionValue;
            _visual.localScale = localScale;
        }
    }

    private void Jump()
    {
        if (_collisionDetector.IsGrounded)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpPower);
        }
    }
}