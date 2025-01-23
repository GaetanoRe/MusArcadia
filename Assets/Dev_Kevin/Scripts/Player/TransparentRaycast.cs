using UnityEngine;

public class TransparentRaycast : MonoBehaviour
{
    [Header("References")]
    public Camera mainCamera; // La cam�ra principale

    public Transform target;  // Le personnage ou l'objet � cibler

    [Header("Transparency Settings")]
    [Range(0f, 1f)] public float transparentAlpha = 0.5f; // Transparence appliqu�e aux objets

    [Header("Layer Mask")]
    public LayerMask obstacleMask; // Les layers pour les obstacles

    public LayerMask groundLayer; // Le layer � ignorer (Ground)

    private float originalAlpha = 1f; // Alpha par d�faut

    // Les objets � rendre transparents
    private SpriteRenderer lastHitSprite; // Le dernier SpriteRenderer modifi�

    private void Update()
    {
        // V�rifie si la cam�ra et le personnage sont assign�s
        if (mainCamera == null || target == null) return;

        // Effectue un raycast entre la cam�ra et le personnage
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 direction = target.position - cameraPosition;

        RaycastHit2D hit = Physics2D.Raycast(cameraPosition, direction, direction.magnitude, obstacleMask);

        // Si un obstacle est d�tect�
        if (hit.collider != null)
        {
            // V�rifie que l'objet n'appartient pas au layer "Ground"
            if (((1 << hit.collider.gameObject.layer) & groundLayer) == 0)
            {
                SpriteRenderer hitSprite = hit.collider.GetComponent<SpriteRenderer>();

                if (hitSprite != null)
                {
                    // Si l'objet d�tect� est diff�rent du dernier modifi�
                    if (lastHitSprite != hitSprite)
                    {
                        ResetLastHit(); // R�initialise l'opacit� du dernier objet
                        lastHitSprite = hitSprite;

                        // Rend l'objet actuel transparent
                        SetSpriteTransparency(hitSprite, transparentAlpha);
                    }
                }
            }
        }
        else
        {
            // R�initialise l'opacit� si aucun objet n'est d�tect�
            ResetLastHit();
        }
    }

    /// <summary>
    /// R�initialise la transparence de l'objet pr�c�dent.
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
    /// <param name="sprite">Le SpriteRenderer � modifier.</param>
    /// <param name="alpha">La valeur d'alpha � appliquer.</param>
    private void SetSpriteTransparency(SpriteRenderer sprite, float alpha)
    {
        Color color = sprite.color;
        color.a = alpha;
        sprite.color = color;
    }
}