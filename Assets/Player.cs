using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] TMP_Text stepText;
    [SerializeField, Range(0.01f, 1f)] float moveDuration=0.2f;
    [SerializeField, Range(0.01f, 1f)] float jumpHeight=0.5f;

    private float backWall;
    private float leftWall;
    private float rightWall;
    [SerializeField] private int maxTravel;
    public int MaxTravel { get => maxTravel;}
    [SerializeField]private int currentTravel;
    public int CurrentTravel { get => currentTravel; }
    public bool IsDie { get => this.enabled == false;}

    [SerializeField] public AudioSource jumping;
    [SerializeField] public AudioSource crash;


    public void SetUp(int minZPos, int extent)
    {
        backWall = minZPos - 1;
        leftWall = -(extent + 1 );
        rightWall = extent + 1;
    }

    private void Update()
    {
        var moveDir = Vector3.zero;
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveDir += new Vector3(0, 0, 1);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDir += new Vector3(0, 0, -1);
        }
         if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDir += new Vector3(1, 0, 0);
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        { 
            moveDir += new Vector3(-1, 0, 0);
        }
        
        if(moveDir == Vector3.zero)
            return;

        if(IsJumping() == false)
        Jump(moveDir);

        if(IsJumping() == true)
        jumping.Play();
    }

    private void Jump(Vector3 targetDirection)
    {
        // Atur rotasi
        var TargetPosition = transform.position + targetDirection;
        transform.LookAt(TargetPosition);
        // loncat ke atas
        var moveSeq = DOTween.Sequence(transform);
        moveSeq.Append(transform.DOMoveY(jumpHeight, moveDuration/2));
        moveSeq.Append(transform.DOMoveY(0, moveDuration/2));
        
        if(TargetPosition.z <= backWall ||
            TargetPosition.x <= leftWall ||
            TargetPosition.x >= rightWall
            )
            return;


        if(Tree.AllPositions.Contains(TargetPosition))
            return;

        

        // gerak maju/mundur/samping
        transform.DOMoveX(TargetPosition.x, moveDuration);
        transform.DOMoveZ(TargetPosition.z, moveDuration)
            .OnComplete(UpdateTravel);
    }

    private void UpdateTravel()
    {
        currentTravel = (int) this.transform.position.z;
        if(currentTravel > maxTravel)
            maxTravel = currentTravel;

        stepText.text = "STEP: " + maxTravel.ToString();
    }

    public bool IsJumping()
    {
        return DOTween.IsTweening(transform);
    }

    private void OnTriggerEnter(Collider other) {
       if(other.tag == "Car")
       {
            AnimateDie();
            crash.Play();
       }
    }

    private void AnimateDie()
    {
        transform.DOScaleY(0.1f, 0.2f);
        transform.DOScaleX(3, 0.2f);
        transform.DOScaleZ(2, 0.2f);
        this.enabled = false;
    }

    private void OnTriggerStay(Collider other) {

    }

    private void OnTriggerExit(Collider other) {
        
    }
}
