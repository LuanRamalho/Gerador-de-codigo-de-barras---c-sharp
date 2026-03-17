using System;
using System.Drawing;
using System.Windows.Forms;

namespace BarcodeGeneratorApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    public class MainForm : Form
    {
        private TextBox txtInput;
        private Panel canvas;
        private Button btnGenerate;
        private Label lblInstructions;

        public MainForm()
        {
            this.Text = "Gerador de Código de Barras";
            this.Size = new Size(1000, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblInstructions = new Label() { Text = "Digite números ou letras:", Location = new Point(20, 20), Width = 200 };
            txtInput = new TextBox() { Location = new Point(20, 45), Width = 400, Font = new Font("Arial", 12) };
            btnGenerate = new Button() { Text = "Gerar", Location = new Point(430, 44), Height = 30, Width = 100 };
            
            canvas = new Panel() { 
                Location = new Point(20, 100), 
                Size = new Size(940, 220), 
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle 
            };

            // Eventos
            btnGenerate.Click += (s, e) => canvas.Refresh();
            canvas.Paint += Canvas_Paint;

            this.Controls.Add(lblInstructions);
            this.Controls.Add(txtInput);
            this.Controls.Add(btnGenerate);
            this.Controls.Add(canvas);
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            string text = txtInput.Text;
            if (string.IsNullOrWhiteSpace(text)) return;

            DrawCode128(e.Graphics, text);
        }

        private void DrawCode128(Graphics g, string data)
        {
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int barHeight = 100;
            int barWidth = 2; 
            
            // Usamos o HashCode para gerar um padrão visual consistente para o exemplo
            // mas limitamos a largura para não estourar o painel
            Random rand = new Random(data.GetHashCode());
            int totalBars = Math.Min(data.Length * 8, (canvas.Width / barWidth) - 20);
            
            float totalCodeWidth = totalBars * barWidth;
            
            // Centralização calculada com base na largura atual do Canvas
            float startX = (canvas.Width - totalCodeWidth) / 2;
            float currentX = startX;
            float y = 30;

            // Desenha as barras
            for (int i = 0; i < totalBars; i++)
            {
                if (rand.Next(0, 2) == 1) // Simulação de barras pretas/brancas
                {
                    g.FillRectangle(Brushes.Black, currentX, y, barWidth, barHeight);
                }
                currentX += barWidth;
            }

            // Desenha o texto centralizado abaixo do código
            Font font = new Font("Consolas", 12, FontStyle.Bold);
            SizeF textSize = g.MeasureString(data, font);
            
            // O segredo aqui: centralizar o texto com base no centro do Canvas, não no X final
            float textX = (canvas.Width - textSize.Width) / 2;
            float textY = y + barHeight + 15;

            // Verifica se o texto é muito grande para o painel e ajusta se necessário
            if (textX < 5) textX = 5; 

            g.DrawString(data, font, Brushes.Black, textX, textY);
        }
    }
}