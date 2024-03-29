using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : IState<Player> {
    private Player _player;

    public void Enter(Player owner) {
        _player = owner;
        _player.Animator.Play(AnimationName.Dead);
    }

    public void Update() {
    }

    public void FixedUpdate() {
    }

    public void Exit() {
    }
}
