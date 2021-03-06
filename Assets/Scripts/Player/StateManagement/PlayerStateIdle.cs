﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : PlayerState
{
    public override void OnEnterState(Player character)
    {
        character.Anim.Play("IdleBlend");
    }
    public override void HandleInput(Player character)
    {
        if (base.ComputeMovement().magnitude != 0) ToState(character, STATE_WALK);
        character.Shoot();
        if (Input.GetKeyDown(KeyCode.X)) character.Shoot();
        if (Input.GetKeyDown(KeyCode.Space)) ToState(character, STATE_DODGE);
    }
    public override void Update(Player character)
    {
        character.Shoot();
    }
    public override void HandleDamage(Player character, Vector2 recoilDirection, float recoilStrength)
    {
        //Cambiamos de estado
        if (character.GetCurrentHealth() <= 0)
            ToState(character, STATE_DEATH);
        else
            ToState(character, STATE_RECOIL);
    }
}
