using System;
using UnityEngine;

public class Noise
{
    #region Values

    static readonly int[] Source = {
        151, 160, 137, 91, 90, 15, 131, 13, 201, 95, 96, 53, 194, 233, 7, 225, 140, 36, 103, 30, 69, 142,
        8, 99, 37, 240, 21, 10, 23, 190, 6, 148, 247, 120, 234, 75, 0, 26, 197, 62, 94, 252, 219, 203,
        117, 35, 11, 32, 57, 177, 33, 88, 237, 149, 56, 87, 174, 20, 125, 136, 171, 168, 68, 175, 74, 165,
        71, 134, 139, 48, 27, 166, 77, 146, 158, 231, 83, 111, 229, 122, 60, 211, 133, 230, 220, 105, 92, 41
    };

    const int RandomSize = 256;
    int[] _random;

    static readonly Vector3[] Gradients = {
        new Vector3(1,1,0), new Vector3(-1,1,0), new Vector3(1,-1,0), new Vector3(-1,-1,0),
        new Vector3(1,0,1), new Vector3(-1,0,1), new Vector3(1,0,-1), new Vector3(-1,0,-1),
        new Vector3(0,1,1), new Vector3(0,-1,1), new Vector3(0,1,-1), new Vector3(0,-1,-1)
    };

    #endregion

    public Noise()
    {
        Randomize(0);
    }

    public Noise(int seed)
    {
        Randomize(seed);
    }

    public float Evaluate(Vector3 point)
    {
        int xi = FastFloor(point.x);
        int yi = FastFloor(point.y);
        int zi = FastFloor(point.z);

        float xf = point.x - xi;
        float yf = point.y - yi;
        float zf = point.z - zi;

        float u = Fade(xf);
        float v = Fade(yf);
        float w = Fade(zf);

        int aaa = Hash(xi, yi, zi);
        int aba = Hash(xi, yi + 1, zi);
        int aab = Hash(xi, yi, zi + 1);
        int abb = Hash(xi, yi + 1, zi + 1);
        int baa = Hash(xi + 1, yi, zi);
        int bba = Hash(xi + 1, yi + 1, zi);
        int bab = Hash(xi + 1, yi, zi + 1);
        int bbb = Hash(xi + 1, yi + 1, zi + 1);

        float x1, x2, y1, y2;
        x1 = Mathf.Lerp(Dot(Gradients[aaa], xf, yf, zf),
                        Dot(Gradients[baa], xf - 1, yf, zf), u);
        x2 = Mathf.Lerp(Dot(Gradients[aba], xf, yf - 1, zf),
                        Dot(Gradients[bba], xf - 1, yf - 1, zf), u);
        y1 = Mathf.Lerp(x1, x2, v);

        x1 = Mathf.Lerp(Dot(Gradients[aab], xf, yf, zf - 1),
                        Dot(Gradients[bab], xf - 1, yf, zf - 1), u);
        x2 = Mathf.Lerp(Dot(Gradients[abb], xf, yf - 1, zf - 1),
                        Dot(Gradients[bbb], xf - 1, yf - 1, zf - 1), u);
        y2 = Mathf.Lerp(x1, x2, v);

        return Mathf.Lerp(y1, y2, w);
    }

    void Randomize(int seed)
    {
        _random = new int[RandomSize * 2];

        for (int i = 0; i < RandomSize; i++)
        {
            _random[i] = Source[i % Source.Length];
            _random[i + RandomSize] = _random[i];
        }
    }

    int Hash(int x, int y, int z)
    {
        // 确保索引始终在 [0, 255] 内，防止超出 _random 数组范围
        int xi = (x % 256 + 256) % 256;
        int yi = (y % 256 + 256) % 256;
        int zi = (z % 256 + 256) % 256;

        return _random[xi + _random[yi + _random[zi]]] % Gradients.Length;
    }

    static int FastFloor(float x)
    {
        return x > 0 ? (int)x : (int)x - 1;
    }

    static float Fade(float t)
    {
        return t * t * t * (t * (t * 6 - 15) + 10);
    }

    static float Dot(Vector3 g, float x, float y, float z)
    {
        return g.x * x + g.y * y + g.z * z;
    }
}
