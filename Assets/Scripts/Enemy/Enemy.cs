using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int hp=100;
    [SerializeField] private GameObject deathObj,attack;
    private Player pl;
    private UnityEngine.UI.Slider myBoold;
    private int maxHp;
    private Animator anEn;
    private string nowAnName;
    private State nowState;
    private NavMeshAgent move;
    private AnimatorStateInfo anInfo;
    private bool isAnplaying;
    private void Start()
    {
        maxHp = hp;
        anEn = GetComponentInChildren<Animator>();
        myBoold = GetComponentInChildren<UnityEngine.UI.Slider>();
        move = GetComponent<NavMeshAgent>();
        pl = Player.instance;
    }
    private void Update()
    {
        AnUpdate();
       

        if (!isAnplaying&&nowState!=State.Death)
            if (Vector3.Distance(transform.position, pl.transform.position) > 4)
            {
                if (nowState != State.Walk)
                    SetState(State.Walk, "Walk");
                move.isStopped = false;
                move.SetDestination(pl.transform.position);
            }
            else if (nowState != State.Attack)
            {
                move.isStopped = true;
                attack.SetActive(true);
                SetState(State.Attack, "Attack");
            }

    }
    private void AnUpdate()
    {
        anInfo = anEn.GetCurrentAnimatorStateInfo(0);
        isAnplaying = !(anInfo.normalizedTime>=0.95f);
        if(!isAnplaying)
        {
            attack.SetActive(false);
            SetState(State.Idle, "Idle");
        }
    }
    public void GetHit(int _attack)
    {
        hp -= _attack;
        SetState(State.Hit,"Hit");
        myBoold.value = (float)hp / maxHp;
        if (hp <= 0)
            GetDie();
    }
    private void SetState(State _sta,string _anName)
    {
        nowState = _sta;
        anEn.Play(_anName,-1,0);
        nowAnName = _anName;
    }
    private void GetDie()
    {
        SetState(State.Death, "Die");
        if(deathObj!=null)
            Instantiate(deathObj, transform.position+Vector3.up*3, Quaternion.identity);
        Destroy(this.gameObject, 3);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {

            other.GetComponent<Player>().GetHit(10);
        }
    }
}
