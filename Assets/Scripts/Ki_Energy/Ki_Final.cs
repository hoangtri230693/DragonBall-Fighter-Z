using UnityEngine;

public class Ki_Final : MonoBehaviour
{
    [SerializeField] EffectSounds_Controller effectsoundController;

    Animator animator;
    Rigidbody2D rb;

    float ki_Speed = 10.0f;
    bool isBooming = false;


    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        effectsoundController = GameObject.Find("EffectSounds_Controller").GetComponent<EffectSounds_Controller>();
    }

    private void Update()
    {
        if (!isBooming)
        {
            rb.linearVelocity = new Vector2(ki_Speed * transform.localScale.x, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player 1" || collision.gameObject.tag == "Player 2" || 
            collision.gameObject.tag == "Ki Final" || collision.gameObject.tag == "Ki DragonFist")
        {
            isBooming = true;
            rb.linearVelocity = Vector2.zero;
            transform.position = collision.transform.position;
            animator.SetBool("Boom", true);
            effectsoundController.PlayKiFinalBoomSound();
        }
    }

    public void End_Boom()
    {
        Destroy(gameObject);
    }
}
