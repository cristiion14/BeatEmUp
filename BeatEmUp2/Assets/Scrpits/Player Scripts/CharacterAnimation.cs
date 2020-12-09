using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour {

    public Animator anim;
	// Use this for initialization
	void Awake()
    {
        anim = GetComponent<Animator>();
    }
	
    
    public void Walk(bool move)
    {
        anim.SetBool(AnimationTags.MOVEMENT, move);
    }
    public void Punch1()
    {
        anim.SetTrigger(AnimationTags.PUNCH_1_TRIGGER);
    }
    public void Punch2()
    {
        anim.SetTrigger(AnimationTags.PUNCH_2_TRIGGER);
    }
    public void Punch3()
    {
        anim.SetTrigger(AnimationTags.PUNCH_3_TRIGGER);
    }
    public void Kick1()
    {
        anim.SetTrigger(AnimationTags.KICK_1_TRIGGER);
    }
    public void Kick2()
    {
        anim.SetTrigger(AnimationTags.KICK_2_TRIGGER);
    }
   
    public void Jump()
    {
        anim.SetTrigger(AnimationTags.JUMP_TRIGGER);
    }

    public void Land()
    {
        anim.SetTrigger(AnimationTags.LAND_TRIGGER);
    }
}
