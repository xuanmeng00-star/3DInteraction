using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int hp=100;
    [SerializeField] private GameObject deathObj;
    private UnityEngine.UI.Slider myBoold;
    private int maxHp;
    private Animator anEn;
    private string nowAnName;
    private State nowState;
    private AnimatorStateInfo anInfo;
    private bool isAnplaying;
    private void Start()
    {
        maxHp = hp;
        anEn = GetComponentInChildren<Animator>();
        myBoold = GetComponentInChildren<UnityEngine.UI.Slider>();
    }
    private void Update()
    {
        AnUpdate();
        if (!isAnplaying&&nowState!=State.Death)
            SetState(State.Idle, "Idle");
    }
    private void AnUpdate()
    {
        anInfo = anEn.GetCurrentAnimatorStateInfo(0);
        isAnplaying = !(anInfo.normalizedTime>=0.95f);
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
            Instantiate(deathObj, transform.position, Quaternion.identity);
        Destroy(this.gameObject, 3);
    }
    
}
