using System.Collections.Generic;
using System.IO;

public static class Util
{

    public static List<Vector3> copiarTrayectoria(List<Vector3> trayectoria)
    {
        List<Vector3> ret = new List<Vector3>();
        foreach (Vector3 it in trayectoria)
            ret.Add(it.clon());
        return ret;
    }

    // Es la distancia del recorrido de toda la trayectoria
    public static float getLongitud(List<Vector3> trayectoria)
    {
        float distancia = 0;
        for (int i = 1; i < trayectoria.Count; i++)
            distancia += Vector3.getDistancia(trayectoria[i - 1], trayectoria[i]);
        return distancia;
    }

    public static void leerAnimacion(string path, out List<Vector3> lista)
    {
        lista = new List<Vector3>();
        StreamReader mySR = new StreamReader(path);
        string line;
        while ((line = mySR.ReadLine()) != null)
        {
            string[] words = line.Split(' ');
            Vector3 v = Vector3.ToVector3(words[0], words[1], words[2]);
            lista.Add(v);
        }
        mySR.Close();
    }

    public static void grabarTrayectoria(string path, out List<Vector3> lista)
    {
        lista = new List<Vector3>();
        StreamReader mySR = new StreamReader(path);
        string line;
        while ((line = mySR.ReadLine()) != null)
        {
            string[] words = line.Split(' ');
            //leo la mano derecha del txt, que es la posicion 8 * 3 (x, y, z)
            Vector3 v = Vector3.ToVector3(words[24], words[24 + 1], words[24 + 2]);
            lista.Add(v);
        }
        mySR.Close();
    }
}
