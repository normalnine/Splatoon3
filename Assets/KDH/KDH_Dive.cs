using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KDH_Dive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            // Cast a ray from the character's position downward
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Renderer renderer = hit.collider.GetComponent<Renderer>();

                if (renderer != null)
                {
                    // Get the UV coordinates at the hit point
                    Vector2 uv = hit.textureCoord;
                    print(uv);
                    // Get the texture on the object
                    
                    RenderTexture renderTexture = hit.collider.gameObject.GetComponent<Paintable>().getMask();

                    // Create a new Texture2D with the same dimensions as the RenderTexture
                    Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);

                    // Set the active RenderTexture and read the pixels from it into the Texture2D
                    RenderTexture.active = renderTexture;
                    texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
                    texture.Apply();

                    // Reset the active RenderTexture and release the temporary RenderTexture
                    RenderTexture.active = null;
                    renderTexture.Release();

                    // Convert UV coordinates to pixel coordinates
                    int pixelX = Mathf.FloorToInt(uv.x * texture.width);
                    int pixelY = Mathf.FloorToInt(uv.y * texture.height);

                    // Get the color at the pixel
                    Color pixelColor = texture.GetPixel(pixelX, pixelY);

                    Debug.Log("Color at hit point: " + pixelColor);
                }
            }
        }
        
    }
}