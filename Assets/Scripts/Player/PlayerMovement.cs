using UnityEngine;

/// <summary>
/// Движение игрока: ходьба, прыжок и вариативная высота прыжка.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _jumpForce = 14f;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheck;

    private Rigidbody2D _rigidbody;
    private float _horizontalInput;
    private bool _isFacingRight = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jumpForce);
        }

        ApplyVariableJumpHeight();
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(_horizontalInput * _moveSpeed, _rigidbody.linearVelocity.y);
        UpdateFacingDirection();
    }

    /// <summary>
    /// Уменьшает высоту прыжка, если игрок отпустил кнопку до достижения пика.
    /// </summary>
    private void ApplyVariableJumpHeight()
    {
        if (Input.GetButtonUp("Jump") && _rigidbody.linearVelocity.y > 0f)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _rigidbody.linearVelocity.y * 0.5f);
        }
    }

    /// <summary>
    /// Проверяет, касается ли игрок земли.
    /// </summary>
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
    }

    /// <summary>
    /// Разворачивает спрайт в сторону движения.
    /// </summary>
    private void UpdateFacingDirection()
    {
        if (_horizontalInput > 0f && !_isFacingRight)
        {
            Flip();
        }
        else if (_horizontalInput < 0f && _isFacingRight)
        {
            Flip();
        }
    }

    /// <summary>
    /// Зеркально отражает спрайт по оси X.
    /// </summary>
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        if (_groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
        }
    }
}