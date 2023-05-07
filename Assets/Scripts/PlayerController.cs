using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private Player player;

    private Vector2 startPos;
    private Vector2 endPos;

    private UnityAction onTrigger;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            startPos = touch.position;
        }

        if (touch.phase == TouchPhase.Stationary)
        {
            if (Vector2.Distance(startPos, touch.position) < 5f) return;

            endPos = touch.position;
            Vector2 delta = endPos - startPos;
            CalculateMoveDirection(delta);

            startPos = touch.position;
        }

        if (touch.phase == TouchPhase.Ended)
        {
            endPos = touch.position;

            Vector2 delta = endPos - startPos;
            CalculateMoveDirection(delta);

            startPos = Vector2.zero;
            endPos = Vector2.zero;
        }

    }

    private void CalculateMoveDirection(Vector2 delta)
    {
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 0)
            {
                player.Move(new Vector2(1, 0));
            }
            else
            {
                player.Move(new Vector2(-1, 0));
            }
        }
        else
        {
            if (delta.y > 0)
            {
                player.Move(new Vector2(0, 1));
            }
            else
            {
                player.Move(new Vector2(0, -1));
            }
        }
    }
}
