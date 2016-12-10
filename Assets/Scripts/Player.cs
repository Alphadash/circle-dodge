using UnityEngine;

public class Player : MonoBehaviour
{
    public Material PlayerMaterial;

    public Gameplay Gameplay;
    public static bool IsAlive;

    private GameObject PlayerQuad;

    void Start()
    {
        PlayerQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        PlayerQuad.GetComponent<Renderer>().material = PlayerMaterial;
        float middle = Mathf.Floor(GenerateGrid.GridSize / 2);
        PlayerQuad.transform.position = new Vector3(middle, middle, Gameplay.PlayerZPosition);

        IsAlive = true;
    }
	
	void Update()
    {
        if (IsAlive)
        {
            if (MovingLeft() && PlayerQuad.transform.position.x > 0) Move(-1, 0);
            else if (MovingRight() && PlayerQuad.transform.position.x < GenerateGrid.GridSize - 1) Move(1, 0);
            else if (MovingDown() && PlayerQuad.transform.position.y > 0) Move(0, -1);
            else if (MovingUp() && PlayerQuad.transform.position.y < GenerateGrid.GridSize - 1) Move(0, 1);
        }
    }

    public void CheckForDeath(int row, int pos)
    {
        if (((row == 0 || row == 2) && PlayerQuad.transform.position.y == pos) || ((row == 1 || row == 3) && PlayerQuad.transform.position.x == pos))
        {
            IsAlive = false;
            Gameplay.GameOver();
        }
    }

    private void Move(int x, int y)
    {
        PlayerQuad.transform.position = new Vector3(PlayerQuad.transform.position.x + x, PlayerQuad.transform.position.y + y, Gameplay.PlayerZPosition);
    }

    private bool MovingLeft()
    {
        return Input.GetButtonDown("Move Left");
    }

    private bool MovingRight()
    {
        return Input.GetButtonDown("Move Right");
    }

    private bool MovingUp()
    {
        return Input.GetButtonDown("Move Up");
    }

    private bool MovingDown()
    {
        return Input.GetButtonDown("Move Down");
    }
}
