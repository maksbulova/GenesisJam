using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float rightMoveBound, leftMoveBound;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            StartCoroutine(TouchHandler());
        }
    }

    IEnumerator TouchHandler()
    {
        while (Input.touchCount > 0)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            Vector2 normilizedTouchPosition = normilizePosition(touchPosition);

            if (normilizedTouchPosition.x >= 0.5f)
            {
                Move(Vector2.right);
            }
            else
            {
                Move(Vector2.left);
            }

            yield return null;
        }
    }

    Vector2 normilizePosition(Vector2 screenPos)
    {
        float normilizedX = screenPos.x / Screen.width;
        float normilizedY = screenPos.y / Screen.height;

        return new Vector2(normilizedX, normilizedY);
    }


    private void Move(Vector3 direction)
    {
        Vector3 moveDirection = direction * playerSpeed * Time.deltaTime;
        if (movementBounds.Contains(transform.position + moveDirection))
        {
            transform.Translate(moveDirection);
        }
    }

}
