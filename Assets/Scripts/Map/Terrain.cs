using UnityEngine;

public class Terrain : MonoBehaviour
{

    [SerializeField] private GameObject _brique;
    [SerializeField] private GameObject _brique2;
    [SerializeField] private int _longueur;
    [SerializeField] private int _largeur;
    [SerializeField] private TerrainData _terrain;

    //Tableau 2 dimensions / Tableau dans un tableau / Matrix
    private float[,] _theWorld;
    void Start()
    {
        init();
        genererLaby();
        //afficherLaby();
        _terrain.SetHeights(0, 0, _theWorld);
    }


    public void init()
    {
        //Louer la mémoire, connaître la dimension de la matrix
        _theWorld = new float[_longueur, _largeur];
    }

    public void genererLaby()
    {
        //Parcourir les 2 dimensions du tableau (faire toutes la longueur (0,1 ; 0,2 ..) puis revenir et augmenter (1,1 ; 1,2 ..))
        //Fait la première boucle puis reste dans la deuxième boucle jusqu'à la condition puis retourne a la première
        for (int i = 0; i < _longueur; i++)
        {
            for (int j = 0; j < _largeur; j++)
            {
                float randx = Random.Range(-10000, 10000);
                float randy = Random.Range(-10000, 10000);
                float die = Mathf.PerlinNoise(i*0.01f, j*0.01f);
                die *= Mathf.PerlinNoise(i * 0.02f, j * 0.02f);
                die *= Mathf.PerlinNoise(i * 0.03f, j * 0.03f);
                die *= Mathf.PerlinNoise(i * 0.04f, j * 0.04f);
                die *= Mathf.PerlinNoise(i + randx, j + randy);
                _theWorld[i, j] = die * 4 ;
            }
        }
    }
    
    public void afficherLaby()
    {
        for (int i = 0; i < _longueur; i++)
        {
            for (int j = 0; j < _largeur; j++)
            {
                //J'affiche si 0 ou 1
                if (_theWorld[i, j] == 0)
                {

                }
                if (_theWorld[i, j] == 1)
                {
                    /* Changement en Y
                    int posY = Random.Range(0, 2);
                    Vector3 pos = new Vector3(i, posY, j);                    
                    Instantiate(_brique, pos, Quaternion.identity);
                    /*/
                    float y = Mathf.PerlinNoise(i / 5f, j / 5f) * 4;
                    Vector3 pos = new Vector3(i * 4, y, j * 4);
                    GameObject briqueGo = Instantiate(_brique, pos, Quaternion.identity);
                    briqueGo.transform.parent = transform.parent;
                    briqueGo.name = "Case" + i + " " + j;
                    //*/
                }
                //Mur
                if (_theWorld[i, j] == 3)
                {
                    Vector3 pos = new Vector3(i, 0, j);
                    Instantiate(_brique2, pos, Quaternion.identity);
                }
            }
        }
    }
    

}
