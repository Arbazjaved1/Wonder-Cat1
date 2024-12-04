using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Room camera
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    // Follow player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    // Vertical camera follow
    [SerializeField] private float verticalFollowSpeed = 2f; // Speed of vertical follow
    [SerializeField] private float jumpThreshold = 0.1f; // Threshold to detect jumping
    [SerializeField] private float verticalSmoothTime = 0.2f; // Smooth transition time for vertical movement
    private float targetVerticalPosition;

    private void Update()
    {
        // Detect if player is jumping (based on vertical velocity or threshold)
        if (Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.y) > jumpThreshold)
        {
            // Smoothly follow player while jumping
            targetVerticalPosition = player.position.y;
        }
        else
        {
            // Lock camera to player's grounded position
            targetVerticalPosition = Mathf.Lerp(targetVerticalPosition, player.position.y, Time.deltaTime * verticalFollowSpeed);
        }

        // Update camera position
        transform.position = new Vector3(
            player.position.x + lookAhead,
            Mathf.Lerp(transform.position.y, targetVerticalPosition, verticalSmoothTime),
            transform.position.z
        );

        // Smooth horizontal look ahead
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}
