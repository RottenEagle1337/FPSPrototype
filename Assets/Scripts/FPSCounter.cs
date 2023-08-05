using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public int FPS { get; private set; }
    [SerializeField] private TMP_Text lable;

    private void Update()
    {
        FPS = (int)(1f / Time.unscaledDeltaTime);
        lable.text = FPS.ToString();
    }
}
