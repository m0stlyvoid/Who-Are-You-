using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class Kugel : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Image image;
    private AudioSource audioSource;
    private TextMeshProUGUI textMeshPro;
    private Vector3 startingPosition;
    private bool commentVisible;
    private bool playedNoRedLifes;
    private bool playedNoGreenLifes;
    private bool playedNoBlueLifes;
    public bool canShoot;
    public bool green;
    public bool red;
    public bool blue;
    public bool hitBlue;
    public bool hitRed;
    public bool hitGreen;
    public int hitCount;
    public float bubbleNumber;
    [SerializeField] private float positioningState;
    [SerializeField] private float shootForce;
    [SerializeField] private float gravity;
    [SerializeField] private float rotionSpeed;
    [SerializeField] private float moveSpeed;
    
    [SerializeField] private float startingLifes;
    [SerializeField] private float redLifes;
    [SerializeField] private float blueLifes;
    [SerializeField] private float greenLifes;
    [SerializeField] private GameObject rotationMarker;
    [SerializeField] private GameObject end;
    [SerializeField] private GameObject redButtonObj;
    [SerializeField] private GameObject redText;
    [SerializeField] private GameObject blueButtonObj;
    [SerializeField] private GameObject blueText;
    [SerializeField] private GameObject greenButtonObj;
    [SerializeField] private GameObject greenText;
    [SerializeField] private GameObject kommentar;
    [SerializeField] private GameObject kommentarText;
    [SerializeField] private AudioClip hitWall;
    [SerializeField] private AudioClip destroyBubble;
    [SerializeField] private AudioClip noLives;
    [SerializeField] private AudioClip moving;
    [SerializeField] private AudioClip lockIn;
    [SerializeField] private AudioClip shoot;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioClip movingSound;
    [SerializeField] private AudioClip close;
    [SerializeField] private AudioClip restartSound;



    private comment commentScript;
  
    GameObject[] listGreenBubbles;
    GameObject[] listBlueBubbles;
    GameObject[] listRedBubbles;
    GameObject[] listWalls;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        image = kommentar.GetComponent<Image>();
        textMeshPro = kommentarText.GetComponent<TextMeshProUGUI>();
        commentScript = kommentarText.GetComponent<comment>();
        audioSource = gameObject.GetComponent<AudioSource>();

    }
    // Start is called before the first frame update
    void Start()
    {
        positioningState = 0;
        startingPosition = transform.position;
        Debug.Log(startingPosition);
        canShoot = false;


        sr.color = new Vector4(0, 0, 0, 1);

        // count bubbles
        listGreenBubbles = GameObject.FindGameObjectsWithTag("green");
        listRedBubbles = GameObject.FindGameObjectsWithTag("red");
        listBlueBubbles = GameObject.FindGameObjectsWithTag("blue");
        bubbleNumber = listBlueBubbles.Length + listGreenBubbles.Length + listRedBubbles.Length;
        Debug.Log(bubbleNumber);
        Debug.Log("blue bubbles:" + listBlueBubbles.Length);

        // set number of lives
        redLifes = startingLifes ;
        blueLifes = startingLifes ;
        greenLifes = startingLifes ;

        image.enabled = false;
        textMeshPro.enabled = false;
        commentVisible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //postioningste 0 -> set position
        //positioningstate 1 -> set rotatin
        // positioningstate 2 -> shoot
        //if (Input.GetKey("space") & canShoot & positioningState)

        if (canShoot) // move, rotate, shoot
        {
           if (Input.GetKeyDown("space") && positioningState < 3)
            {
                positioningState = positioningState + 1;
              
                 if (positioningState == 2)
                {
                    ballShot();
                }
                else
                {
                    audioSource.clip = lockIn;
                    audioSource.Play();
                }
            }
            else if (positioningState == 0)
            {
                move();
            }
            else if (positioningState == 1)
            {
                if (Input.GetKey("d"))
                {
                    rotateLeft();
                }
                if (Input.GetKey("a"))
                {
                    rotateRight();
                }
              
                playMovingSound();
            }
        }
      else  if (Input.GetKeyDown("space")) //can't shoot , reset when missed
        {
            if (positioningState == 2)
            {
                stopShot();
                ResetBall();
               // if (blueLifes > 0 || redLifes > 0 || greenLifes > 0)
                {
                   // audioSource.clip = restartMissed;
                   // audioSource.Play();
                }

            }
        }


        if (green | blue | red)
        {
            redButtonObj.SetActive(false);
            blueButtonObj.SetActive(false);
            greenButtonObj.SetActive(false);
            Debug.Log("button off");
        }

        // reset ball
        // reset when hit
        if (commentVisible && Input.GetKeyDown("space") | Input.GetMouseButtonDown(0)) 
        {
            ResetBall();
            audioSource.clip = close;
            audioSource.Play();
        }

        if ( bubbleNumber == 0 ||
            redLifes == 0 | listRedBubbles.Length == 0 &&
            greenLifes== 0  | listGreenBubbles.Length == 0 &&
            blueLifes == 0 | listBlueBubbles.Length == 0)

        {
            done();
            Debug.Log("done");
            audioSource.clip = close;
            audioSource.loop = false;
            redLifes = 1;S
        }

        //reset when hit
        if (end.activeInHierarchy && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(1);
        }
    }

    void ballShot()
    {
        audioSource.clip = shoot;
        audioSource.Play();
        Debug.Log("shoot");
        rb.AddForce(transform.up * shootForce);
        canShoot = false;
        rb.gravityScale = gravity;
        rotationMarker.SetActive(false);

    }

    void rotateLeft()
    {
        transform.Rotate(0, 0, rotionSpeed);
        rb.constraints = RigidbodyConstraints2D.None;

    }

    void rotateRight()
    {
        transform.Rotate(0, 0, rotionSpeed * -1);
        rb.constraints = RigidbodyConstraints2D.None;
    }

    void move()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);
        playMovingSound();
      
    }

    private void playMovingSound()
    {
        if (Input.GetKeyDown("d") | Input.GetKeyDown("a"))
        {
            audioSource.clip = movingSound;
            audioSource.Play();
            audioSource.loop = true;
        }
        if (Input.GetKeyUp("d") | Input.GetKeyUp("a"))
        {
            audioSource.loop = false;
            audioSource.Stop();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("hit " + collider.gameObject.tag);

       if (collider.gameObject.tag == "wall")
        {
            audioSource.clip = hitWall;
            audioSource.Play();  
        }


        if (collider.gameObject.tag == "green" & green || collider.gameObject.tag == "red" & red || collider.gameObject.tag == "blue" & blue)
        {
            audioSource.clip = destroyBubble;
            audioSource.Play();
            Destroy(collider.gameObject);
            stopShot();
            bubbleNumber = bubbleNumber - 1;
            image.enabled = true;
            textMeshPro.enabled = true;
            commentVisible = true;
            Debug.Log(collider.gameObject.tag);
            hitCount = hitCount + 1;
           

            if (collider.gameObject.tag == "green")
            {
                hitGreen = true;
                greenLifes = startingLifes;
              
            }

            else if (collider.gameObject.tag == "red")
            {
                hitRed = true;
                redLifes = startingLifes;
                Debug.Log("hit red");
            }

            else
            {
                hitBlue = true;
                blueLifes = startingLifes;
            }

            commentScript.DisplayComment();

            
        }
    }
    public void GreenButton()
    {
        green = true;
        blue = false;
        red = false;
        canShoot = true;
        sr.color = new Vector4(0, 1, 0, 0.75f);
        greenLifes = greenLifes - 1;
        audioSource.clip = buttonSound;
        audioSource.Play();
    }

    public void BlueButton()
    {
        green = false;
        blue = true;
        red = false;
        canShoot = true;
        sr.color = new Vector4(1, 0.92f, 0.016f, 0.75f);
        blueLifes = blueLifes - 1;
        audioSource.clip = buttonSound;
        audioSource.Play();
    }

    public void RedButton()
    {
        green = false;
        blue = false;
        red = true;
        canShoot = true;
        sr.color = new Vector4(1, 0, 0, 0.75f);
        redLifes = redLifes - 1;
        audioSource.clip = buttonSound;
        audioSource.Play();
    }

    void done()

    {
        end.SetActive(true);
        listWalls = GameObject.FindGameObjectsWithTag("wall");
        foreach (GameObject wall in listWalls)
        {
            wall.SetActive(false);
        }
       
        foreach (GameObject obj in listRedBubbles)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj2 in listGreenBubbles)
        {
            obj2.SetActive(false);
        }
        foreach (GameObject obj3 in listBlueBubbles)
        {
            obj3.SetActive(false);
        }
        sr.enabled = false;
        rotationMarker.SetActive(false);
        canShoot = true;
       bubbleNumber = 1;
    }

    public void activateShooting()
    {
        canShoot = true;
    }

    private void stopShot()
    {
        rb.velocity = new Vector2(0.0f, 0.0f);
        rb.angularVelocity = 0;
        canShoot = false;
        rb.gravityScale = 0;

        Debug.Log(hitBlue & hitGreen & hitRed);

        green = false;
        blue = false;
        red = false;
    }

    private void ResetBall()
    {
        listBlueBubbles = GameObject.FindGameObjectsWithTag("blue");
        listGreenBubbles = GameObject.FindGameObjectsWithTag("green");
        listRedBubbles = GameObject.FindGameObjectsWithTag("red");
        positioningState = 0;
        image.enabled = false;
        textMeshPro.enabled = false;
        commentVisible = false;
        rotationMarker.SetActive(true);
        transform.position = (startingPosition);
        Debug.Log("blue bubbles:" + listBlueBubbles.Length);
        hitBlue = false;
        hitGreen = false;
        hitRed = false;

        // hi Patrick,
        // ich weiß, dass das nicht die besste stelle im script ist, aber
        // ich hab keinen Bock mehr auf das Projekt und wills fertig kriegen
        // der komplette Rest meiner Game Jam Gruppe hatte noch nie ne game engine aufgemacht
        // bock bisschen zu lernen wie man sachen rum schiebt um mit fertigen assets level desing machen zu können
        // oder wie man sprites austauscht hatten die auch nicht
        // also musste ich alles machen + beim game & leveldesing helfen + sprites neu formatieren + orga
        // hat natürlich nicht gepasst
        // also projekt zuhause weitermachen
        // die ghosten mich ürbrigens jetzt auch
        // ich bin überhaupt erst auf den gamejam geganen um irgendein c# projekt zu machen
        // und ich find die idee nicht cool genug da jetzt noch massiv Zeit reinzu stecken
        // also funktioniert = good enough & ich geh andere Projekte machen

        // play restart sound
        if (blueLifes > 0 && redLifes > 0 && greenLifes > 0)
        {
            audioSource.clip = restartSound;
            audioSource.Play();
            Debug.Log("lives not 0");
        }
        else if (blueLifes == 0 && playedNoBlueLifes)
        {
            audioSource.clip = restartSound;
            audioSource.Play();
        }
        else if (greenLifes == 0 && playedNoGreenLifes)
        {
            audioSource.clip = restartSound;
            audioSource.Play();
        }

        sr.color = new Vector4(0, 0, 0, 1);

        if (listBlueBubbles.Length > 0 && blueLifes > 0)
        {
            blueButtonObj.SetActive(true);
          

        }  
        else if (!playedNoBlueLifes & blueLifes == 0)
        {
            audioSource.clip = noLives;
            audioSource.Play();
            Debug.Log("no blue lives");
            playedNoBlueLifes = true;
        }

        if (listGreenBubbles.Length > 0 && greenLifes > 0)
        {
            greenButtonObj.SetActive(true);
          
        }
        else if (!playedNoGreenLifes)
        {
            audioSource.clip = noLives;
            audioSource.Play();
            playedNoGreenLifes = true;
          
        }

        if (listRedBubbles.Length > 0 && redLifes > 0)
        {
            redButtonObj.SetActive(true);

        }
        else if (!playedNoRedLifes)
        {
            audioSource.clip = noLives;
            audioSource.Play();
            playedNoRedLifes = true;
        } 
    }
}