using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameControllerScript gameController;
    public GameObject parent;
    public Rigidbody rb;

    public float timer = 0f;
    float limit = 1f;
    
    bool canMove = true;
    bool canJump = true;
    
    Vector3[] postions = new Vector3[3];
    int currentPos = 1;
    int prevPos;

    int score = 0;
    // Start is called before the first frame update
    void Start()
    {

        postions[0] = new Vector3(-2.5f, transform.position.y, transform.position.z);
        postions[1] = new Vector3(0f, transform.position.y, transform.position.z);
        postions[2] = new Vector3(2.5f, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        LerpingPlayer(prevPos, currentPos);

        MovePlayer();

        //Jump();

        transform.Rotate(new Vector3(10, transform.rotation.y, transform.rotation.z));

        if (transform.position.y < 0.4f)
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, 0.4f, transform.position.z);
            canJump = true;
        }

        Debug.Log(rb.velocity.y);
    }

    void MovePlayer()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentPos > 0 && canMove)
            {
                prevPos = currentPos;
                currentPos--;
                canMove = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentPos < 2 && canMove)
            {
                prevPos = currentPos;
                currentPos++;
                canMove = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
            canJump = false;
            
        }
    }

    void LerpingPlayer(int prev, int to)
    {
        if (!canMove)
        {
            timer += Time.deltaTime * 2f;

            var direction = prev - to;

            if(direction < 0)
            {
                parent.transform.Rotate(new Vector3(0, 1, 0));
            } else 
            {
                parent.transform.Rotate(new Vector3(0, -1, 0));
            }

            Vector3 xLerp = Vector3.Lerp(postions[prev], postions[to], timer);
            transform.position = new Vector3(xLerp.x, transform.position.y, transform.position.z);

            if (timer >= limit)
            {
                transform.rotation = new Quaternion(transform.rotation.x, 0, 0, 1);
                timer = 0;
                canMove = true;
            }

        }
    }

    void Jump()
    {
        if (canJump)
        {
            rb.AddForce(new Vector3(0f, 300f, 0f));
            rb.useGravity = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "pickUp")
        {
            score += 10;
            gameController.totalScore.text = score.ToString();
            other.gameObject.SetActive(false);
        }
    }
}
