using UnityEngine;


public class Projectile : MonoBehaviour
{
    public float speed = 50; //projectile speed - should be the fastest thing in game
    [Space]
    private float difficulty = Butten_Controler.difficulty;

    void Update()
    {
        transform.Translate(Vector2.up * speed * difficulty * Time.deltaTime); //move forward
    }

    private void OnTriggerEnter2D(Collider2D collision) //if collide with anything
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject); //destory self
            Debug.Log("Hit Player"); //and notify - score calculations happen in enemies
        }
        else if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Debug.Log("Hit Enemy");
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
            Debug.Log("Hit Wall");
        }
    }
}

