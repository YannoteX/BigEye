using UnityEngine;

public class SquareConstraint : MonoBehaviour
{
    [SerializeField][Range(-1f, 1f)] private float marginPercentage = 0.1f; // Marge en pourcentage (peut �tre n�gative pour agrandir)

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

        // Calculez la marge bas�e sur le pourcentage
        float margin = parentMinDimension * marginPercentage;

        // Calculez la dimension disponible en tenant compte de la marge (peut agrandir si marge n�gative)
        float adjustedDimension = parentMinDimension - (margin * 2);

        // Assurez-vous que la dimension reste positive m�me avec une marge n�gative
        adjustedDimension = Mathf.Max(0, adjustedDimension);

        // Appliquez la m�me dimension � la largeur et � la hauteur de l'enfant
        childRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, adjustedDimension);
        childRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, adjustedDimension);
    }
}
