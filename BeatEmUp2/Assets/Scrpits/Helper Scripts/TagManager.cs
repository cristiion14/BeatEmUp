﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTags : MonoBehaviour {
    public const string MOVEMENT = "Movement";

    public const string PUNCH_1_TRIGGER = "Punch1";
    public const string PUNCH_2_TRIGGER = "Punch2";
    public const string PUNCH_3_TRIGGER = "Punch3";

    public const string KICK_1_TRIGGER = "Kick1";
    public const string KICK_2_TRIGGER = "Kick2";

    public const string ATTACK_1_TRIGGER = "Attack1";
    public const string ATTACK_2_TRIGGER = "Attack2";
    public const string ATTACK_3_TRIGGER = "Attack3";

    public const string IDLE_ANIMATION = "Idle";

    public const string KNOCK_DOWN_TRIGGER = "KnockDown";
    public const string STAND_UP_TRIGGER = "StandUp";
    public const string HIT_TRIGGER = "Hit";
    public const string DEATH_TRIGGER = "Death";

    public const string JUMP_TRIGGER = "Jump";
    public const string LAND_TRIGGER = "Land";
    public const string RUN_TRIGGER = "Run";
    public const string DEFEND_TRIGGER = "Defend";
    public const string GROUND_KICK = "GroundKick";
    public const string GROUND_PUNCH = "GroundPunch";
    public const string JUMP_KICK = "JumpKick";

    public const string HIT = "Hit";

    public const string CAN_SMOKE = "CanSmoke";
    public const string CROWBAR_1 = "Crowbar1";
    public const string CROWBAR_2 = "Crowbar2";
    public const string CROWBAR_3 = "Crowbar3";
    public const string PICKUP_OBJ = "PickUPOBJ";
}

public class Axis
{
    public const string HORIZONTAL_AXIS = "Horizontal";
    public const string VERTICAL_AXIS = "Vertical";
}

public class Tags
{
    public const string GROUND_TAG = "Ground";
    public const string PLAYER_TAG = "Player";
    public const string ENEMY_TAG = "Enemy";

    public const string LEFT_ARM_TAG = "LeftArm";
    public const string LEFT_LEG_TAG = "LeftLeg";
    public const string UNTAGGED_TAG = "Untagged";
    public const string MAIN_CAMERA_TAG = "MainCamera";
    public const string HEALTH_UI = "HealthUI";


    /// <summary>
    /// tags of the attack points of the player
    /// </summary>
    public const string PLeft_Punch = "LeftPunch";
    public const string PRight_Punch = "RightPunch";
    public const string PLeft_Kick = "LeftKick";
    public const string PRight_Kick = "RightKick";


    

}

public class EnemyAnim
{
    public const string FOUND_PLAYER = "FoundPlayer";
    public const string CAN_ATTACK = "CanAttack";

}
