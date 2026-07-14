using UnityEngine;

/// <summary>
/// Управляет визуализацией и движением языка игрока.
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class PlayerTongue : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _radius = 5f;
    [SerializeField] private float _thickness = 0.2f;
    [SerializeField] private float _attackDelay = 0.3f;

    private LineRenderer _lineRenderer;
    private Vector2 _direction;
    private float _currentLength;
    private float _cooldownTimer;
    private bool _isExtending;
    private bool _isRetracting;

    /// <summary>
    /// Скорость выдвижения и возврата языка.
    /// </summary>
    public float Speed
    {
        get => _speed;
        set => _speed = Mathf.Max(0.1f, value);
    }

    /// <summary>
    /// Максимальная длина языка (радиус поражения).
    /// </summary>
    public float Radius
    {
        get => _radius;
        set => _radius = Mathf.Max(0.1f, value);
    }

    /// <summary>
    /// Толщина языка.
    /// </summary>
    public float Thickness
    {
        get => _thickness;
        set => _thickness = Mathf.Max(0.01f, value);
    }

    /// <summary>
    /// Задержка между атаками.
    /// </summary>
    public float AttackDelay
    {
        get => _attackDelay;
        set => _attackDelay = Mathf.Max(0f, value);
    }

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        HideTongue();
    }

    private void Update()
    {
        UpdateCooldown();
        UpdateTongueMovement();
        UpdateLineRenderer();
    }

    /// <summary>
    /// Запускает атаку языком в указанном направлении.
    /// </summary>
    public void Attack(Vector2 direction)
    {
        if (_cooldownTimer > 0f || _isExtending || _isRetracting)
            return;

        _direction = direction.normalized;
        _currentLength = 0f;
        _isExtending = true;
        _lineRenderer.enabled = true;
    }

    /// <summary>
    /// Уменьшает таймер кулдауна.
    /// </summary>
    private void UpdateCooldown()
    {
        if (_cooldownTimer > 0f)
        {
            _cooldownTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Обновляет движение языка (выдвижение или возврат).
    /// </summary>
    private void UpdateTongueMovement()
    {
        if (_isExtending)
        {
            _currentLength += _speed * Time.deltaTime;
            if (_currentLength >= _radius)
            {
                _currentLength = _radius;
                _isExtending = false;
                _isRetracting = true;
            }
        }
        else if (_isRetracting)
        {
            _currentLength -= _speed * Time.deltaTime;
            if (_currentLength <= 0f)
            {
                _currentLength = 0f;
                _isRetracting = false;
                _cooldownTimer = _attackDelay;
                HideTongue();
            }
        }
    }

    /// <summary>
    /// Обновляет позиции LineRenderer для отрисовки языка.
    /// </summary>
    private void UpdateLineRenderer()
    {
        if (!_lineRenderer.enabled)
            return;

        Vector2 startPoint = transform.position;
        Vector2 endPoint = startPoint + _direction * _currentLength;

        _lineRenderer.SetPosition(0, startPoint);
        _lineRenderer.SetPosition(1, endPoint);
        _lineRenderer.startWidth = _thickness;
        _lineRenderer.endWidth = _thickness;
    }

    /// <summary>
    /// Скрывает язык.
    /// </summary>
    private void HideTongue()
    {
        _lineRenderer.enabled = false;
    }
}