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

    #region Player Animations

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

    public void GroundKick()
    {
        anim.SetTrigger(AnimationTags.GROUND_KICK);
    }
   
    public void Jump()
    {
        anim.SetTrigger(AnimationTags.JUMP_TRIGGER);
    }

    public void Land()
    {
        anim.SetTrigger(AnimationTags.LAND_TRIGGER);
    }

    public void Run()
    {
        anim.SetTrigger(AnimationTags.RUN_TRIGGER);
    }

    public void Defend()
    {
        anim.SetTrigger(AnimationTags.DEFEND_TRIGGER);
    }

    public void GroundPunch()
    {
        anim.SetTrigger(AnimationTags.GROUND_PUNCH);
    }

    public void JumpKick()
    {
        anim.SetTrigger(AnimationTags.JUMP_KICK);
    }

    #endregion

    #region Enemy Animations
    public void EnemyAttack(int attack)
    {
        //Switch case to select the specific attack
        switch(attack)
        {
                case 0:
                    {
                        anim.SetTrigger(AnimationTags.ATTACK_1_TRIGGER);
                        break;
                    }
                case 1:
                    {
                        anim.SetTrigger(AnimationTags.ATTACK_2_TRIGGER);
                        break;
                    }
                case 2:
                    {
                        anim.SetTrigger(AnimationTags.ATTACK_3_TRIGGER);
                        break;
                    }
            }
    }

    public void Play_IddleAnim()
    {
        anim.Play(AnimationTags.IDLE_ANIMATION);
    }

    public void EnemyWalk(bool move)
    {
        anim.SetBool(AnimationTags.MOVEMENT, move);
    }

    public void KnockDown()
    {
        anim.SetTrigger(AnimationTags.KNOCK_DOWN_TRIGGER);
    }

    public void StandUP()
    {
        anim.SetTrigger(AnimationTags.STAND_UP_TRIGGER);
    }

    public void Hit()
    {
        anim.SetTrigger(AnimationTags.HIT_TRIGGER);
    }

    public void Death()
    {
        anim.SetTrigger(AnimationTags.DEATH_TRIGGER);
    }
    #endregion
}
