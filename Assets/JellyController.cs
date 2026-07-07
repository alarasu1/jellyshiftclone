using UnityEngine;

public class JellyController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 5f;
    public float JelasticitySpeed = 10f;

    [Header("Scale Limits")]
    public float maxX = 1.2f;
    public float maxY = 1.2f;

    [Header("Jelly Mesh Reference")]
    public Transform jellyMesh;

    private Vector3 targetScale = Vector3.one;
    private bool isGameOver = false;

    void Start()
    {
        if (jellyMesh == null)
        {
            jellyMesh = transform.Find("jellymesh");
        }
    }

    void Update()
    {
        if (isGameOver) return;

        // 1. SÜREKLÝ ÝLERÝ GÝTME HAREKETÝ
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // 2. FARE HAREKETÝNE GÖRE JÖLEYE ÞEKÝL VERME
        if (Input.GetMouseButton(0))
        {
            float mouseY = Input.GetAxis("Mouse Y");

            if (mouseY > 0)
            {
                targetScale.y = maxY;
                targetScale.x = 0.3f;
            }
            else if (mouseY < 0)
            {
                targetScale.y = 0.15f;
                targetScale.x = maxX;
            }
        }
        else
        {
            targetScale = Vector3.one;
        }

        jellyMesh.localScale = Vector3.Lerp(jellyMesh.localScale, targetScale, JelasticitySpeed * Time.deltaTime);
    }

    // 3. EN KESÝN ÇÖZÜM: GEÇÝÞ ANINDA TETÝKLENME
    private void OnTriggerEnter(Collider other)
    {
        // Engel 1'in kontrol çizgisine girdiysek (YASSI OLMALIYIZ)
        if (other.gameObject.name.Contains("engel1"))
        {
            if (jellyMesh.localScale.y > 0.5f) // Eðer yassý deðilsek (Yükseksek)
            {
                StopGame();
            }
        }

        // Engel 2'nin kontrol çizgisine girdiysek (ÝNCE OLMALIYIZ)
        if (other.gameObject.name.Contains("engel2"))
        {
            if (jellyMesh.localScale.x > 0.5f) // Eðer ince deðilsek (Geniþsek)
            {
                StopGame();
            }
        }
    }

    void StopGame()
    {
        isGameOver = true;
        forwardSpeed = 0f;
    }
}