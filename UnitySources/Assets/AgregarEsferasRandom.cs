using UnityEngine;
using System.Collections.Generic;

public class AgregarEsferasRandom : MonoBehaviour {

    private List<Vector3> lista;
    public int cantidadAAgregar = 0;
    public bool agregar = false;

    private List<GameObject> esferasDibujadas;

	// Use this for initialization
    void Start()
    {
        Util.leerAnimacion("Assets\\trajectory.txt", out lista);
        esferasDibujadas = new List<GameObject>();
        dibujar(lista, new Color(0f, 0f, 0.3f));
    }

	// Update is called once per frame
	void Update () {
        if (agregar)
        {
            //Elimino las agregadas anteriormente
            int cant = esferasDibujadas.Count;
            for (int i = 0; i < cant; i++)
            {
                GameObject eliminar = esferasDibujadas[0];
                esferasDibujadas.Remove(eliminar);
                Destroy(eliminar);
            }
            
            //Cargo las nuevas
            List<Vector3> nuevaLista = agregarPuntos(lista, cantidadAAgregar);
            dibujar(nuevaLista, new Color(0.3f, 0f, 0f));
            dibujar(lista, new Color(0f, 0f, 0.3f));
            agregar = false;
        }
	}

    private void dibujar(List<Vector3> esferasADibujar, Color c)
    {
        for (int i = 0; i < esferasADibujar.Count; i++)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = new UnityEngine.Vector3(0.05f, 0.05f, 0.05f);
            Vector3 v = esferasADibujar[i];
            sphere.transform.position = new UnityEngine.Vector3(v.x, v.y, v.z);
            sphere.GetComponent<Renderer>().material.color = c;
            esferasDibujadas.Add(sphere);
        }
    }

    //agrega n cantidad de puntos a una lista en posiciones aleatorias
    public static List<Vector3> agregarPuntos(List<Vector3> lista, int n)
    {
        List<Vector3> ret = new List<Vector3>();
        List<Vector3> listaCopia = Util.copiarTrayectoria(lista);
        System.Random r = new System.Random();
        for (int punto = 0; punto < n; punto++)
        {
            int posicion = r.Next(listaCopia.Count - 1);
            ret.Add(agregarPunto(listaCopia, posicion, posicion + 1));
        }
        return ret;
    }

    private static Vector3 agregarPunto(List<Vector3> lista, int anterior, int siguiente)
    {
        Vector3 va = lista[anterior];
        Vector3 vs = lista[siguiente];
        Vector3 desplazamiento = (vs - va) / 2;
        desplazamiento = va + desplazamiento;
        lista.Insert(siguiente, desplazamiento);
        return desplazamiento;
    }

}
