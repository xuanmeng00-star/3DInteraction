using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SetObj : MonoBehaviour
{
    [SerializeField] private UnityEvent onTrigger, outTrigger, mouseDown;
    [SerializeField] private GameObject atk;
    private Animation an;
    private void Start()
    {
        an = GetComponent<Animation>();
    }
    private void OnTriggerEnter(Collider other)
    {
        onTrigger.Invoke();
        if (this.CompareTag("Door"))
            an.Play();
    }
    private void OnTriggerExit(Collider other)
    {
        outTrigger.Invoke();
        if (this.CompareTag("Door"))
            an.Play();
    }
    private void OnMouseDown()
    {
        mouseDown.Invoke();
    }
    public void GetHit()
    {
        if (atk != null)
        {
            Instantiate(atk, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else
            an.Play();
    }
}
