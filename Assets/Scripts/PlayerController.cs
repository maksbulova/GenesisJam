using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerTurnSpeed, playerForwardSpeed;
    public float rightMovementBound, leftMovementBound;
    private Camera followingCamera;
    public AnimationCurve accelerationCurve;
    [Tooltip("Seconds for speed change")]
    public float acceleration;

    private void Awake()
    {
        followingCamera = Camera.main;
    }

    private void Start()
    {
        StartCoroutine(Movement());
    }

    void TouchHandler(out Vector3 turnDirection)
    {
        Vector2 touchPosition = Input.GetTouch(0).position;
        Vector2 normilizedTouchPosition = normilizePosition(touchPosition);

        if (normilizedTouchPosition.x >= 0.5f)
        {
            turnDirection = Vector3.right;
        }
        else
        {
            turnDirection = Vector3.left;
        }
    }

    Vector2 normilizePosition(Vector2 screenPos)
    {
        float normilizedX = screenPos.x / Screen.width;
        float normilizedY = screenPos.y / Screen.height;

        return new Vector2(normilizedX, normilizedY);
    }


    IEnumerator Movement()
    {
        // TODO pause
        while (Application.isPlaying)
        {
            Vector3 turnMovement;
            if (Input.touchCount > 0)
            {
                TouchHandler(out Vector3 turnDirection);

                turnMovement = turnDirection * playerTurnSpeed * Time.deltaTime;
                float newXPosition = transform.position.x + turnMovement.x;

                if (newXPosition < leftMovementBound || newXPosition > rightMovementBound)
                {
                    turnMovement = Vector3.zero;
                }
            }
            else
            {
                turnMovement = Vector3.zero;
            }

            Vector3 forwardMovment = Vector3.forward * playerForwardSpeed * Time.deltaTime;
            Vector3 resultMovement = forwardMovment + turnMovement;
            transform.Translate(resultMovement, Space.World);

            FollowCamera(forwardMovment);

            yield return null;
        }
    }

    void FollowCamera(Vector3 followedMovement)
    {
        Vector3 cameraMovement = followedMovement;
        cameraMovement.x = 0;
        followingCamera.transform.Translate(cameraMovement, Space.World);
    }

    public IEnumerator ChangeSpeed(float changeAmount)
    {
        float oldSpeed = playerForwardSpeed;
        float newSpeed = playerForwardSpeed + changeAmount;

        float timer = 0;
        while (timer < acceleration)
        {
            float t = accelerationCurve.Evaluate(timer) / acceleration;
            playerForwardSpeed = Mathf.Lerp(oldSpeed, newSpeed, t);

            timer += Time.deltaTime;
            yield return null;
        }
    }
}
