﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenuAttribute(fileName = "New Hand Ability", menuName = "Player/Abilities/Hand", order = 51)]
public class HandAbility : Ability
{
    [SerializeField] private float _attackForce;
    [SerializeField] private float _usefulTime;

    private AttackState _state;
    private Coroutine _coroutine;
       
    public override event UnityAction AbilityEnded;

    public override void UseAbility(AttackState attack)
    {
        if (_coroutine != null)
            Reset();

        _state = attack;

        _coroutine = _state.StartCoroutine(Attack(_state));
        _state.CollisionDetected += OnPlayerAttack;
    }

    private void OnPlayerAttack(IDamagable damagable)
    {
        if (damagable.ApplyDamage(_state.Rigidbody, _attackForce) == false)
            return;
        {
            _state.Rigidbody.velocity /= 2;
        }
    }

    private IEnumerator Attack(AttackState state)
    {
        float time = _usefulTime;

        while(time > 0)
        {
            state.Rigidbody.velocity = state.Rigidbody.velocity.normalized * _attackForce;
            time -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }    

    private void Reset()
    {
        _state.Rigidbody.velocity = Vector3.zero;
        _state.StopCoroutine(_coroutine);
        _coroutine = null;
        _state.CollisionDetected -= OnPlayerAttack;
    }
}
