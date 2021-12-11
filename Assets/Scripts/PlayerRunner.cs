using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunner : MonoBehaviour
{
    private enum PlayerState
    {
        normal,
        immortal
    }

    PlayerState playerState;
    private PlayerController playerController;

    [Tooltip("Seconds of undamageble after hit")]
    public float damageDuration;
    public float slowdownStrenght;
    public float damageAnimationSpeed;


    private void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        playerState = PlayerState.normal;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") || playerState == PlayerState.normal)
        {
            StartCoroutine(HitObstacle());
        }
    }

    IEnumerator HitObstacle()
    {
        playerState = PlayerState.immortal;
        playerController.StartCoroutine(playerController.ChangeSpeed(-slowdownStrenght));

        float timer = 0;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        while (timer < damageDuration)
        {
            float t = Mathf.Abs(Mathf.Sin(timer * damageAnimationSpeed));
            Color color = Color.Lerp(Color.white, Color.red, t);
            spriteRenderer.color = color;
            timer += Time.deltaTime;

            yield return null;
        }

        spriteRenderer.color = Color.white;
        playerState = PlayerState.normal;
        playerController.StartCoroutine(playerController.ChangeSpeed(slowdownStrenght));
    }

}
