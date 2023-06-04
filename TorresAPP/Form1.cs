namespace TorresAPP
{
    public partial class Hanoiform : Form
    {
        Stack<Label> Torre1 = new Stack<Label>();
        public Hanoiform()
        {
            InitializeComponent();

            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_nuevo_juego_Click(object sender, EventArgs e)
        {
            //Limpiamos la primera pila correspondiente a torre1
            Torre1.Clear();
            // COn esta linea limpiamos las torres
            pboxT1.Controls.Clear();

            int nuevo = (int)spn_cantidad.Value;
            Label label;
            int rcolor = 0;
            int tamx = panelT1.Width;
            int tamy = 25;
            int posx = (panelT1.Width - tamx) / 2;
            int posy = 245;

            Random ran = new Random();
            // Crear una nueva instancia de Label
            for (int i = 1; i <= nuevo; i++)
            {
                label = new Label();
                label.Name = "label" + i.ToString();
                label.Location = new Point(posx, posy);
                // Establecer la posición en el formulario (x, y)
                label.Size = new Size(tamx, tamy);
                // Establecer el tamaño del Label (ancho, alto)
                label.Text = string.Format("[{0}]", i);
                //label.Font = new Font(label.Font, FontStyle.Bold); // Establecer estilo de fuente en negrita
                label.Font = new Font(label.Font.FontFamily, 5, FontStyle.Bold); // Establecer fuente con tamaño y estilo



                tamx -= panelT1.Width / 10;//Dividir entre numero de discos

                posx = (panelT1.Width - tamx) / 2;//posx = posx - 10;
                posy = posy - tamy; // Agregar un espaciado entre los labels

                int anteriorcolor = rcolor;
                rcolor = ran.Next(1, 10);
                do
                {
                    rcolor = ran.Next(1, 11);
                } while (rcolor == anteriorcolor);
                anteriorcolor = rcolor;
                switch (rcolor)
                {
                    case 1: label.BackColor = Color.Yellow; break;
                    case 2: label.BackColor = Color.Blue; break;
                    case 3: label.BackColor = Color.Green; break;
                    case 4: label.BackColor = Color.Purple; break;
                    case 5: label.BackColor = Color.Thistle; break;
                    case 6: label.BackColor = Color.Crimson; break;
                    case 7: label.BackColor = Color.Chocolate; break;
                    case 8: label.BackColor = Color.SpringGreen; break;
                    case 9: label.BackColor = Color.Cyan; break;
                    case 10: label.BackColor = Color.Sienna; break;
                    default:
                        rcolor = 0;
                        break;
                }
                // Agregar el Label al formulario                
                pboxT1.Controls.Add(label);
                Torre1.Push(label); // Agregar el Label a la pila



            }

        }
    }
}