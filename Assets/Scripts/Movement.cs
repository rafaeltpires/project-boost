using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float mainThrust = 400f;
    [SerializeField] private float rotationThrust = 125f;
    [SerializeField] private float rocketRotation = 250f;
    
    [SerializeField] private AudioClip rocketThrust;
    
    [SerializeField] ParticleSystem thrustParticles;
    
    private Rigidbody rb;
    private AudioSource mAudioSource;
    private GameObject AtomRocket;
    
    private bool isAlive;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mAudioSource = GetComponent<AudioSource>();
        thrustParticles = GameObject.Find("Rocket Jet Particles").GetComponent<ParticleSystem>();
        AtomRocket = GameObject.Find("AtomRocket");
    }
    private void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    private void ProcessThrust()
    {
        if (!Input.GetKey(KeyCode.Space))
        {
            mAudioSource.Stop();
            thrustParticles.Stop();
            return;
        }
        
        AtomRocket.transform.Rotate(Time.deltaTime * rocketRotation * Vector3.up);
        rb.AddRelativeForce(Time.deltaTime * mainThrust *  Vector3.up); // Adiciona força relativa ao objecto, tendo em conta a direção // Vector3 estar no fim irá ser mais eficiente: https://stackoverflow.com/questions/57933831/unity-rider-order-of-multiplication-operations-is-inefficient
        
        if (!mAudioSource.isPlaying)
        {
            mAudioSource.PlayOneShot(rocketThrust);
        }
        
        if (!thrustParticles.isPlaying)
        {
            thrustParticles.Play();
        }
        
    }
    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            ApplyRotation(-rotationThrust);
        }
    }
    private void ApplyRotation(float rotationThisFrame)
    { 
        rb.freezeRotation = true; // congelar a rotação, para que possamos corrigir manualmente
        transform.Rotate(Time.deltaTime * rotationThisFrame * Vector3.forward);
        rb.freezeRotation = false;
    }
}
