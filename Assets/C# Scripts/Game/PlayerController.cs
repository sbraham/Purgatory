using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 10; //creates and defines the variable speed. public therefore can change in IDE
    [Space]
    private Rigidbody2D rb; //Creates a variable rb of type 2D Rigid Body
    private Vector2 moveVelocity; //Creates a variable moveVelocity of type 2D Vector
    [Space]
    private Vector3 faceTarget;
    private float offset = -90;
    public GameObject projectile;
    public Transform shootPoint;
    [Space]
    private double Lives = 5;
    private double timeAlive = 0;
    public double score = 0;
    [Space]
    public Image Lives1; //set to the red dots - the life points
    public Image Lives2; //Lives1 set to the first life point, Lives5 set to the last life point
    public Image Lives3;
    public Image Lives4;
    public Image Lives5;
    [Space]
    private int rounds;
    public float timeToLoad;
    private float tempTime;
    private bool reloading;
    [Space]
    public Slider ReloadBar;
    public Image Ammo1; //set to the Black dots - the rounds in magazine
    public Image Ammo2; //Ammo1 set to the first round, Lives5 set to the last round
    public Image Ammo3;
    public Image Ammo4;
    public Image Ammo5;
    [Space]
    public GameObject spawner1;
    public GameObject spawner2;
    public GameObject spawner3;
    public GameObject spawner4;
    [Space]
    public GameObject gameUI;
    public GameObject EndGameUI;
    public GameObject ScoreInputUI;
    [Space]
    public TextMeshProUGUI DesplayScore;
    public TextMeshProUGUI DesplayTime;
    public TextMeshProUGUI DesplayFinalScore;
    [Space]
    private float difficulty = Butten_Controler.difficulty;

    public void EndGame() //runs when player dies
    {
        score = score * timeAlive; //score is multiplied by the time alive
        score = Math.Round(score); //time ilive is rounded to nearest whole number

        spawner1.SetActive(false); //spawners are diabled
        spawner2.SetActive(false);
        spawner3.SetActive(false);
        spawner4.SetActive(false);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); //any enemies on screan are destroyed
        foreach (GameObject enemy in enemies)
            GameObject.Destroy(enemy);

        EndGameUI.SetActive(true); //UI changes

        DesplayFinalScore.text = "Your Score: " + score; //the desplay obejct desplayer final score
    }

    public void UpdateScore()
    {
        DesplayScore.text = "Score: " + score; //set the text to “Score: whatever the score is” 
    }

    public void UpdateTime()
    {   
        DesplayTime.text = "Time: " + timeAlive; //set the text of time desplay object to “Time: whatever the time is” 
    }

    public void UpdateLife() //as  lives decrease
    {
        if (Lives <= 4) //objects representing lives are hidden
        {
            Lives1.enabled = false; //hide the life point
        }
        if (Lives <= 3)
        {
            Lives2.enabled = false;
        }
        if (Lives <= 2)
        {
            Lives3.enabled = false;
        }
        if (Lives <= 1)
        {
            Lives4.enabled = false;
        }
        if (Lives <= 0) //when no lives left
        {
            Lives5.enabled = false; //hide the life point
            gameObject.SetActive(false); //player disabled and 
            EndGame(); //The game ends
        }
    }

    public void UpdateAmmo() //as  lives decrease
    {
        if (rounds == 5) //objects representing lives are hidden
        {
            Ammo1.enabled = true;
            Ammo2.enabled = true;
            Ammo3.enabled = true;
            Ammo4.enabled = true;
            Ammo5.enabled = true;
        }
        if (rounds == 4)
        {
            Ammo1.enabled = false;
            Ammo2.enabled = true;
            Ammo3.enabled = true;
            Ammo4.enabled = true;
            Ammo5.enabled = true;
        }
        if (rounds == 3)
        {
            Ammo1.enabled = false;
            Ammo2.enabled = false;
            Ammo3.enabled = true;
            Ammo4.enabled = true;
            Ammo5.enabled = true;
        }
        if (rounds == 2)
        {
            Ammo1.enabled = false;
            Ammo2.enabled = false;
            Ammo3.enabled = false;
            Ammo4.enabled = true;
            Ammo5.enabled = true;
        }
        if (rounds == 1) //when no lives left
        {
            Ammo1.enabled = false;
            Ammo2.enabled = false;
            Ammo3.enabled = false;
            Ammo4.enabled = false;
            Ammo5.enabled = true;
        }
        if (rounds == 0)
        {
            Ammo1.enabled = false;
            Ammo2.enabled = false;
            Ammo3.enabled = false;
            Ammo4.enabled = false;
            Ammo5.enabled = false;
        }
    }

    private void Awake() //Runs at start of program
    {
        gameUI.SetActive(true);
        EndGameUI.SetActive(false);
        ScoreInputUI.SetActive(false);

        rb = GetComponent<Rigidbody2D>(); //define rb to be this components rigid body
        UpdateScore();

        Lives1.enabled = true;
        Lives2.enabled = true;
        Lives3.enabled = true;
        Lives4.enabled = true;
        Lives5.enabled = true;

        Ammo1.enabled = true;
        Ammo2.enabled = true;
        Ammo3.enabled = true;
        Ammo4.enabled = true;
        Ammo5.enabled = true;

        timeAlive = 0;
        rounds = 5;
        reloading = false;
    }

    private void Update() //runs every frame
    {
        timeAlive = timeAlive + Time.deltaTime; //increases the time alive by the frame rate
        UpdateTime();

        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); //define moveInput to be equal to any input of vertical or horizontal direction (arrow keys or WASD) - only happens when the appropriate key pressed
        moveVelocity = moveInput.normalized * speed; //normalizes the vector making the value 1 (just direction) then times it by the speed

        rb.MovePosition(rb.position + moveVelocity * difficulty * Time.fixedDeltaTime); //moves the sprite to its position plus the vector. it's multiplied by Time.fixedDeltaTime so the distance is the same on all processing speeds

        faceTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition); //This Vector3 definition is added at the start - face position is the mouse position
        Vector3 difference = faceTarget - transform.position; //new Vector3 is difference between
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; //new float is how much to rotate

        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset); //rotation code, 0 in x and y, rotZ in Z

        if (Input.GetKeyDown("r")) //if R key pressed
        {
            if (!reloading)
            {
                tempTime = timeToLoad; //reset timer
                reloading = true; //start reloading
                UpdateAmmo(); //update the ammo in case of change
            }
        }

        if (Input.GetMouseButtonDown(0)) //if left mouse click
        {
            if (rounds > 1 && !reloading) //and there are more than 1 round in the magazine and you are not reloading
            {
                Instantiate(projectile, shootPoint.position, transform.rotation); //instantiate projectile
                rounds--; //reduce rounds by one
                UpdateAmmo(); //and update the ammo display
            }
            else if (rounds == 1 && !reloading) //and there is 1 round in the magazine and you are not alreading reloading
            {
                Instantiate(projectile, shootPoint.position, transform.rotation); //instantiate last projectile
                rounds--; //reduce the rounds by one
                UpdateAmmo(); //update the display
                tempTime = timeToLoad; //reset the timer
                reloading = true; //and start reloading
            }
            else { } //nothing happens  if you are reloading
        }

        if (reloading) //if you are reloading
        {
            if (tempTime <= 0) //if reloading timer = 0
            {
                rounds = 5; //refill rounds in magazine
                reloading = false; //stop reloading
                UpdateAmmo(); //update the display
                ReloadBar.value = 0; //and set reloading bar to 0
            }
            else //if time doesn’t = 0
            {
                tempTime -= Time.deltaTime * (difficulty); //timer ticks down
                ReloadBar.value = tempTime; //and change the value on the loading bar - visual display to how long reloading takes
            }
        }

        if (Input.GetKeyDown("p"))
        {
            Thread.Sleep(5000);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //if collision reduce the health by 1
    {
        if (collision.CompareTag("Enemy")) //multiple tags doing same thing here will do different things later
        {
            Lives = Lives - 1f; //reduces lives by one
            Debug.Log("Lives = " + Lives);
            UpdateLife(); //Life is updated after a collision as that is when life changes
        }
        if (collision.CompareTag("Projectile"))
        {
            Lives = Lives - 0.5f; //reduces lives by half due to double collisions
            Debug.Log("Lives = " + Lives);
            UpdateLife();
        }
        if (collision.CompareTag("EnemyProjectile"))
        {
            Lives = Lives - 0.5f; //reduces lives by half due to double collisions
            Debug.Log("Lives = " + Lives);
            UpdateLife();
        }
    }
}

