using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Movement : MonoBehaviour
{
    public SpriteRenderer sr;
    public GameObject sproutyr;
    public GameObject necruff;
    public GameObject sluggle;
    public GameObject quackle;
    public GameObject hydrake;
    public Pausing Pausing;
    public HealthBar Health;

    public GameObject spiritChange;

    public SpiritUI spiritCount;

    public Fireball firePrefab;
    public Fireball bubblePrefab;

    public float movementSpeed;
    public float jumpForce;

    public Rigidbody2D rb;
    public Transform feet;
    public LayerMask groundLayers;
    public LayerMask solidGroundLayers;
    public LayerMask waterLayers;

    public GameObject necruffButton;
    public GameObject sluggleButton;
    public GameObject quackleButton;
    public GameObject hydrakeButton;

    public string Elestral;

    private float extraJumpsBase;
    private float extraJumps;

    private float Qspeed;

    private float swimSpeed;
    private float Sspeed;
    public float bubbleSize;

    public float fireSize;

    public float strength;

    public float facing;
    public float moving;

    private float xpos;
    private float ypos;

    float mx;

    public bool Walking;
    public bool Jumping;
    public bool Falling;

    private bool dialoguePause;
    public bool paused;

    public bool pulling;

    private bool hurting;

    private Vector3 checkpointPos;
    private bool touchingCheckpoint;

    public AudioSource theme;
    public AudioSource earthSound;
    public AudioSource fireSound;
    public AudioSource waterSound;
    public AudioSource Splash;
    public AudioSource Item;

    private void Start()
    {
        Elestral = "Sproutyr";

        extraJumpsBase = 0;
        extraJumps = extraJumpsBase;

        Qspeed = 5;
        swimSpeed = 0;
        Sspeed = 3;
        strength = 0;
        fireSize = 0;
        bubbleSize = 0;

        facing = 1;
        moving = 0;

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && paused == false)
        {
            Time.timeScale = 0f;
            spiritChange.SetActive(true);
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            Time.timeScale = 1f;
            spiritChange.SetActive(false);
        }

        if (Pausing.paused == true || dialoguePause == true)
        {
            paused = true;
        }
        else if (Pausing.paused == false && dialoguePause == false)
        {
            paused = false;
        }

        if (paused == false)
        {
            mx = Input.GetAxisRaw("Horizontal");
        }
        else if (paused == true)
        {
            Walking = false;
            mx = 0f;
        }
        
        if (Input.GetAxisRaw("Horizontal") != 0 && paused == false)
        {
            Walking = true;
            if(Input.GetAxisRaw("Horizontal") > 0)
            {
                moving = 1;
                facing = 1;
            }
            else if(Input.GetAxisRaw("Horizontal") < 0)
            {
                moving = -1;
                facing = -1;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            Walking = false;
            moving = 0;
        }

        if (rb.velocity.y > 0.5)
        {
            Jumping = true;
            Falling = false;
        }
        else if (rb.velocity.y < -0.5)
        {
            Jumping = false;
            Falling = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && Elestral == "Necruff" && fireSize > 0 && paused == false)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.E) && Elestral == "Sluggle" && bubbleSize > 0 && paused == false && IsSwimming() == true)
        {
            ShootBubble();
        }

        if (Input.GetButtonDown("Jump"))
        {
            if(IsGrounded())
            {
                Jump();
            }
            else if(Elestral == "Hydrake" && extraJumps > 0)
            {
                Jump();
                extraJumps = extraJumps - 1;
            }
            else if(Elestral == "Sluggle" && IsSwimming())
            {
                Jump();
            }
        }
        
        if (IsOnSolidGround() == true && IsSwimming() == false)
        {
            xpos = transform.position.x;
            ypos = transform.position.y;
            Sspeed = 3;
        }

        if (IsSwimming() && Elestral != "Sluggle")
        {
            StartCoroutine(WaterReturn());
        }
        else if (IsSwimming() && Elestral == "Sluggle")
        {
            Sspeed = swimSpeed;
        }

        if (touchingCheckpoint == true)
        {
            checkpointPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
        }


        //GET RID OF THESE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(Hurt());
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            NecruffButton();
            SluggleButton();
            QuackleButton();
            HydrakeButton();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            EarthSpirit();
            FireSpirit();
            WaterSpirit();
            ThunderSpirit();
            WindSpirit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dialogue")
        {
            dialoguePause = true;
        }

        if (collision.tag == "Enemy" & hurting == false)
        {
            StartCoroutine(Hurt());
        }

        if (collision.tag == "Ambrosia")
        {
            Item.Play();
            Heal();
        }

        if (collision.tag == "Checkpoint")
        {
            touchingCheckpoint = true;
        }

        if (collision.tag == "Water")
        {
            Splash.Play();
        }

        if (collision.tag == "Water" && Elestral == "Sluggle")
        {
            theme.volume = 0.05f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Dialogue")
        {
            dialoguePause = false;
        }

        if (collision.tag == "Checkpoint")
        {
            touchingCheckpoint = false;
        }

        if (collision.tag == "Water" && Elestral == "Sluggle")
        {
            Splash.Play();
            theme.volume = 0.1f;
        }
    }

    private void FixedUpdate()
    {
        if(Elestral == "Quackle")
        {
            Vector2 movement = new Vector2(mx* (movementSpeed + Qspeed), rb.velocity.y);
            rb.velocity = movement;
        }
        else if (Elestral == "Sluggle")
        {
            Vector2 movement = new Vector2(mx* (movementSpeed - Sspeed), rb.velocity.y);
            rb.velocity = movement;
        }
        else
        {
            Vector2 movement = new Vector2(mx* movementSpeed, rb.velocity.y);
            rb.velocity = movement;
        }
    }
    
    private void Jump()
    {
        if (Elestral == "Sluggle" && IsSwimming() == false)
        {
            Vector2 movement = new Vector2(rb.velocity.x, (jumpForce/2));
            rb.velocity = movement;
        }
        else
        {
            Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
            rb.velocity = movement;
        }
    }

    public bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.1f, groundLayers);
        if(groundCheck != null)
        {
            Falling = false;
            extraJumps = extraJumpsBase;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsOnSolidGround()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.1f, solidGroundLayers);
        if(groundCheck != null)
        {
            Falling = false;
            extraJumps = extraJumpsBase;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsSwimming()
    {
        Collider2D waterCheck = Physics2D.OverlapCircle(feet.position, 0.1f, waterLayers);
        if(waterCheck != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Shoot()
    {
        Fireball fireball = Instantiate(firePrefab, transform.position, transform.rotation);
        fireball.SetSize(fireSize);
        fireball.Project(new Vector3 (facing,0,0));
    }

    private void ShootBubble()
    {
        Fireball bubble = Instantiate(bubblePrefab, transform.position, transform.rotation);
        bubble.SetSize(bubbleSize);
        bubble.Project(new Vector3 (facing,0,0));
    }

    public void EarthSpirit()
    {
        earthSound.Play();
        strength = strength + 1;
        spiritCount.Earth();
    }

    public void FireSpirit()
    {
        fireSound.Play();
        fireSize = fireSize + 1;
        spiritCount.Fire();
    }

    public void WaterSpirit()
    {
        waterSound.Play();
        swimSpeed = swimSpeed - 1;
        bubbleSize = bubbleSize + 1;
        spiritCount.Water();
    }

    public void ThunderSpirit()
    {
        Qspeed = Qspeed + 1; 
        spiritCount.Thunder();
    }

    public void WindSpirit()
    {
        extraJumpsBase = extraJumpsBase + 1;
        extraJumps = extraJumps + 1;
        spiritCount.Wind();
    }

    public void Sproutyr()
    {
        Elestral = "Sproutyr";
        sproutyr.transform.position = transform.position;
        sproutyr.SetActive(true);
        necruff.SetActive(false);
        sluggle.SetActive(false);
        quackle.SetActive(false);
        hydrake.SetActive(false);
        spiritChange.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Necruff()
    {
        Elestral = "Necruff";
        necruff.transform.position = transform.position;
        sproutyr.SetActive(false);
        necruff.SetActive(true);
        sluggle.SetActive(false);
        quackle.SetActive(false);
        hydrake.SetActive(false);
        spiritChange.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Sluggle()
    {
        Elestral = "Sluggle";
        sluggle.transform.position = transform.position;
        sproutyr.SetActive(false);
        necruff.SetActive(false);
        sluggle.SetActive(true);
        quackle.SetActive(false);
        hydrake.SetActive(false);
        spiritChange.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quackle()
    {
        Elestral = "Quackle";
        quackle.transform.position = transform.position;
        sproutyr.SetActive(false);
        necruff.SetActive(false);
        sluggle.SetActive(false);
        quackle.SetActive(true);
        hydrake.SetActive(false);
        spiritChange.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Hydrake()
    {
        Elestral = "Hydrake";
        hydrake.transform.position = transform.position;
        sproutyr.SetActive(false);
        necruff.SetActive(false);
        sluggle.SetActive(false);
        quackle.SetActive(false);
        hydrake.SetActive(true);
        spiritChange.SetActive(false);
        Time.timeScale = 1f;
    }

    public void NecruffButton()
    {
        necruffButton.SetActive(true);
    }

    public void SluggleButton()
    {
        sluggleButton.SetActive(true);
    }

    public void QuackleButton()
    {
        quackleButton.SetActive(true);
    }

    public void HydrakeButton()
    {
        hydrakeButton.SetActive(true);
    }
    
    private IEnumerator Hurt()
    {
        hurting = true;
        Health.Hurt();
        yield return new WaitForSeconds(1f);
        hurting = false;
    }

    private IEnumerator WaterReturn()
    {
        yield return new WaitForSeconds(0.25f);
        transform.position = new Vector3 (xpos, ypos, 0f);
    }

    private void Heal()
    {
        Health.Heal();
    }

    public void Checkpoint()
    {
        transform.position = checkpointPos;
        Health.FullHeal();
    }
}