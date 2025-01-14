using UnityEngine;
using UnityEngine.AI; // N'oubliez pas d'ajouter cette r�f�rence pour utiliser NavMeshAgent

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 10f; // Force de saut
    public float rotationSpeed = 700f; // Vitesse de rotation
    public Camera playerCamera;
    private NavMeshAgent navMeshAgent;  // R�f�rence au NavMeshAgent
    private Rigidbody rb;  // Le Rigidbody 3D du personnage, si n�cessaire pour le saut

    private void Start()
    {
        rb = GetComponent<Rigidbody>();  // R�cup�re le Rigidbody 3D attach� � ce GameObject
        navMeshAgent = GetComponent<NavMeshAgent>();  // R�cup�re le NavMeshAgent attach� au GameObject

        if (playerCamera == null)
        {
            playerCamera = Camera.main;  // Si la cam�ra n'est pas assign�e, on prend la cam�ra principale
        }
    }

    private void Update()
    {
        // R�cup�re les entr�es horizontales (A/D ou fl�ches gauche/droite) pour le mouvement
        float horizontalInput = Input.GetAxis("Horizontal");  // Axe des X (gauche/droite)
        float verticalInput = Input.GetAxis("Vertical");      // Axe des Z (avant/arri�re)

        // Cr�e un vecteur de mouvement bas� sur les entr�es
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // On applique le mouvement en fonction de l'orientation de la cam�ra
        if (moveDirection.magnitude > 0)
        {
            // Calculer la direction en fonction du forward de la cam�ra
            Vector3 cameraForward = playerCamera.transform.forward;
            cameraForward.y = 0f; // On ignore l'axe Y pour �viter que le personnage se d�place vers le haut/bas
            cameraForward.Normalize(); // Normalisation pour �viter que la vitesse ne varie

            // La direction du mouvement est bas�e sur l'orientation de la cam�ra
            Vector3 cameraRight = playerCamera.transform.right;
            cameraRight.y = 0f;
            cameraRight.Normalize();

            Vector3 move = cameraForward * verticalInput + cameraRight * horizontalInput;

            // Utilisez la vitesse du NavMeshAgent au lieu de la variable moveSpeed
            navMeshAgent.Move(move * navMeshAgent.speed * Time.deltaTime); // Utilise la vitesse du NavMeshAgent
        }

        // Si la touche de saut (espace) est press�e et que le personnage est au sol
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Applique une impulsion pour sauter
        }
    }

    // V�rifie si le personnage est au sol (pour �viter les doubles sauts)
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f); // Un rayon v�rifie si le sol est sous le personnage
    }
}