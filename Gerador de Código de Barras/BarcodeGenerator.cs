using System.Collections.Generic;
using System.Drawing;

public class BarcodeGenerator
{
    private readonly Dictionary<char, string> _code39Alphabet = new Dictionary<char, string>
    {
        {'0', "000110100"}, {'1', "100100001"}, {'2', "001100001"}, {'3', "101100000"},
        {'4', "000110001"}, {'5', "100110000"}, {'6', "001110000"}, {'7', "000100101"},
        {'8', "100100100"}, {'9', "001100100"}, {'A', "100001001"}, {'B', "001001001"},
        {'C', "101001000"}, {'D', "000011001"}, {'E', "100011000"}, {'F', "001011000"},
        {' ', "011000100"}, {'*', "010010100"} 
    };

    public Bitmap Generate(string text)
    {
        string fullText = $"*{text.ToUpper()}*";
        int width = fullText.Length * 25; 
        Bitmap bitmap = new Bitmap(width, 120); // Aumentei um pouco a altura (de 100 para 120)
        
        using (Graphics g = Graphics.FromImage(bitmap))
        {
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // Melhora a qualidade do texto
            
            float xPos = 10;

            foreach (char c in fullText)
            {
                if (!_code39Alphabet.ContainsKey(c)) continue;

                foreach (char bit in _code39Alphabet[c])
                {
                    float barWidth = (bit == '1') ? 3 : 1;
                    g.FillRectangle(Brushes.Black, xPos, 10, barWidth, 60);
                    xPos += barWidth + 1;
                }
                xPos += 2; 
            }

            // --- NOVIDADE: Desenhando o texto abaixo das barras ---
            using (Font font = new Font("Arial", 10, FontStyle.Bold))
            {
                // Calcula o centro para o texto
                SizeF textSize = g.MeasureString(text.ToUpper(), font);
                float textX = (width - textSize.Width) / 2;
                float textY = 75; // Posicionado logo abaixo das barras (que terminam em 70)

                g.DrawString(text.ToUpper(), font, Brushes.Black, textX, textY);
            }
        }
        return bitmap;
    }
}