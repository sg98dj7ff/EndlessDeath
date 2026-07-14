using UnityEngine;

/// <summary>
/// Обрабатывает ввод для атаки языком.
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerTongue _tongue;
    [SerializeField] private Camera _camera;

    private void Awake()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryAttack();
        }
    }

    /// <summary>
    /// Пытается выполнить атаку языком в направлении точки клика.
    /// </summary>
    private void TryAttack()
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector2 direction = ((Vector2)mousePosition - (Vector2)transform.position).normalized;
        _tongue.Attack(direction);
    }
}