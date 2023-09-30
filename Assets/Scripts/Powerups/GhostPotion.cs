using UnityEngine;

public class GhostPotion : IPowerUp
{
    private bool destroyed = false;
    public bool Destroyed => destroyed;
    private float countdownTimer = 999.0f; // How long until the potion is destroyed
    private float powerPool = 999.0f; // How much the entity can be untargetable for
    private Transform target; // Who will be untargetable
    
    // Visual transparency
    private Renderer renderer;

    public GhostPotion(Transform target)
    {
        this.target = target;

        // Visual transparency
        renderer = target.GetComponent<Renderer>();
        SetTransparencyModeToMaterial();

    }

    private void SetTransparencyModeToMaterial()
    {
        if(renderer != null)
        {
            renderer.material.SetFloat("_Mode", 2); // 2 corresponds to Transparent rendering mode.
            renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            renderer.material.SetInt("_ZWrite", 0);
            renderer.material.DisableKeyword("_ALPHATEST_ON");
            renderer.material.EnableKeyword("_ALPHABLEND_ON");
            renderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            renderer.material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        }
    }

    public void Destroy()
    {
        destroyed = true;
        target.gameObject.GetComponent<Collider>().enabled = true;
    }

    public void Tick()
    {
        if(!destroyed && countdownTimer > 0 && powerPool > 0)
        {
            countdownTimer -= Time.deltaTime;
            if(Input.GetKey(KeyCode.Space))
            {
                powerPool -= Time.deltaTime;
                target.gameObject.GetComponent<Collider>().enabled = false;
                ChangeAlpha(0.5f);
            }
            else
            {
                target.gameObject.GetComponent<Collider>().enabled = true;
                ChangeAlpha(1f);
            }
        }
        else
        {
            Destroy();
        }
    }

    private void ChangeAlpha(float alphaValue)
    {
        if (renderer != null)
        {
            // Get the current material and color.
            Material material = renderer.material;
            Color color = material.color;

            // Modify the alpha value.
            color.a = alphaValue;

            // Apply the modified color to the material.
            material.color = color;
        }
    }

    public void Use()
    {
        if(target.gameObject.GetComponent<Collider>() == null) { destroyed = true; }
        // Animation
    }
}
