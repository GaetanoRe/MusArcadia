using UnityEngine;
using UnityEngine.AI; // N'oubliez pas d'ajouter cette référence pour utiliser NavMeshAgent

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 10f; // Force de saut
    public float rotationSpeed = 700f; // Vitesse de rotation
    public Camera playerCamera;
    private NavMeshAgent navMeshAgent;  // Référence au NavMeshAgent
    private Rigidbody rb;  // Le Rigidbody 3D du personnage, si nécessaire pour le saut

    private void Start()
    {
        rb = GetComponent<Rigidbody>();  // Récupère le Rigidbody 3D attaché à ce GameObject
        navMeshAgent = GetComponent<NavMeshAgent>();  // Récupère le NavMeshAgent attaché au GameObject

        if (playerCamera == null)
        {
            playerCamera = Camera.main;  // Si la caméra n'est pas assignée, on prend la caméra principale
        }
    }

    private void Update()
    {
        // Récupère les entrées horizontales (A/D ou flèches gauche/droite) pour le mouvement
        float horizontalInput = Input.GetAxis("Horizontal");  // Axe des X (gauche/droite)
        float verticalInput = Input.GetAxis("Vertical");      // Axe des Z (avant/arrière)

        // Crée un vecteur de mouvement basé sur les entrées
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // On applique le mouvement en fonction de l'orientation de la caméra
        if (moveDirection.magnitude > 0)
        {
            // Calculer la direction en fonction du forward de la caméra
            Vector3 cameraForward = playerCamera.transform.forward;
            cameraForward.y = 0f; // On ignore l'axe Y pour éviter que le personnage se déplace vers le haut/bas
            cameraForward.Normalize(); // Normalisation pour éviter que la vitesse ne varie

            // La direction du mouvement est basée sur l'orientation de la caméra
            Vector3 cameraRight = playerCamera.transform.right;
            cameraRight.y = 0f;
            cameraRight.Normalize();

            Vector3 move = cameraForward * verticalInput + cameraRight * horizontalInput;

            // Utilisez la vitesse du NavMeshAgent au lieu de la variable moveSpeed
            navMeshAgent.Move(move * navMeshAgent.speed * Time.deltaTime); // Utilise la vitesse du NavMeshAgent
        }

        // Si la touche de saut (espace) est pressée et que le personnage est au sol
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Applique une impulsion pour sauter
        }
    }

    // Vérifie si le personnage est au sol (pour éviter les doubles sauts)
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f); // Un rayon vérifie si le sol est sous le personnage
    }
}