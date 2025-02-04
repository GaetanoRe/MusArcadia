using UnityEngine;

public class TransparentRaycast : MonoBehaviour
{
    [Header("References")]
    public Camera mainCamera; // La caméra principale

    public Transform target;  // Le personnage ou l'objet à cibler

    [Header("Transparency Settings")]
    [Range(0f, 1f)] public float transparentAlpha = 0.5f; // Transparence appliquée aux objets

    [Header("Layer Mask")]
    public LayerMask obstacleMask; // Les layers pour les obstacles

    public LayerMask groundLayer; // Le layer à ignorer (Ground)

    private float originalAlpha = 1f; // Alpha par défaut

    // Les objets à rendre transparents
    private SpriteRenderer lastHitSprite; // Le dernier SpriteRenderer modifié

    private void Update()
    {
        // Vérifie si la caméra et le personnage sont assignés
        if (mainCamera == null || target == null) return;

        // Effectue un raycast entre la caméra et le personnage
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 direction = target.position - cameraPosition;

        RaycastHit2D hit = Physics2D.Raycast(cameraPosition, direction, direction.magnitude, obstacleMask);

        // Si un obstacle est détecté
        if (hit.collider != null)
        {
            // Vérifie que l'objet n'appartient pas au layer "Ground"
            if (((1 << hit.collider.gameObject.layer) & groundLayer) == 0)
            {
                SpriteRenderer hitSprite = hit.collider.GetComponent<SpriteRenderer>();

                if (hitSprite != null)
                {
                    // Si l'objet détecté est différent du dernier modifié
                    if (lastHitSprite != hitSprite)
                    {
                        ResetLastHit(); // Réinitialise l'opacité du dernier objet
                        lastHitSprite = hitSprite;

                        // Rend l'objet actuel transparent
                        SetSpriteTransparency(hitSprite, transparentAlpha);
                    }
                }
            }
        }
        else
        {
            // Réinitialise l'opacité si aucun objet n'est détecté
            ResetLastHit();
        }
    }

    /// <summary>
    /// Réinitialise la transparence de l'objet précédent.
    /// </summary>
    private void ResetLastHit()
    {
        if (lastHitSprite != null)
        {
            SetSpriteTransparency(lastHitSprite, originalAlpha);
            lastHitSprite = null;
        }
    }

    /// <summary>
    /// Modifie la transparence d'un SpriteRenderer.
    /// </summary>
    /// <param name="sprite">Le SpriteRenderer à modifier.</param>
    /// <param name="alpha">La valeur d'alpha à appliquer.</param>
    private void SetSpriteTransparency(SpriteRenderer sprite, float alpha)
    {
        Color color = sprite.color;
        color.a = alpha;
        sprite.color = color;
    }
}