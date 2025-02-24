using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Lane Settings")]
    public float[] laneYPositions = new float[3] { -0.5f, -2.0f, -3.5f  };

    // 0 = top, 1 = middle, 2 = bottom
    public int currentLaneIndex = 1;

    [Header("Lane Switching")]
    public float laneSwitchSpeed = 20f; // how fast we slide to the new lane

    [Header("Jump Settings")]
    public float jumpHeight = 2f;       // how high the "fake jump" goes
    public float jumpDuration = 1f;   // time for the entire jump (up + down)

    private bool isJumping = false;     // are we mid‐jump?
    private Vector3 targetPosition;     // where we want to slide the player

    void Start()
    {
        // Initialize our targetPosition to the correct lane
        targetPosition = transform.position;
        targetPosition.y = laneYPositions[currentLaneIndex];
        transform.position = targetPosition;
    }

    void Update()
    {
        // Only allow lane switching if we’re not jumping
        if (!isJumping)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentLaneIndex = Mathf.Max(currentLaneIndex - 1, 0);
                SetLaneTarget();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentLaneIndex = Mathf.Min(currentLaneIndex + 1, laneYPositions.Length - 1);
                SetLaneTarget();
            }
        }

        // Jump (only if not already jumping)
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            StartCoroutine(Jump());
        }

        // Smoothly move toward the lane’s target position
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            laneSwitchSpeed * Time.deltaTime
        );
    }

    // Updates the y of targetPosition so we slide to the new lane over time
    private void SetLaneTarget()
    {
        Vector3 pos = targetPosition;
        pos.y = laneYPositions[currentLaneIndex];
        targetPosition = pos;
    }


    // Moves us up from the lane’s baseline by jumpHeight then brings back down
    private IEnumerator Jump()
    {
        isJumping = true;

        float startTime = 0f;
        float halfDuration = jumpDuration / 2f;

        // Which lane are we on when we start jumping?
        float laneBaseY = laneYPositions[currentLaneIndex];

        while (startTime < jumpDuration)
        {
            startTime += Time.deltaTime;

            // For 1st half, move up; for 2nd half, move down
            if (startTime <= halfDuration)
            {
                // Up phase
                float t = startTime / halfDuration;  // goes from 0 to 1
                float newY = Mathf.Lerp(laneBaseY, laneBaseY + jumpHeight, t);
                SetVerticalPosition(newY);
            }
            else
            {
                // Down phase
                float t = (startTime - halfDuration) / halfDuration; // 0 to 1 again
                float newY = Mathf.Lerp(laneBaseY + jumpHeight, laneBaseY, t);
                SetVerticalPosition(newY);
            }

            yield return null;
        }

        // Ensure final position is exactly the lane’s baseline
        SetVerticalPosition(laneBaseY);

        isJumping = false;
    }

    private void SetVerticalPosition(float newY)
    {
        Vector3 currentPos = transform.position;
        currentPos.y = newY;
        transform.position = currentPos;
    }
}
