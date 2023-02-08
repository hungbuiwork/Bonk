using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CursorMovement : MonoBehaviour
{
    /// <summary>
    /// Moves the cursor, keeping it within a set of bounds. This cursor is where the player will spawn troops/ buildings.
    /// </summary>
    [SerializeField]
    private float cursorSpeed;
    [SerializeField]
    private Collider2D bounds;

    private void Awake()
    {
    }
    private void Update()
    {
        //Input. Change in future
        float verticalMovement = Input.GetAxis("Vertical");
        float horizontalMovement = Input.GetAxis("Horizontal");
        Move(horizontalMovement, verticalMovement);

    }
    private void Move(float x, float y)
    {
        //Moves the cursor
        Vector3 newPosition = this.transform.position + new Vector3(x, y, 0) * cursorSpeed * Time.deltaTime;
        //Only move the cursor to the new position if it is within bounds
        if (CheckBounds(newPosition))
        {
            this.transform.position += new Vector3(x, y, 0) * cursorSpeed * Time.deltaTime;
        }
    }

    private bool CheckBounds(Vector3 position)
    {
        //Checks that the cursor is within the correct bounds
        if(bounds == null) { return true; }
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f);
        if (!colliders.Contains(bounds))
        {
            return false;
        }
        return true;
    }

    

}
