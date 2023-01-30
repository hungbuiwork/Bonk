using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CursorMovement : MonoBehaviour
{
    [SerializeField]
    private float cursorSpeed;
    [SerializeField]
    private Collider2D bounds;

    private void Awake()
    {
    }
    private void Update()
    {
        float verticalMovement = Input.GetAxis("Vertical");
        float horizontalMovement = Input.GetAxis("Horizontal");
        Move(horizontalMovement, verticalMovement);

    }
    private void Move(float x, float y)
    {
        Vector3 newPosition = this.transform.position + new Vector3(x, y, 0) * cursorSpeed * Time.deltaTime;
        if (CheckBounds(newPosition))
        {
            this.transform.position += new Vector3(x, y, 0) * cursorSpeed * Time.deltaTime;
        }
    }

    private bool CheckBounds(Vector3 position)
    {
        if(bounds == null) { return true; }
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f);
        if (!colliders.Contains(bounds))
        {
            return false;
        }
        return true;
    }

    

}
