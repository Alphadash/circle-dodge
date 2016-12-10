using UnityEngine;

public class Lasers : MonoBehaviour
{
    public Material LaserMaterial, BeamMaterial;

    public Player Player;

    public float LaserFireDelay, LaserSpawnDelay;
    
    private Laser[][] ActiveLasers;
    private float NextLaser;

    private const int SidesSpawningLasers = 4;

    // To avoid multiple local variables being created when CreateLaser lands on an occupied slot
    private int CreateRow, CreatePos;

    void Start()
    {
        ActiveLasers = new Laser[SidesSpawningLasers][];
        for (int i = 0; i < SidesSpawningLasers; i++) ActiveLasers[i] = new Laser[GenerateGrid.GridSize];

        LaserFireDelay = 2f;
        LaserSpawnDelay = (LaserFireDelay + 0.1f) / (GenerateGrid.GridSize - 1);
        NextLaser = LaserSpawnDelay;
    }
    
    void FixedUpdate()
    {
        if (Player.IsAlive)
        {
            NextLaser -= Time.deltaTime;

            if (NextLaser <= 0f)
            {
                NextLaser = LaserSpawnDelay;
                CreateLaser();
            }

            for (int row = 0; row < SidesSpawningLasers; row++)
            {
                for (int pos = 0; pos < GenerateGrid.GridSize; pos++)
                {
                    if (ActiveLasers[row][pos] != null)
                    {
                        ActiveLasers[row][pos].Update(Time.deltaTime);

                        if (ActiveLasers[row][pos].TimeLeft <= 0f)
                        {
                            Player.CheckForDeath(row, pos);

                            if (ActiveLasers[row][pos].TimeLeft <= -0.1f)
                            {
                                ActiveLasers[row][pos].Remove();
                                ActiveLasers[row][pos] = null;
                            }
                        }
                    }
                }
            }
        }
    }

    private void CreateLaser()
    {
        CreateRow = Random.Range(0, SidesSpawningLasers);
        CreatePos = Random.Range(0, GenerateGrid.GridSize);

        if (ActiveLasers[CreateRow][CreatePos] != null) CreateLaser(); // Try again if slot is occupied
        else ActiveLasers[CreateRow][CreatePos] = new Laser(LaserMaterial, BeamMaterial, LaserFireDelay, CreateRow, CreatePos);

    }

    private class Laser
    {
        public float TimeLeft;

        private float TimeToFire;
        private GameObject LaserQuad, BeamQuad;

        public Laser(Material LaserMaterial, Material BeamMaterial, float timeToFire, int row, int pos)
        {
            TimeToFire = TimeLeft = timeToFire;

            LaserQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            LaserQuad.GetComponent<Renderer>().material = LaserMaterial;

            BeamQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            BeamQuad.GetComponent<Renderer>().material = BeamMaterial;
            BeamQuad.transform.parent = LaserQuad.transform;
            BeamQuad.transform.position = new Vector3(0, 0, Gameplay.BeamZPosition);
            BeamQuad.transform.localScale = new Vector3(0.1f, 1, 1);

            if (row == 0)
            {
                LaserQuad.transform.position = new Vector3(-1, pos, Gameplay.LaserZPosition);
                BeamQuad.transform.Rotate(0, 0, 180);
            }
            else if (row == 1)
            {
                LaserQuad.transform.position = new Vector3(pos, -1, Gameplay.LaserZPosition);
                BeamQuad.transform.Rotate(0, 0, 270);
            }
            else if (row == 2)
            {
                LaserQuad.transform.position = new Vector3(GenerateGrid.GridSize, pos, Gameplay.LaserZPosition);
            }
            else
            {
                LaserQuad.transform.position = new Vector3(pos, GenerateGrid.GridSize, Gameplay.LaserZPosition);
                BeamQuad.transform.Rotate(0, 0, 90);
            }
        }

        public void Update(float timePassed)
        {
            TimeLeft -= timePassed;

            if (TimeLeft > 0f) BeamQuad.transform.localScale = new Vector3(1.1f - (TimeLeft / TimeToFire), 1, 1);
            else BeamQuad.transform.localScale = new Vector3(8 + ((GenerateGrid.GridSize - 3) * 2), 1, 1);
        }

        public void Remove()
        {
            Destroy(LaserQuad);
        }
    }
}
