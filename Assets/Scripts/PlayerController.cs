using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    private StateMachine stateMachine;
    
    

    private void Awake()
    {
    }
    private void Start()
    {
        stateMachine = new StateMachine();
        stateMachine.SetState(new IdleState(gameObject, text));
    }

    private void Update()
    {
        stateMachine.Update();
        stateMachine.FixedUpdate();

        float h = Input.GetAxisRaw("Horizontal");
        if (h != 0 && stateMachine.GetCurrentState() is not WalkState walkState)
        {
            stateMachine.SetState(new WalkState(gameObject, text));
        }
        else if (h == 0 && stateMachine.GetCurrentState() is not IdleState idleState)
        {
            stateMachine.SetState(new IdleState(gameObject, text));
        }

    }
}
