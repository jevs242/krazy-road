using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

    [SerializeField]private Vector3 OriginalTransform;
    [SerializeField]private Vector3 TinyTransform;
    [SerializeField]private AudioSource _Gem;
    [SerializeField]private AudioSource _Music;
    [SerializeField]private AudioSource _KartIdle;
    [SerializeField]private AudioSource _KartRunning;
    [SerializeField]private Renderer Body;
    [SerializeField]private Renderer Kart;
    [SerializeField]private GameObject[] Hat;
    [SerializeField]private Material[] Color;
    [SerializeField]private float VelocitySum = 0.1f;
    [SerializeField] public Animator TinyImage; 

    [SerializeField]
    private float maxSpeed = 30;

    [SerializeField] private int MoveTime = 10;
    private int desiredLane = 1; // 0:Left 1:middle 2:right

    public float laneDistance = 4; //Distance of the lane

    public float Gravity = 9.8f;

    public bool TinyActivateP = false;
    public bool Tiny = false;
    Animator m_Animator;


    [SerializeField] private AudioClip SoundCollition;
    [SerializeField] private GameObject SoundKart;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        m_Animator = gameObject.GetComponent<Animator>();
        SetPlayer();

        _KartIdle.Play();
        TinyImage.SetTrigger("NoReady");

    }

    // Update is called once per frame
    void Update()
    {
        if((int)forwardSpeed == 30)
        {
            MoveTime = 19;
        }
        else if((int)forwardSpeed == 40)
        {
            MoveTime = 20;
        }

        if (!PlayerManager.isGameStarted)
        {
            if(SwipeManager.tap)
            {
                _KartIdle.Stop();
                _KartRunning.Play();
                _Music.Stop();
                StartCoroutine(TinyActivate(5.0f));
            }
            return;
        }
        if (forwardSpeed < maxSpeed)
            forwardSpeed += VelocitySum * Time.deltaTime;

        if((int)forwardSpeed == 20)
        {
            VelocitySum = 0.07f;
        }
        else if((int)forwardSpeed == 25)
        {
            VelocitySum = 0.05f;
        }

        PlayerManager.Mph = (int)forwardSpeed;
        PlayerManager.Distance += forwardSpeed * Time.deltaTime;
        direction.z = forwardSpeed;

        direction.y -= Gravity * Time.deltaTime;

        if(SwipeManager.swipeRight)
        {
            desiredLane++;
            if(desiredLane == 3)
            {
                desiredLane = 2;
            }
        }

        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if(desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;

        }
        else if(desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        //transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.fixedDeltaTime);
        if (transform.position == targetPosition - transform.position)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * MoveTime * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.isGameStarted)
            return;
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle2" && Tiny)
        {
            Physics.IgnoreCollision(hit.collider, this.GetComponent<Collider>());
            return;
        }

        if(hit.transform.tag == "Obstacle" || hit.transform.tag == "Obstacle2")
        {
            _KartRunning.Stop();
            _Music.Play();
            SoundKart.SetActive(false);
            AudioSource.PlayClipAtPoint(SoundCollition, new Vector3(0, transform.position.y, transform.position.z), 10f);
            PlayerManager.gameOver = true;
        }
    }

    private void SetPlayer()
    {
        
        Kart.material = Color[PlayerPrefs.GetInt("NowColorKart")];
        Body.material = Color[PlayerPrefs.GetInt("NowColorPlayer")];
        if(Hat[PlayerPrefs.GetInt("NowHat")] != null)
        {
            Hat[PlayerPrefs.GetInt("NowHat")].SetActive(true);
        }
    }

    public void Sound()
    {
        if(!_Gem.isPlaying)
        {
            _Gem.Play();
        }
    }

    public void SoundEngine(bool Off)
    {
        if(Off)
        {
            _KartRunning.Stop();
            _Music.Play();
        }
        else
        {
            _Music.Stop();
            _KartRunning.Play();
        }
    }
    IEnumerator TinyActivate(float time)
    {
        yield return new WaitForSeconds(time);
        TinyImage.ResetTrigger("NoReady");
        TinyImage.SetTrigger("Ready");
        TinyActivateP = true;
    }

    public void TinyFuntion()
    {
        if(TinyActivateP)
        {
            Tiny = true;
            m_Animator.ResetTrigger("NoTiny");
            m_Animator.SetTrigger("YesTiny");
            TinyImage.ResetTrigger("Ready");
            TinyImage.SetTrigger("Used");
            TinyActivateP = false;
            StartCoroutine(TinyDelay(8.0f));
        }
    }

    IEnumerator TinyDesactivate(float time)
    {
        yield return new WaitForSeconds(time);
        m_Animator.ResetTrigger("YesTiny");
        Tiny = false;
        TinyImage.ResetTrigger("Red");
        TinyImage.SetTrigger("NoReady");
        m_Animator.SetTrigger("NoTiny");
        StartCoroutine(TinyRecovery(20f));
    }

    IEnumerator TinyDelay(float time)
    {
        yield return new WaitForSeconds(time);

        TinyImage.ResetTrigger("NoTiny");
        TinyImage.SetTrigger("Red");
        StartCoroutine(TinyDesactivate(2.0f));
    }

    IEnumerator TinyRecovery(float time)
    {
        yield return new WaitForSeconds(time);
        TinyImage.ResetTrigger("NoReady");
        TinyImage.SetTrigger("Ready");
        TinyActivateP = true;
    }
}
