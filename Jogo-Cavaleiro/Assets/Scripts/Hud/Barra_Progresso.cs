using UnityEngine;

public class Barra_Progresso : MonoBehaviour
{
    public Transform playerTransform;
    public Transform topOfTower;
    public Transform bottomOfTower;

    public RectTransform iconUI;           // Ícone que sobe
    public RectTransform progressBar;      // Barra onde o ícone se move

    private float minY;
    private float maxY;

    void Start()
    {
        // Define limites locais da barra onde o ícone pode subir
        float halfBarHeight = progressBar.rect.height / 2f;

        minY = -halfBarHeight;             // Base da barra
        maxY = halfBarHeight;              // Topo da barra
    }

    void Update()
    {
        float totalHeight = topOfTower.position.y - bottomOfTower.position.y;
        float playerHeight = Mathf.Clamp(playerTransform.position.y - bottomOfTower.position.y, 0f, totalHeight);

        float normalizedProgress = playerHeight / totalHeight;

        // Interpola entre minY e maxY com base no progresso
        float newY = Mathf.Lerp(minY, maxY, normalizedProgress);
        Vector3 newLocalPos = iconUI.localPosition;
        newLocalPos.y = newY;
        iconUI.localPosition = newLocalPos;
    }
}
