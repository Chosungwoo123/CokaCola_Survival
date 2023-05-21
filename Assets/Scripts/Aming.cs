using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aming : MonoBehaviour
{
    public float angle;
    Vector2 target, mouse;

    [SerializeField] SpriteRenderer weaponSprite;
    
    private void Update()
    {
        target = transform.position;
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
        if(Mathf.Abs(angle) < 90)
        {
            weaponSprite.flipY = true;
        }
        else
        {
            weaponSprite.flipY = false;
        }
        this.transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
    }
}
