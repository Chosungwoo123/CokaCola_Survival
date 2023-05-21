using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
        {
            return;
        }

        Vector3 playerPos = GameManager.Instance.curPlayer.transform.position;
        Vector3 myPos = transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        float dirX = GameManager.Instance.playerInputX < 0 ? -1 : 1;
        float dirY = GameManager.Instance.playerInputY < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 60);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 60);
                }
                break;
        }
    }
}
