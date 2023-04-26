using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player; 

    private Vector2 startPos;
    private Vector2 endPos;


    private void Start()
    {
        player = GetComponent<Player>();    
    }

    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if(touch.phase == TouchPhase.Began)
        {
            startPos = touch.position;
        }

        if(touch.phase == TouchPhase.Ended)
        {
            endPos = touch.position;

            Vector2 delta = endPos - startPos;

            if(Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                if (delta.x > 0)
                {
                    Debug.Log("qua phai");
                    player.Move(new Vector2(1, 0));
                }
                else
                {
                    Debug.Log("qua trai");
                    player.Move(new Vector2(-1, 0));
                }
            }
            else
            {
                if (delta.y > 0)
                {
                    Debug.Log("len tren");
                    player.Move(new Vector2(0, 1));
                }
                else
                {
                    Debug.Log("xuong duoi");
                    player.Move(new Vector2(0, -1));
                }
            }

            startPos = Vector2.zero;
            endPos = Vector2.zero;
        }

        
    }
}
