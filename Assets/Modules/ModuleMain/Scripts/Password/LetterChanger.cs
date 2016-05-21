using UnityEngine;
using System.Collections;

/*
 * Created by Michelle Ritzema.
 * 
 * Class that handles the actions of a password panel letter.
 * Each panel can show any letter of the alphabet, unless it is locked at the start.
 */

public class LetterChanger : MonoBehaviour {

    private bool initialized = false;
    private int currentInteger = 1;

    public GameObject letterPanel;
    public Texture[] letters = new Texture[27];
    public Texture blank, A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z;

	/*
     * Fills the letters array with all the possible letter textures.
     * Also sets the initialized boolean to true so that the update function will be called.
     */
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
        initialized = false;
    }

    /*
     * Updates the panel to the correct letter, but only one time.
     */
    void Update()
    {
        if(!initialized)
        {
            SetTexture(currentInteger);
            initialized = true;
        }
    }

    /*
     * Initializes the letter panel during the start of the game.
     * It is either set to 0 or 1, depending on the length of the stored password.
     */
    public void InitializePanel(int value)
    {
        currentInteger = value;
    }

    /*
     * Sets the letter panel's texture to the texture that is stored under the supplied index.
     */
    public void SetTexture(int value)
    {
        letterPanel.GetComponent<Renderer>().material.mainTexture = letters[value];
    }

    /*
     * Updates the current letter indicator to the next value. This integer is dependent on whether the method 
     * is called as an addition or a subtraction. If the indicator has reached the end of the letter array, 
     * it starts at the beginning, and vice versa. If the current letter indicator is set to 0, nothing is changed.
     */
    private void setCorrectInteger(bool addition)
    {
        if (!currentInteger.Equals(0))
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

    /*
     * Moves the letter panel up by one step and updates the texture.
     */
    public void MoveUp()
    {
        setCorrectInteger(true);
        SetTexture(currentInteger);
    }

    /*
     * Moves the letter panel down by one step and updates the texture.
     */
    public void MoveDown()
    {
        setCorrectInteger(false);
        SetTexture(currentInteger);
    }

    /*
     * Fetches the current letter indicator.
     */
    public int GetCurrentLetter()
    {
        return currentInteger;
    }

}