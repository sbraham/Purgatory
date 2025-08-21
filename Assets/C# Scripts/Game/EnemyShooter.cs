using UnityEngine;


public class EnemyShooter : MonoBehaviour
{
    public float speed = 5; //variable making speed of the enemy
    public int Value = 5; //variable dictating the value of killing this
    public float stoppingDistance = 6.5f; //the f signifies it is a float not an integer
    public float retreatDistance = 5; //the difference between the stopping and retreating distance is the place where the shooter can sit, not to 
                                      //close not too far away.The shooter doesn't have to be here to shoot or land a hit, it can shoot anywhere  
                                      //and projectiles should travel indefinitely before collision with wall
    [Space]
    private float timeBtwShots;
    private float startTimeBtwShots = 3;
    [Space]
    public GameObject Projectile;
    public Transform shotPoint;
    private Transform moveTarget;
    private Vector3 shootTarget;
    [Space]
    GameObject player; //code to link with the PlayerController code so I can edite variables
    PlayerController PlayerController;
    [Space]
    private float difficulty = Butten_Controler.difficulty;

    private float offset = -90;

    void Awake()
    {
        moveTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //Players location

        player = GameObject.FindGameObjectWithTag("Player"); //code to link with the PlayerController
        PlayerController = player.GetComponent<PlayerController>();

        timeBtwShots = startTimeBtwShots;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, moveTarget.position) > stoppingDistance) //if too far away
        {
            transform.position = Vector2.MoveTowards(transform.position, moveTarget.position, speed * Time.deltaTime * (difficulty * 1)); //move closer
        }
        else if (Vector2.Distance(transform.position, moveTarget.position) < stoppingDistance && Vector2.Distance(transform.position, moveTarget.position) > retreatDistance) //if close enough and far away enough
        {
            transform.position = this.transform.position; //stay still
        }
        else if (Vector2.Distance(transform.position, moveTarget.position) < retreatDistance) //if too close
        {
            transform.position = Vector2.MoveTowards(transform.position, moveTarget.position, -speed * Time.deltaTime * (difficulty * 1)); //move away - note -speed
        }

        if (timeBtwShots <= 0) //when timer = 0 
        {
            Instantiate(Projectile, shotPoint.position, transform.rotation); //shoot
            timeBtwShots = startTimeBtwShots; //then reset timer
        }
        else
        {
            timeBtwShots -= Time.deltaTime; //timer ticks down 
        }

        shootTarget = GameObject.FindGameObjectWithTag("Player").transform.position; //Vector3 position of player
        Vector3 difference = shootTarget - transform.position; //difference between player's location and shooters location
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
            Destroy(gameObject); //destroy this object when hhit by projectile
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
