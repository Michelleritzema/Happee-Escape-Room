using UnityEngine;
using System.Collections;

public class LetterChanger : MonoBehaviour {

    public static Texture[] letters = new Texture[27];
    public Texture blank, A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z;
    public GameObject letterPanel;
    private int currentInteger = 1;
    private bool updatable = false;

	// Use this for initialization
	void Start () {
        letters[0] = blank;
        letters[1] = A;
        letters[2] = B;
        letters[3] = C;
        letters[4] = D;
        letters[5] = E;
        letters[6] = F;
        letters[7] = G;
        letters[8] = H;
        letters[9] = I;
        letters[10] = J;
        letters[11] = K;
        letters[12] = L;
        letters[13] = M;
        letters[14] = N;
        letters[15] = O;
        letters[16] = P;
        letters[17] = Q;
        letters[18] = R;
        letters[19] = S;
        letters[20] = T;
        letters[21] = U;
        letters[22] = V;
        letters[23] = W;
        letters[24] = X;
        letters[25] = Y;
        letters[26] = Z;
        updatable = true;
    }

    void Update()
    {
        if(updatable)
        {
            SetImage(currentInteger);
            updatable = false;
        }
    }

    public void InitializeImage(int value)
    {
        currentInteger = value;
        //SetImage(value);
    }

    public void SetImage(int value)
    {
        letterPanel.GetComponent<Renderer>().material.mainTexture = letters[value];
    }

    public void moveUp()
    {
        setCorrectInteger(true);
        SetImage(currentInteger);
    }

    public void moveDown()
    {
        setCorrectInteger(false);
        SetImage(currentInteger);
    }

    public int GetCurrentLetter()
    {
        return currentInteger;
    }

    private void setCorrectInteger(bool addition)
    {
        if(!currentInteger.Equals(0))
        {
            if (addition)
            {
                if (currentInteger.Equals(26))
                    currentInteger = 1;
                else
                    currentInteger++;
            }
            else
            {
                if (currentInteger.Equals(1))
                    currentInteger = 26;
                else
                    currentInteger--;
            }
        }
    }

}
