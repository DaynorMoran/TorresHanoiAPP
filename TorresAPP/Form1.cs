using System.Media;

namespace TorresAPP
{
    public partial class Hanoiform : Form
    {
        
        int cantidaddemovimientos = 0;
        int cantidaddiscos;

        Stack<Label> PilaTorre1 = new Stack<Label>();
        Stack<Label> PilaTorre2 = new Stack<Label>();
        Stack<Label> PilaTorre3 = new Stack<Label>();
        // Reproducir el sonido
        SoundPlayer sonidobloques = new SoundPlayer(Properties.Resources.golpe);
        SoundPlayer sonidoinicio = new SoundPlayer(Properties.Resources.begin);
        SoundPlayer sonidoerror = new SoundPlayer(Properties.Resources.error);

        //Para controlar las pociciones de los discos(labels) en las torres
        int posxT1 = 0;
        int posyT1 = 245;
        int posxT2 = 0;
        int posyT2 = 245;
        int posxT3 = 0;
        int posyT3 = 245;


        Label discoentrante;
        Label ultimodisco;

        
        //Metodo para deshabilitar los botones de movimiento entre torres
        void deshabilitarBotones() {
            btn_t1at2.Enabled = false;
            btn_t1at3.Enabled = false;
            btn_t2at1.Enabled = false;
            btn_t2at3.Enabled = false;
            btn_t3at1.Enabled = false;
            btn_t3at2.Enabled = false;
        }
        //Funcion para saber si es un movimiento valido
        static bool esValido(Label discoentrante, Label discosuperior)
        {
            if (discoentrante.Width>discosuperior.Width)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        void moverDisco(ref Stack<Label> Torreorigen,ref Stack<Label> Torredestino,string nomTdestino){

            //Obtener el disco a mover
            discoentrante = Torreorigen.Peek();
            //Obtener el nombre de la Pila destino(esto para agregar graficamente)
            //Si la torre destino esta vacia agregar disco(Label) directamente
            if (Torredestino.Count()==0)
            {
                // Agregar a PilaTorre
                Torredestino.Push(discoentrante);
                // Eliminar de Pila Torre Origen
                Torreorigen.Pop();

                dibujardisco(nomTdestino);
                sonidobloques.Play();
                cantidaddemovimientos++;
            }                
            else
            {
                //SI no esta vacia
                //Debemos saber si el disco entrante es mas grande que el superior
                //de la Torre destino
                ultimodisco=Torredestino.Peek();

                if (esValido(discoentrante,ultimodisco))
                {
                    //Agregar a Torre
                    // Agregar a PilaTorre2
                    Torredestino.Push(discoentrante);
                    // Eliminar de Pila Torre Origen
                    Torreorigen.Pop();
                    dibujardisco(nomTdestino);
                    sonidobloques.Play();
                    cantidaddemovimientos++;
                }
                else
                {
                    sonidoerror.Play();
                    MessageBox.Show("Movimiento no valido");
                }
            }
        }
        //Metodo para dibujar el disco en la torre
        //Recibe la torre donde se dibujara el disco
        public void dibujardisco(string nombretorredestino)
        {
            
            if (nombretorredestino == "Torre1")
            {
                posyT1 = 0; 
                posxT1 = 0;
                
                posxT1 = discoentrante.Location.X;
                posyT1 = 245 - ((PilaTorre1.Count()*25)-25);
                discoentrante.Location = new Point(posxT1, posyT1);
                // Agregar a Torre2 gráficamente
                pboxT1.Controls.Add(discoentrante);

            }
            if (nombretorredestino == "Torre2")
            {
                posyT2 = 0;
                posxT2 = 0;
                posxT2 = discoentrante.Location.X;
                posyT2 = 245 - ((PilaTorre2.Count() * 25) - 25);
                discoentrante.Location = new Point(posxT2, posyT2);
                // Agregar a Torre3 gráficamente
                pboxT2.Controls.Add(discoentrante);
            }
            if (nombretorredestino == "Torre3")
            {
                posyT3 = 0;
                posxT3 = 0;
                posxT3 = discoentrante.Location.X;
                posyT3 = 245 - ((PilaTorre3.Count() * 25) - 25);
                discoentrante.Location = new Point(posxT3, posyT3);
                // Agregar a Torre3 gráficamente
                pboxT3.Controls.Add(discoentrante);
            }
        }

        bool juegocompletado()
        {
            if (PilaTorre1.Count == 0 && (PilaTorre2.Count == cantidaddiscos || PilaTorre3.Count == cantidaddiscos))
            {                
                return true;
            }
            else
            {
                return false;
            }
        }
        public Hanoiform()
        {
            //Reproduzco sonido de inicio
            sonidoinicio.Play();
            InitializeComponent();  
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private async void btn_nuevo_juego_Click(object sender, EventArgs e)
        {
            
            cantidaddemovimientos = 0;
            //Deshabilito los botones mientras creo una nueva torre
            deshabilitarBotones();
            
            //Deshabilitar el boton mientras se crea la nueva torre
            btn_nuevo_juego.Enabled = false;           
            //Limpiamos las Pilas cada que iniciamos un nuevo Juego
            PilaTorre1.Clear();
            PilaTorre2.Clear();
            PilaTorre3.Clear();
            // Con esta linea limpiamos las torres graficamente
            pboxT1.Controls.Clear();
            pboxT2.Controls.Clear();
            pboxT3.Controls.Clear();
            //Pare resetear las posiciones en las torres
            //Torre1
            posxT1 = 0;
            posyT1 = 245;
            
            //Guardo el valor del NumericUpDown
            cantidaddiscos = (int)spn_cantidad.Value;

            Label label;
                        
            int tamx = panelT1.Width;
            int tamy = 25;
            
            //Las sgtes dos lineas me serviran para los colores
            int rcolor = 0;
            Random ran = new Random();
            // Crear una nueva instancia de Label
            // hasta la cantidad seleccionada en el NumericUpDDown
            for (int i = 1; i <= cantidaddiscos; i++)
            {
                label = new Label();
                label.Name = "label" + i.ToString();
                label.Location = new Point(posxT1, posyT1);
                // Establecer la posición en el formulario (x, y)
                label.Size = new Size(tamx, tamy);
                // Establecer el tamaño del Label (ancho, alto)
                label.Text = string.Format("{0}", i);
                
                label.Font = new Font(label.Font.FontFamily, 8, FontStyle.Bold); // Establecer fuente con tamaño y estilo

                //Dividir entre numero de discos
                tamx -= panelT1.Width / cantidaddiscos;

                posxT1 = (panelT1.Width - tamx) / 2;
                posyT1 = posyT1 - tamy; // Agregar un espaciado entre los labels

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
                    case 2: label.BackColor = Color.DodgerBlue; break;
                    case 3: label.BackColor = Color.Green; break;
                    case 4: label.BackColor = Color.Goldenrod; break;
                    case 5: label.BackColor = Color.Thistle; break;
                    case 6: label.BackColor = Color.Crimson; break;
                    case 7: label.BackColor = Color.Chocolate; break;
                    case 8: label.BackColor = Color.OliveDrab; break;
                    case 9: label.BackColor = Color.Cyan; break;
                    case 10: label.BackColor = Color.Lime; break;
                    
                }
                // Agregar el Label al formulario                
                pboxT1.Controls.Add(label);
                PilaTorre1.Push(label); // Agregar el Label a la pila
                sonidobloques.Play();

                await Task.Delay(150); // Esperar
            }
            //habilitar el boton cuando termina de crear la nueva torre
            btn_nuevo_juego.Enabled = true;

            //habilitar los botones al termino de agregar todos los discos
            //Esto para evitar que se mueva algun disco
            //al momento de generar la torre inicial.
            
            btn_t1at2.Enabled = true;
            btn_t1at3.Enabled = true;
            btn_t2at1.Enabled = true;
            btn_t2at3.Enabled = true;
            btn_t3at1.Enabled = true;
            btn_t3at2.Enabled = true;
            
        }

        
        //****************Eventos de los botones*******************************
        //Para mover un disco de una Torre a otra Torre.
        private void btn_t1at2_Click(object sender, EventArgs e)
        {
            if (PilaTorre1.Count == 0)
            {
                MessageBox.Show("Esta torrre ya esta vacia.");
            }
            else
            {
                moverDisco(ref PilaTorre1, ref PilaTorre2, "Torre2");
                if (juegocompletado())
                {
                    sonidoinicio.Play();
                    MessageBox.Show("¡Felicidades, has completado el juego!\n" +
                        "Cantidad de movimientos: " + cantidaddemovimientos);
                    deshabilitarBotones();
                }
            }
        }

        private void btn_t1at3_Click(object sender, EventArgs e)
        {
            if (PilaTorre1.Count == 0)
            {
                MessageBox.Show("Esta torrre ya esta vacia.");
            }
            else
            {
                moverDisco(ref PilaTorre1, ref PilaTorre3, "Torre3");
                if (juegocompletado())
                {
                    sonidoinicio.Play();
                    MessageBox.Show("¡Felicidades, has completado el juego!\n" +
                        "Cantidad de movimientos: " + cantidaddemovimientos);
                    deshabilitarBotones();
                }
            }
            
        }

        

        private void btn_t2at1_Click(object sender, EventArgs e)
        {
            
            if (PilaTorre2.Count == 0)
            {
                MessageBox.Show("Esta torrre ya esta vacia.");
            }
            else
            {
                moverDisco(ref PilaTorre2, ref PilaTorre1, "Torre1");
                if (juegocompletado())
                {
                    sonidoinicio.Play();
                    MessageBox.Show("¡Felicidades, has completado el juego!\n" +
                        "Cantidad de movimientos: " + cantidaddemovimientos);
                    deshabilitarBotones();
                }
            }
        }

        

        private void btn_t2at3_Click(object sender, EventArgs e)
        {
            if (PilaTorre2.Count == 0)
            {
                MessageBox.Show("Esta torrre ya esta vacia.");
            }
            else
            {
                moverDisco(ref PilaTorre2, ref PilaTorre3, "Torre3");
                if (juegocompletado())
                {
                    sonidoinicio.Play();
                    MessageBox.Show("¡Felicidades, has completado el juego!\n" +
                        "Cantidad de movimientos: " + cantidaddemovimientos);
                    deshabilitarBotones();
                }
            }
        }

        private void btn_t3at1_Click(object sender, EventArgs e)
        {
            if (PilaTorre3.Count == 0)
            {
                MessageBox.Show("Esta torrre ya esta vacia.");
            }
            else
            {
                moverDisco(ref PilaTorre3, ref PilaTorre1, "Torre1");
                if (juegocompletado())
                {
                    sonidoinicio.Play();
                    MessageBox.Show("¡Felicidades, has completado el juego!\n" +
                        "Cantidad de movimientos: " + cantidaddemovimientos);
                    deshabilitarBotones();
                }
            }
        }
        private void btn_t3at2_Click(object sender, EventArgs e)
        {
            if (PilaTorre3.Count == 0)
            {
                MessageBox.Show("Esta torrre ya esta vacia.");
            }
            else
            {
                moverDisco(ref PilaTorre3, ref PilaTorre2, "Torre2");
                if (juegocompletado())
                {
                    sonidoinicio.Play();
                    MessageBox.Show("¡Felicidades, has completado el juego!\n" +
                        "Cantidad de movimientos: " + cantidaddemovimientos);
                    deshabilitarBotones();
                }
            }
        }
        //*********************************************************************

        private void btn_cantidadmovimientos_Click(object sender, EventArgs e)
        {
            if (cantidaddemovimientos == 0)
            {
                MessageBox.Show("Todavia no has hecho ningun movimiento");
            }
            else
            {
                MessageBox.Show("En esta partida has hecho: \n" + cantidaddemovimientos + " movimientos");
            }

        }

        

        private void btn_reglas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reglas básicas de las Torres de Hanoi:\n\n" +
    "1. Hay tres pilares o torres.\n\n" +
    "2. Inicialmente, todos los discos están apilados en uno de los pilares en orden descendente de tamaño, con el disco más grande en la parte inferior.\n\n" +
    "3. El objetivo es mover todos los discos a otro pilar, respetando el orden original de los discos.\n\n" +
    "4. Solo se puede mover un disco a la vez.\n\n" +
    "5. No se puede colocar un disco más grande sobre uno más pequeño.");

        }
        //salir del formulario
        private void btn_salir_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private async void btn_como_Click(object sender, EventArgs e)
        {
            //Deshabilito algunos botones para evitar 
            //La modificacion de la torre1 al momento de su creacion
            btn_nuevo_juego.Enabled = false;
            btn_como.Enabled = false;
            //Limpio las pilas(Stack)
            PilaTorre1.Clear();
            PilaTorre2.Clear();
            PilaTorre3.Clear();
            // Con esta linea limpiamos las torres graficamente
            pboxT1.Controls.Clear();
            pboxT2.Controls.Clear();
            pboxT3.Controls.Clear();
            //Pare resetear las posiciones en las torres
            //Torre1
            posxT1 = 0;
            posyT1 = 245;

            deshabilitarBotones();

            //Guardo el valor del NumericUpDown
            cantidaddiscos = (int)spn_cantidad.Value;

            Label label;

            //calculo el tamaño que tendran los discos
            //usando como referencia el ancho de los paneles del formulario
            int tamx = panelT1.Width;
            //Este sera el alto que tendra cada disco.
            int tamy = 25;

            //Las sgtes dos lineas me serviran para los colores
            int rcolor = 0;
            Random ran = new Random();
            // Crear una nueva instancia de Label
            // hasta la cantidad seleccionada en el NumericUpDDown
            for (int i = 1; i <= 3; i++)
            {
                label = new Label();
                label.Name = "label" + i.ToString();
                label.Location = new Point(posxT1, posyT1);
                // Establecer la posición en el formulario (x, y)
                label.Size = new Size(tamx, tamy);
                // Establecer el tamaño del Label (ancho, alto)
                label.Text = string.Format("{0}", i);

                label.Font = new Font(label.Font.FontFamily, 8, FontStyle.Bold); // Establecer fuente con tamaño y estilo

                //Dividir entre numero de discos
                tamx -= panelT1.Width / cantidaddiscos;

                posxT1 = (panelT1.Width - tamx) / 2;
                posyT1 = posyT1 - tamy; // Agregar un espaciado entre los labels

                //Esta variable es para que los colores no se repitan seguidos
                int anteriorcolor = rcolor;

                rcolor = ran.Next(1, 10);
                do
                {
                    rcolor = ran.Next(1, 11);
                } while (rcolor == anteriorcolor);
                anteriorcolor = rcolor;
                //Aqui le asigno un color respecto al numero random generado
                switch (rcolor)
                {
                    case 1: label.BackColor = Color.Yellow; break;
                    case 2: label.BackColor = Color.DodgerBlue; break;
                    case 3: label.BackColor = Color.Green; break;
                    case 4: label.BackColor = Color.Goldenrod; break;
                    case 5: label.BackColor = Color.Thistle; break;
                    case 6: label.BackColor = Color.Crimson; break;
                    case 7: label.BackColor = Color.Chocolate; break;
                    case 8: label.BackColor = Color.OliveDrab; break;
                    case 9: label.BackColor = Color.Cyan; break;
                    case 10: label.BackColor = Color.Lime; break;

                }
                // Agregar el Label al formulario                
                pboxT1.Controls.Add(label);
                // Agregar el Label a la pila de la Torre 1
                PilaTorre1.Push(label); 
                //Sonidito
                sonidobloques.Play();

                await Task.Delay(250); // Esperar
            }
            await Task.Delay(1000); // Esperar


            /*******************************************************/
            //Muestro ventanas emergentes para poner en contexto al usuario
            MessageBox.Show("Al principo se tiene una torre con todos los discos.", "Mensaje", MessageBoxButtons.OK);
            MessageBox.Show("La idea es Pasar todos los discos de una torre a otra.", "Mensaje", MessageBoxButtons.OK);
            MessageBox.Show("No se puede poner un disco grande encima de uno pequeño.", "Mensaje", MessageBoxButtons.OK);


            moverDisco(ref PilaTorre1, ref PilaTorre2, "Torre2");
            await Task.Delay(560); // Esperar
            moverDisco(ref PilaTorre1, ref PilaTorre3, "Torre3");
            await Task.Delay(600); // Esperar
            moverDisco(ref PilaTorre2, ref PilaTorre3, "Torre3");
            await Task.Delay(600); // Esperar
            moverDisco(ref PilaTorre1, ref PilaTorre2, "Torre2");
            await Task.Delay(600); // Esperar
            moverDisco(ref PilaTorre3, ref PilaTorre1, "Torre1");
            await Task.Delay(600); // Esperar
            moverDisco(ref PilaTorre3, ref PilaTorre2, "Torre2");
            await Task.Delay(600); // Esperar
            moverDisco(ref PilaTorre1, ref PilaTorre2, "Torre2");
            await Task.Delay(600); // Esperar

            btn_como.Enabled = true;
            btn_nuevo_juego.Enabled = true;

            sonidoinicio.Play();
            MessageBox.Show("¡Asi es como se juega...!!!\n" +
                "Cantidad de movimientos: " + cantidaddemovimientos);
            deshabilitarBotones();
        }
    }
}