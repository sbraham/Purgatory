using UnityEngine;


public class EnemyChaser : MonoBehaviour
{
    public float speed = 5; //variable making speed of the enemy
    public int Value = 1; //variable dictating the value of killing this
    [Space]
    private Transform target; //set a variable to be later defined as the location of the player
    [Space]
    private Vector3 faceTarget;
    private float offset = -90;
    [Space]
    GameObject player; //code to link with the PlayerController code so I can edite variables
    PlayerController PlayerController;
    [Space]
    private float difficulty = Butten_Controler.difficulty;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //defines the game point target as a sprite with the tag “player”’s location (the player)

        player = GameObject.FindGameObjectWithTag("Player"); //code to link with the PlayerController
        PlayerController = player.GetComponent<PlayerController>();

    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime * (difficulty * 1)); //every update the enemy moves towards the target location (the player)

        faceTarget = GameObject.FindGameObjectWithTag("Player").transform.position; //Vector3 position of player
        Vector3 difference = faceTarget - transform.position; //difference between player's location and shooters location
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; //calculate angular difference difference player and shooter

        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset); //turn by the angle to face player
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject); //destroy this object when it hits the player
        }
        if (collision.CompareTag("Projectile"))
        {
            Destroy(gameObject); //destroy this object when hit by projectile
            PlayerController.score = PlayerController.score + Value; //and increase the score by its value
            Debug.Log("Score = " + PlayerController.score);
            PlayerController.UpdateScore(); //update the score after a collision between enemy and player projectile
        }
        if (collision.CompareTag("EnemyProjectile"))
        {
            Destroy(gameObject); //destroy withough increasing score if hit with enemy projectile
        }
    }
}
