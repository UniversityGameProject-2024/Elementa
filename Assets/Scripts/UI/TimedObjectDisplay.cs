using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimedImageDisplay : MonoBehaviour
{
    [Header("UI Settings")]
    [Tooltip("The image to fill horizontally.")]
    [SerializeField] private Image imageToFill;

    [Tooltip("The total time the image will remain visible (in seconds).")]
    [SerializeField] private float visibleDuration = 5f;

    [Tooltip("The speed of the fill effect (time it takes for one loop to complete).")]
    [SerializeField] private float fillSpeed = 1f;

    private void Start()
    {
        if (imageToFill != null)
        {
            // Ensure the image type is set to Filled
            if (imageToFill.type != Image.Type.Filled)
            {
                imageToFill.type = Image.Type.Filled;
                imageToFill.fillMethod = Image.FillMethod.Horizontal;
            }

            // Start the coroutine to handle looping fill and visibility
            StartCoroutine(FillAndDisappear());
        }
        else
        {
            Debug.LogWarning("Image to fill is not assigned.");
        }
    }

    private IEnumerator FillAndDisappear()
    {
        float elapsedVisibleTime = 0f;

        while (elapsedVisibleTime < visibleDuration)
        {
            float elapsedFillTime = 0f;

            // Fill the image in a loop during the visible duration
            while (elapsedFillTime < fillSpeed)
            {
                imageToFill.fillAmount = elapsedFillTime / fillSpeed;

                // Wait for the next frame
                yield return null;

                // Increment elapsed fill time
                elapsedFillTime += Time.deltaTime;
            }

            // Reset the fill amount for the next loop
            imageToFill.fillAmount = 0f;

            // Increment the total visible time
            elapsedVisibleTime += fillSpeed;
        }

        // Ensure the image is hidden when the visible duration ends
        imageToFill.gameObject.SetActive(false);
    }
}