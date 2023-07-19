using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KDH_ColorCheck : MonoBehaviour
{
    public static KDH_ColorCheck instance;
    Texture2D texture;
    public RenderTexture renderTexture;
    private void Awake()
    {
        instance = this;
        texture = new Texture2D(1024, 1024, TextureFormat.RGB24, false);
    }

    public int ColorCheck()
    {
        // Cast a ray from the character's position downward
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Renderer renderer = hit.collider.GetComponent<Renderer>();

            if (renderer != null)
            {
                //print(hit.collider.name);
                // Get the UV coordinates at the hit point
                Vector2 uv = hit.textureCoord;
                //print(uv);
                // Get the texture on the object


                Paintable paintable = hit.collider.GetComponent<Paintable>();

                if(paintable == null)
                {
                    return 3;
                }

                renderTexture = paintable.getMask();

                // Create a new Texture2D with the same dimensions as the RenderTexture
                //Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);

                // Set the active RenderTexture and read the pixels from it into the Texture2D
                RenderTexture.active = renderTexture;
                texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
                texture.Apply();

                // Reset the active RenderTexture and release the temporary RenderTexture
                RenderTexture.active = null;
                //renderTexture.Release();

                // Convert UV coordinates to pixel coordinates
                int pixelX = Mathf.FloorToInt(uv.x * texture.width);
                int pixelY = Mathf.FloorToInt(uv.y * texture.height);

                // Get the color at the pixel
                Color pixelColor = texture.GetPixel(pixelX, pixelY);

                //Debug.Log("Color at hit point: " + pixelColor);

                return CheckColorRGB(pixelColor);
            }
        }
        return 3;
    }


    [System.Serializable]
    public struct ColorRange
    {
        public float minValue;
        public float maxValue;
    }

    public ColorRange player_redRange;
    public ColorRange player_greenRange;
    public ColorRange player_blueRange;

    public ColorRange enemy_redRange;
    public ColorRange enemy_greenRange;
    public ColorRange enemy_blueRange;

    public int CheckColorRGB(Color color)
    {
        if (ColorComponentInRange(color.r, player_redRange) &&
            ColorComponentInRange(color.g, player_greenRange) &&
            ColorComponentInRange(color.b, player_blueRange))
        {
            //Debug.Log("Player Paint");
            return 1;
        }
        else if (ColorComponentInRange(color.r, enemy_redRange) &&
                ColorComponentInRange(color.g, enemy_greenRange) &&
                ColorComponentInRange(color.b, enemy_blueRange))
        {
           // Debug.Log("Enemy Paint");
            return 2;
        }

        else
        {
            //Debug.Log("No Paint");
            return 3;

        }
    }

    private bool ColorComponentInRange(float value, ColorRange range)
    {
        // Check if the value falls within the specified range
        bool withinRange = value >= range.minValue && value <= range.maxValue;

        return withinRange;
    }
}
