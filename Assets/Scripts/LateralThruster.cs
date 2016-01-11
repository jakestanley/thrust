using UnityEngine;
using System.Collections;

public class LateralThruster : MonoBehaviour {

    public GameObject gameObject;
    public ParticleSystem particleSystem;

	// Use this for initialization
	void Start () {
        particleSystem.startSpeed = 10;
        particleSystem.emissionRate = DEFAULT_EMISSION_RATE;
        particleSystem.maxParticles = MAX_PARTICLES;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void engage(){
        particleSystem.enableEmission = true;
    }

    public void disengage(){
        particleSystem.enableEmission = false;
    }

    private const int DEFAULT_SPEED = 10;
    private const int DEFAULT_EMISSION_RATE = 50;
    private const int MAX_PARTICLES = 500;
}
