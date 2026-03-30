using UnityEngine;

public class TextReveal
{
    string Text;
    int revealIndex = 0;
    float CharSpeed;
    float currentTime;
    public TextReveal(string text, float charSpeed)
    {
        Text = text;
        CharSpeed = charSpeed;
    }

    public void Update()
    {
        //add timee
        currentTime += Time.deltaTime;
        if (currentTime >= CharSpeed)// if timer goes off
        {
            //check if reached the end
            if (Text.Length > revealIndex)
            {
                revealIndex++;
                currentTime = 0;
            }
        }
    }

    public bool IsTextRevealed()
    {
        return Text.Length-1 <= revealIndex;       
    }

    public string GetTextReveal()
    {
        return Text.Substring(0, revealIndex + 1);
    }
}
