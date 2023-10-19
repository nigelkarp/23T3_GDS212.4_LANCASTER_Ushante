using UnityEngine;
// reference chatgpt for this script
public class FishBehavior : MonoBehaviour
{
    public float swimSpeed = 2.0f;
    public float rotationSpeed = 90.0f;

    public float minX = -5.0f; // Define the minimum X-coordinate for the boundary
    public float maxX = 5.0f;  // Define the maximum X-coordinate for the boundary
    public float minY = -5.0f; // Define the minimum Y-coordinate for the boundary
    public float maxY = 5.0f;  // Define the maximum Y-coordinate for the boundary

    private Vector2 targetPosition;

    void Start()
    {
        targetPosition = GetRandomTargetPosition();
    }

    void Update()
    {
        SwimTowardsTarget();
        RotateTowardsTarget();
        CheckBoundary();
    }

    void SwimTowardsTarget()
    {
        // Move the fish toward the target position
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, swimSpeed * Time.deltaTime);

        if (Vector2.Distance((Vector2)transform.position, targetPosition) < 0.1f)
        {
            targetPosition = GetRandomTargetPosition();
        }
    }

    void RotateTowardsTarget()
    {
        Vector2 targetDirection = (targetPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void CheckBoundary()
    {
        // Ensure the fish stays within the defined boundary
        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY)
        );
        transform.position = clampedPosition;
    }

    Vector2 GetRandomTargetPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector2(randomX, randomY);
    }
}
