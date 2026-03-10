using System;
using System.Drawing;
using System.Windows.Forms;

namespace BarcodeApp
{
    public partial class Form1 : Form
    {
        // Declaração dos componentes
        private TextBox txtInput;
        private Button btnGerar;
        private PictureBox picBarcode;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Configuração básica do Formulário
            this.Text = "Gerador de Código de Barras";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Configuração da Caixa de Texto
            txtInput = new TextBox();
            txtInput.Location = new Point(20, 20);
            txtInput.Size = new Size(240, 20);

            // Configuração do Botão
            btnGerar = new Button();
            btnGerar.Text = "Gerar";
            btnGerar.Location = new Point(270, 18);
            btnGerar.Click += new EventHandler(btnGerar_Click);

            // Configuração do PictureBox (onde o código aparece)
            picBarcode = new PictureBox();
            picBarcode.Location = new Point(20, 60);
            picBarcode.Size = new Size(340, 150);
            picBarcode.BorderStyle = BorderStyle.FixedSingle;
            picBarcode.SizeMode = PictureBoxSizeMode.Zoom;

            // Adicionando os controles ao formulário
            this.Controls.Add(txtInput);
            this.Controls.Add(btnGerar);
            this.Controls.Add(picBarcode);
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            BarcodeGenerator generator = new BarcodeGenerator();
            if (!string.IsNullOrEmpty(txtInput.Text))
            {
                // Gera e exibe a imagem
                picBarcode.Image = generator.Generate(txtInput.Text);
            }
            else
            {
                MessageBox.Show("Por favor, digite algo para gerar o código.");
            }
        }
    }
}