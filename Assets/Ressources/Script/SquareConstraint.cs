using UnityEngine;

public class SquareConstraint : MonoBehaviour
{
    [SerializeField][Range(-1f, 1f)] private float marginPercentage = 0.1f; // Marge en pourcentage (peut être négative pour agrandir)

    private RectTransform parentRect;
    private RectTransform childRect;

    private void Start()
    {
        // Obtenez les RectTransform du parent et de l'enfant
        childRect = GetComponent<RectTransform>();
        parentRect = transform.parent.GetComponent<RectTransform>();

        if (parentRect == null)
        {
            Debug.LogError("Ce GameObject n'a pas de parent avec un RectTransform !");
        }
    }

    private void Update()
    {
        if (parentRect == null) return;

        // Calculez la plus petite dimension du parent
        float parentMinDimension = Mathf.Min(parentRect.rect.width, parentRect.rect.height);

        // Calculez la marge basée sur le pourcentage
        float margin = parentMinDimension * marginPercentage;

        // Calculez la dimension disponible en tenant compte de la marge (peut agrandir si marge négative)
        float adjustedDimension = parentMinDimension - (margin * 2);

        // Assurez-vous que la dimension reste positive même avec une marge négative
        adjustedDimension = Mathf.Max(0, adjustedDimension);

        // Appliquez la même dimension à la largeur et à la hauteur de l'enfant
        childRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, adjustedDimension);
        childRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, adjustedDimension);
    }
}
