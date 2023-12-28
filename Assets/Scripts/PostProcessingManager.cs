using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingManager : MonoBehaviour
{
    private static PostProcessingManager instance;
    private PostProcessVolume postProcessVolume;
    private Vignette vignetteLayer;

    public static PostProcessingManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PostProcessingManager>();
                if (instance == null)
                {
                    GameObject managerObject = new GameObject("PostProcessingManager");
                    instance = managerObject.AddComponent<PostProcessingManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        postProcessVolume = gameObject.AddComponent<PostProcessVolume>();
        postProcessVolume.profile = ScriptableObject.CreateInstance<PostProcessProfile>();

        postProcessVolume.profile.AddSettings<Vignette>();
        postProcessVolume.profile.TryGetSettings(out vignetteLayer);
    }

    public void IncreaseVignetteIntensity(float increaseAmount)
    {
        vignetteLayer.intensity.value += increaseAmount;
    }
}
