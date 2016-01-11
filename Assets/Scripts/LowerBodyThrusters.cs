using UnityEngine;
using System.Collections;

public class LowerBodyThrusters : MonoBehaviour {

    public GameObject gameObject;
    public ParticleSystem engineTrailsA, engineTrailsB, engineTrailsC, engineTrailsD;

	// Use this for initialization
	void Start () {
	   setDefaultValues(engineTrailsA);
       setDefaultValues(engineTrailsB);
       setDefaultValues(engineTrailsC);
       setDefaultValues(engineTrailsD); // TODO stick in an array list
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void engage(){
        engineTrailsA.enableEmission = true;
        engineTrailsB.enableEmission = true;
        engineTrailsC.enableEmission = true;
        engineTrailsD.enableEmission = true;
    }

    public void disengage(){
        engineTrailsA.enableEmission = false;
        engineTrailsB.enableEmission = false;
        engineTrailsC.enableEmission = false;
        engineTrailsD.enableEmission = false;
    }

    private void setDefaultValues(ParticleSystem particleSystem){
        particleSystem.emissionRate = DEFAULT_EMISSION_RATE;
        particleSystem.startSpeed = 20;
        particleSystem.maxParticles = 1200;
    }

    private const int DEFAULT_SPEED = 20;
    private const int DEFAULT_EMISSION_RATE = 150;
    private const int MAX_PARTICLES = 1200;
    private const float DEFAULT_SCALE = 0.2f;
}
