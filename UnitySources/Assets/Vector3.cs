using System;

public class Vector3
{
    public float x = 0;
    public float y = 0;
    public float z = 0;

    public static Vector3 Zero
    {
        get { return new Vector3(); }
    }

    public Vector3()
    {
        this.set(0, 0, 0);
    }

    public Vector3(float x, float y, float z)
    {
        this.set(x, y, z);
    }

    public Vector3(Vector3 v)
    {
        this.set(v.x, v.y, v.z);
    }

    public Vector3(string xyz)
    {
        xyz = xyz.Replace('(', ' ').Replace(')', ' ');
        string[] trim = xyz.Split(',');

        this.x = System.Convert.ToSingle(trim[0]);
        this.y = System.Convert.ToSingle(trim[1]);
        this.z = System.Convert.ToSingle(trim[2]);
    }

    private void set(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public float getModulo()
    {
        return (float)Math.Sqrt(x * x + y * y + z * z);
    }

    public Vector3 normalizar()
    {
        float modulo = getModulo();
        return this.clon() / modulo;
    }
    public float productoEscalar(Vector3 v)
    {
        return x * v.x + y * v.y + z * v.z;
    }

    public Vector3 productoCruzado(Vector3 v)
    {
        float i = y * v.z - v.y * z;
        float j = x * v.z - v.x * z;
        float k = x * v.y - v.x * y;
        return new Vector3(i, -j, k);
    }

    public override string ToString()
    {
        return "(" + x + ", " + y + ", " + z + ")";
    }

    public Vector3 clon()
    {
        return new Vector3(x, y, z);
    }

    public bool igual(Vector3 v)
    {
        return ((x == v.x) && (y == v.y) && (z == v.z));
    }

    #region Sobrecarga de operadores
    public static Vector3 operator +(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
    }

    public static Vector3 operator -(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
    }

    public static Vector3 operator /(Vector3 v1, float f)
    {
        return new Vector3(v1.x / f, v1.y / f, v1.z / f);
    }

    public static Vector3 operator *(Vector3 v1, float f)
    {
        return new Vector3(v1.x * f, v1.y * f, v1.z * f);
    }
    #endregion

    public static Vector3 ToVector3(string x, string y, string z)
    {
        return new Vector3(System.Convert.ToSingle(x), System.Convert.ToSingle(y), System.Convert.ToSingle(z));
    }

    public static float getDistancia(Vector3 v1, Vector3 v2)
    {
        float determinante = (v1.x - v2.x) * (v1.x - v2.x) + (v1.y - v2.y)
                * (v1.y - v2.y) + (v1.z - v2.z) * (v1.z - v2.z);
        return (float)Math.Sqrt(determinante);
    }

}
