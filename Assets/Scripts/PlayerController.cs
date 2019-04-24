using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;
    public float powerUp; 
    public bool rate;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
   
    private float nextFire;

    private Rigidbody rb;
    private AudioSource Audio;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        PickUp();

      if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject clone = Instantiate(shot, shotSpawn.position, shotSpawn.rotation); 
            Audio.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            rate = true;
            powerUp = 5;
            Destroy(other.gameObject);
        }
    }

    void PickUp()
    {

        if (rate == true)
        {
            powerUp -= Time.deltaTime;
            fireRate = 0.10f;

        }
        else if (rate == false)
        {
            fireRate = .45f;
        }

        if (powerUp <=0)
        {
            rate = false;
        }

    }


    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
             Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
             0.0f,
             Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}