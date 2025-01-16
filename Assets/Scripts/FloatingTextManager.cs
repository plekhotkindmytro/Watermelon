using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;
public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager Instance;

    public Canvas targetCanvas;
    public GameObject floatingTextPrefab; // Reference to the TextMeshPro prefab
    public Transform scoreField;          // The position where the text should move (score UI position)
    public float moveDuration = 1f;       // Time for text to move to score field
    public Vector3 spawnOffset = new Vector3(0, 2, 0); // Offset where the text spawns
    public Vector3 scaleChange = new Vector3(0.5f, 0.5f, 0.5f); // Final scale at the score position

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SpawnFloatingText(Vector3 startScreenPosition, string textContent, bool isStatic, float time)
    {
        // Instantiate the floating text prefab
        GameObject floatingText = Instantiate(floatingTextPrefab, startScreenPosition, Quaternion.identity);
        floatingText.transform.SetParent(targetCanvas.transform, true);
        //floatingText.GetComponent<RectTransform>().position = startPosition;
        //floatingText.GetComponent<RectTransform>().SetParentstartPosition;
        // Set the text content
        TMP_Text textMesh = floatingText.GetComponent<TMP_Text>();
        if (textMesh != null)
        {
            textMesh.text = textContent;
        }

        // Move the text to the score position and scale it down
        if(!isStatic)
        {
            StartCoroutine(MoveToScoreField(floatingText));
        } else
        {
            floatingText.transform.localScale = Vector3.zero;
            floatingText.transform.DOScale(1f, 0.5f);
            Destroy(floatingText, time);
        }
    }


    private IEnumerator MoveToScoreField(GameObject floatingText)
    {
        yield return new WaitForSeconds(0.5f);

        Vector3 startScale = floatingText.transform.localScale;
        Vector3 endScale = startScale + scaleChange;

        float elapsedTime = 0f;

        Vector3 startPosition = floatingText.transform.position;
        Vector3 endPosition = scoreField.position;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;

            // Lerp position
            floatingText.transform.position = Vector3.Lerp(startPosition, endPosition, t);

            // Lerp scale
            floatingText.transform.localScale = Vector3.Lerp(startScale, endScale, t);

            yield return null;
        }

        // Ensure it ends at the target position
        floatingText.transform.position = endPosition;

        // Destroy the floating text
        Destroy(floatingText);
    }
}