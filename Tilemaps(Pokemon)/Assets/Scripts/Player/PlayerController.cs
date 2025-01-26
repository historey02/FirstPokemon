using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public bool isMoving;
    private Vector2 input;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Only checks for input if the player is not already moving
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //prevents diagoncal movement
            if(input.x != 0) { input.y = 0; }

            //Only sets the target position and calls the Move() if the input doesn't equal 0
            if (input != Vector2.zero)
            {
                var targetPosition = transform.position;
                targetPosition.x += input.x;
                targetPosition.y += input.y;
                StartCoroutine(Move(targetPosition));
            }
        }
        
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        //while the player is approaching but has not yet reached the target position
        //Mathf.Epsilon is the smallest float after 0
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
            yield return null;
        }
        transform.position = targetPos;
        isMoving=false;
    }
}
