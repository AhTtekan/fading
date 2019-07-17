using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public SoundPlayer sp;
    public EnemyManager em;
    public float timeInLightToDie = 3f;
    public float timeLeftToDie = 30f;
    public float speed = 5f;
    public bool isAggro = false;
    public float timeUntilAutoAggro;// = 2*60*60f;//2 minutes
    public float timeUntilNextWander = 180f;
    [SerializeField]
    public Vector3 wanderDestination;// = transform.position;
    public float fleeing = 0f;
    public GameObject fleeFromSource;
    public LightCheck lc;
    public LightCheck playerLc;
    public float MaxDistFromPlayer;
    bool sendDialog = false, movable = true;
    public float timeSinceLastNoise=0;
    //bool facingLeft = false;
    // Use this for initialization
    void Start()
    {
        wanderDestination = (Random.insideUnitSphere * 15) + transform.position;
        wanderDestination.z = 0;
        playerLc = GameObject.Find("Player").transform.Find("PlayerSprite").GetComponent<LightCheck>();
        em = GameObject.Find("Main Camera").GetComponent<EnemyManager>();
        timeUntilAutoAggro = Random.Range(60 * 60, 60 * 60 * 2);
        timeSinceLastNoise = Random.Range(60 * 15, 60 * 90);
    }

    // Update is called once per frame
    void Update()
    {
        DoLoop();
    }
    public void DoLoop()
    {
        if(timeSinceLastNoise <= 0)
        {
            sp.PlaySound(Random.Range(0, 2));
            timeSinceLastNoise = Random.Range(60 * 15, 60 * 120);
        }
        else
        {
            timeSinceLastNoise--;
        }
        if (movable)
        {
            BuildAI();
            if (lc.isInLight)
            {
                this.timeLeftToDie -= Time.deltaTime;
                if (timeLeftToDie < 0)
                    Die();
            }
            timeUntilAutoAggro -= 1;
            if (timeUntilAutoAggro <= -60 * 10)
            {
                timeUntilAutoAggro = Random.Range(60 * 60, 60 * 60 * 2);
            }
            CheckPlayerDist();
        }
    }

    private void CheckPlayerDist()
    {
        float dist = Vector3.Distance(transform.position, playerLc.transform.position);
        if(dist < 15 && !sendDialog)
        {
            var dm = GameObject.FindObjectOfType<DialogManager>();
            dm.queue.Enqueue(dm.dialogs[3]);
            sendDialog = true;
        }
        if (dist > MaxDistFromPlayer)
            Die();
    }
    
    public virtual void Aggro(bool stealItems)
    {
        //move towards player
        MoveTowardsPlayer();
        if(Vector3.Distance(transform.position, playerLc.transform.position) < 2)
        {
            var anim = transform.Find("StalkerSprite").GetComponent<Animator>();
            anim.SetTrigger("Attack");
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 towardsDirection = playerLc.transform.position - transform.position;
        Debug.DrawRay(transform.position,
            towardsDirection,
            Color.blue
            );
        towardsDirection.Normalize();
        Move(towardsDirection, true);
    }

    public virtual void Wander()
    {
        if (timeUntilNextWander <= 0)
        {
            var dir = wanderDestination - transform.localPosition;
            dir.Normalize();
            Debug.DrawLine(transform.localPosition, wanderDestination, Color.yellow);
            Move(dir, false);
            if (Vector3.Distance(transform.localPosition, wanderDestination) < 1)
            {
                wanderDestination = (Random.insideUnitSphere * 15) + transform.position;
                wanderDestination.z = 0;
                timeUntilNextWander = Random.Range(120, 60 * 5);
            }
        }
        else
        {
            timeUntilNextWander--;
        }
    }
    public void Flee()
    {
        Vector3 awayDirection = new Vector3();
        if (fleeing > 0)
        {
            fleeing--;
            awayDirection = transform.position - fleeFromSource.transform.position;
        }
        else
        {
            fleeFromSource = lc.lightSource.gameObject;
            fleeing = 180;
            awayDirection = (transform.position - lc.lightSource.position);
        }

        Debug.DrawRay(transform.position,
            awayDirection,
            Color.blue
            );
        wanderDestination = (Random.insideUnitSphere * 15) + transform.position;
        awayDirection.Normalize();
        Move(awayDirection, true);
    }
    public virtual void BuildAI()
    {
        if (fleeing > 0f || (lc.isInLight && lc.lightSource != null))
        {
            Flee();
        }
        else
        {
            if (!playerLc.isInLight)
            {
                Aggro(false);
            }
            else
            {
                if (timeUntilAutoAggro <= 0)
                {
                    Aggro(true);
                }
                else
                {
                    Wander();
                }
            }
        }
    }
    public void Die()//called by animation renderer after death anim plays
    {
        movable = false;
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(1f);
        em.enemies.Remove(this);
        GameObject.Destroy(this.gameObject);
        yield return null;
    }

    void Move(Vector3 dir, bool fast)
    {
        if(dir.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1);
        }
        transform.Translate(dir * this.speed * (fast ? 1.5f : 1f) * Time.deltaTime);
        transform.position = new Vector3(transform.position.x,
            transform.position.y, 0);

    }
}
