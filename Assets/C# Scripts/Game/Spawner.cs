using UnityEngine;


public class Spawner : MonoBehaviour
{
    private float difficulty = Butten_Controler.difficulty;
    [Space]
    public GameObject enemy1; //Game objects will be given the prefabricated objects of the enemies.
    public GameObject enemy2;  //enemy1 = chaser, enemy2 = shooter, enemy3 = hunter
    public GameObject enemy3;
    [Space]
    private int count = 0; //count the number of time enemies have spawned to allow harder enemies to spawn later in game when count is high
    private float ShooterStart = 30; //number of spawns shooters will start
    private float HunterStart = 50; //number of spawns hunters will start
    [Space]
    private int randNum; //placeholder for a randomly generated number
    [Space]
    public float timeBtwSpawn = 0; //time until next spawn - counts down through the program
    public float startTimeBtwSpawn = 5; //the number timeBtwSpawns is set to after a spawn
    public float timeChange = 0.05f; //the amount startTimeBtwSpawn changes every spawn (decrease)

    private void Awake()
    {
        ShooterStart = 14 - (difficulty * 10);
        HunterStart = 24 - (difficulty * 10);
    }

    private void Spawn(int phase) //function that will spawn a random enemy
    {
        phase++; //the “phase” determines what enemies can spawn - phase++ is faze +1 to make the maths work
        randNum = Random.Range(1, phase); //spawns just enemy1

        //randNum = 1;
        //randNum = 2;
        //randNum = 3;

        if (randNum == 1)
        {
            Instantiate(enemy1, transform.position, Quaternion.identity);
        }
        else if (randNum == 2) //spawns enemy1 or enemy2
        {
            Instantiate(enemy2, transform.position, Quaternion.identity);
        }
        else if (randNum == 3) //spawns any enemy
        {
            Instantiate(enemy3, transform.position, Quaternion.identity);
        }
    }

    void Update()
    {
        if (timeBtwSpawn <= 0) //if the count down to spawn = 0
        {
            count = count + 1; //count the spawn

            timeBtwSpawn = startTimeBtwSpawn; //reset the clock
            if (startTimeBtwSpawn > 1.5f)
            {
                startTimeBtwSpawn = startTimeBtwSpawn - timeChange; //decrease the max on the clock
            }

            if (count <= ShooterStart) //start of the game
            {
                Spawn(1); //phase 1
            }
            else if (count > ShooterStart && count <= HunterStart)
            {
                Spawn(2); //phase 2
            }
            else if (count > HunterStart) //late game
            {
                Spawn(3); //phase 3
            }
        }
        else //if time doesn’t = 0
        {
            timeBtwSpawn -= Time.deltaTime * (difficulty * 1); //timer ticks down
        }
    }
}
