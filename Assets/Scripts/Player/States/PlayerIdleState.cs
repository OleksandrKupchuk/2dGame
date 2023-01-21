using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : IState<Player> {
    private Player _player;

    public void Enter(Player owner) {
        Debug.Log($"<color=yellow>enter idle state</color>");

        _player = owner;
        _player.Animator.Play(PlayerAnimationName.Idle);
        //_player.Animator.Play(PlayerAnimationName.Run);
    }

    public void ExecuteUpdate() {
        //Debug.Log("info = " + _player.Animator.GetCurrentAnimatorStateInfo(0).IsName(PlayerAnimationName.Attack));
        //Debug.Log($"<color=yellow>idle execute</color>");
        //Debug.Log("jump button press = " + _player.JumpInputAction.action.triggered);
        if (_player.isHit) {
            _player.StateMachine.ChangeState(_player.HitState);
        }
        else if (!_player.IsGround()) {
            _player.StateMachine.ChangeState(_player.JumpDownState);
        }
        else if (_player.CanJump) {
            _player.StateMachine.ChangeState(_player.JumpUpState);
        }
        //else if (_player.isAttack) {
        //    _player.StateMachine.ChangeState(_player.AttackState);
        //}
        else if (Mathf.Abs(_player.GetMovementInput().x) > 0) {
            _player.StateMachine.ChangeState(_player.RunState);
        }
    }

    public void ExecuteFixedUpdate() {
        //Debug.Log($"<color=yellow>idle fixed execute</color>");
    }

    public void Exit() {
        Debug.Log($"<color=red>exit </color><color=yellow>idle state</color>");

        _player.Animator.StopPlayback();
    }
}
