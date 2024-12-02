using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public ParticleSystem collisionParticles;

    public ParticleSystem collisionParticlesPower;

    public void particulasColision()
    {
        collisionParticles.Play();
    }

    public void particulasColisionPower()
    {
        collisionParticlesPower.Play();
    }
}