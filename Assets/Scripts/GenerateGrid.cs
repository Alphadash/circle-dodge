using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    public Material TileMaterial;

    public static int GridSize;

    public Camera Camera;
    
	void Start()
    {
        AdjustCamera(1 + ((GridSize - 3) * 0.5f), 1.35f + ((GridSize - 3) * 0.65f), 3.3f + ((GridSize - 3) * 0.7f));

        GameObject dummy;
	    for (int x = 0; x < GridSize; x++)
        {
            for (int y = 0; y < GridSize; y++)
            {
                dummy = GameObject.CreatePrimitive(PrimitiveType.Quad);
                dummy.GetComponent<Renderer>().material = TileMaterial;
                dummy.transform.position = new Vector3(x, y, Gameplay.TileZPosition);
            }
        }
    }

    private void AdjustCamera(float x, float y, float size)
    {
        Camera.transform.position = new Vector3(x, y, -10);
        Camera.GetComponent<Camera>().orthographicSize = size;
    }
}
