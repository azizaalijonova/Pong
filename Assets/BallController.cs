// using System.Collections;
// using System.Collections.Generic;
// using System.Runtime.CompilerServices;
// using UnityEngine;
// using UnityEngine.UI;

// public class BallController : MonoBehaviour
// {
//     public float initialSpeed = 10f;

//     public float speedIncrease = 0.2f;

//     public Text playerText;
    
//     public Text opponentText;

//     private int hitCounter;

//     private Rigidbody2D rb;
//     // Start is called before the first frame update
//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         Invoke("StartBall", 2f);
//     }

//     private void FixedUpdate(){
//         rb.velocity = Vector2.ClampMagnitude(rb.velocity, initialSpeed + (speedIncrease*hitCounter));

//     }

//     private void StartBall(){
//         rb.velocity = new Vector2(-1, 0) * (initialSpeed + speedIncrease * hitCounter);
//     }

//     private void RestrartBall(){
//          rb.velocity = new Vector2(0, 0);
//          transform.position = new Vector2(0, 0);
//          hitCounter = 0;
//          Invoke("StartBall", 2f);
//     }

//     private void PlayerBounce(Transform obj){
//         hitCounter++;

//         Vector2 ballPosition = transform.position;
//         Vector2 playerPosition = obj.position;

//         float xDirection;
//         float yDirection;

//         if(transform.position.x >0){
//             xDirection = -1;
//         } else{
//             xDirection = 1;
//         }

//         yDirection = (ballPosition.y - playerPosition.y)/obj.GetComponent<Collider2D>().bounds.size.y;

//         if(yDirection == 0){
//             yDirection = 0.25f;
//         }

//         rb.velocity = new Vector2(xDirection, yDirection) * (initialSpeed + (speedIncrease*hitCounter));


//     }

//     private void OnCollisionEnter2D(Collision2D other){
//         if(other.gameObject.name == "PaddleA" || other.gameObject.name == "PaddleB"){
//             PlayerBounce(other.transform);
//         }
//     }
//     private void OnTriggerEnter2D(Collider2D other){
//         if(transform.position.x >0) {
//             RestrartBall();

//             opponentText.text = (int.Parse(opponentText.text)+1).ToString();
//         } else if( transform.position.x <0){
//             RestrartBall();
//             playerText.text = (int.Parse(playerText.text)+1).ToString();
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 10f;
    [SerializeField] private float speedIncrease = 0.25f;
    [SerializeField] private Text AScore;
    [SerializeField] private Text BScore;

    private int hitCounter;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall", 2f);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, initialSpeed + (speedIncrease * hitCounter));
    }

    private void StartBall()
    {
        rb.velocity = new Vector2(-1, 0) * (initialSpeed + speedIncrease * hitCounter);
    }

    private void ResetBall()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        hitCounter = 0;
        Invoke("StartBall", 2f);
    }

    private void PlayerBounce(Transform myObject)
    {
        hitCounter++;
        Debug.Log("Hit Counter: " + hitCounter);

        Vector2 ballPos = transform.position;
        Vector2 playerPos = myObject.position;

        float xDirection = (transform.position.x > 0) ? -1 : 1;
        float yDirection = (ballPos.y - playerPos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;
        
        if (yDirection == 0)
        {
            yDirection = 0.25f;
        }

        rb.velocity = new Vector2(xDirection, yDirection) * (initialSpeed + (speedIncrease * hitCounter));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.name);
        if (collision.gameObject.name == "PaddleA" || collision.gameObject.name == "PaddleB")
        {
            PlayerBounce(collision.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > 0)
        {
            ResetBall();
            AScore.text = (int.Parse(AScore.text) + 1).ToString();
        }
        else if (transform.position.x < 0)
        {
            ResetBall();
            BScore.text = (int.Parse(BScore.text) + 1).ToString();
        }
    }
}