using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Walk,
    Run,
    Attack,
    Hit,
    Pick,
    Jump,
    Death
}
public class Player : MonoBehaviour
{
    [SerializeField] private float speed, upForce,atk;
    [SerializeField] private GameObject attack;
    private Transform mainCa;
    private float vertical, horizontal;
    private int hp,maxHp;
    private State nowSta;
    private Vector3 dirFor,dirRig,dir;
    private Animator anPlay;
    private Rigidbody rb;
    private bool canUp, isAnPlayer;
    private string nowAnName;
    private AnimatorStateInfo stateinfo;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCa = Camera.main.transform;
        anPlay = GetComponent<Animator>();
        maxHp = hp;
    }
    private void Update()
    {
        InputUp();
        AnUpate();
    }
    private void LateUpdate()
    {
        Move();
    }

    private void InputUp()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        dirFor = new Vector3(mainCa.forward.x, 0, mainCa.forward.z);
        dirRig = new Vector3(mainCa.right.x, 0, mainCa.right.z);
        dir = (dirFor*vertical + dirRig* horizontal).normalized;

        if (Input.GetButtonDown("Jump")&&canUp)
            Jump();
        if (Input.GetMouseButtonDown(0)&&nowAnName!="Attack")
            Attack();
    }
    private void Move()
    {        
        if (vertical == 0 && horizontal == 0)
            if (nowSta == State.Walk)
                SetState(State.Idle, "Idle");
            else if(nowSta != State.Idle)
                return;
        transform.Translate(dir * speed * Time.deltaTime,Space.World);
        if (vertical != 0 || horizontal != 0)
        {
            transform.forward = dir;
            if(!isAnPlayer )
                SetState(State.Walk, "Run");
        }
    }
    private void Jump()
    {
        canUp = false;
        if (nowSta == State.Walk || nowSta == State.Idle)
            SetState(State.Jump, "Walk");
        rb.AddForce(Vector3.up * upForce);
    }
    public void GetHit(int _attack)
    {
        hp -= _attack;
    }
    public void SetState(State _want,string _playAnName)
    {
        nowSta = _want;
        anPlay.Play(_playAnName , -1, 0);
        nowAnName = _playAnName;
    }
    private void Attack()
    {
        SetState(State.Attack, "Attack");
        attack.SetActive(true);
    }
    private void AnUpate()
    {
        stateinfo = anPlay.GetCurrentAnimatorStateInfo(0);
        isAnPlayer = !(stateinfo.normalizedTime>=0.95f);
        if (!isAnPlayer)
        {
            attack.SetActive(false);
            SetState(State.Idle,"Idle");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.ClosestPoint(this.transform.position).y < transform.position.y)
        {
            canUp = true;
            if (nowSta == State.Jump)
                SetState(State.Idle, "Idle");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            other.GetComponent<Enemy>().GetHit(10);
        if (other.CompareTag("Atk"))
            other.GetComponent<SetObj>().GetHit();
    }
}

