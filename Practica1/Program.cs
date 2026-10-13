namespace Practica1
{
    internal class Program
    {
        const int ANCHO = 71, ALTO = 20,  // ancho y alto del área de juego (sin contar la parte de puntuación)              
              LONG_PALA = 8, LEFT = 0, TOP = 0;         // longitud de las palas

        static Random rnd = new Random(); // para aleatorizar la dirección de la pelota
        static void Main()
        {
            Console.CursorVisible = false; // para no dibujar el cursor en pantalla

            int pala1Y, pala2Y,    // posición vertical de las palas
                pelotaX, pelotaY,  // posición de la pelota
                dirX, dirY,       // dirección de la pelota
                puntos1, puntos2;  // puntos de cada jugador


            // Inicialización de las variables de estado del juego
            pala1Y = (ALTO - LONG_PALA) / 2;
            pala2Y = (ALTO - LONG_PALA) / 2;

            pelotaX = ANCHO / 2 + 1;    //Posicion de la pelota (inicio)
            pelotaY = ALTO / 2;             
            dirX = -1;                   //Direccion de la pelota (inicio)
            dirY = 1;

            puntos1 = 0;    //Puntuaciones
            puntos2 = 0;

            bool salir = false;
            while (!salir)
            {

                // LEER TECLADO
                if (Console.KeyAvailable)
                { // ver si se ha pulsado alguna tecla
                    ConsoleKeyInfo tecla = Console.ReadKey(true); // recoger pulsación de tecla
                                                                  // tecla puede ser: 
                                                                  // ConsoleKey.W, ConsoleKey.S, ConsoleKey.I, ConsoleKey.K: movimiento palas
                                                                  // ConsoleKey.Q, ConsoleKey.P:  salir, pausar

                }

                // MOVIMIENTO PELOTA

                if (((pelotaY == 1) && (dirY == -1)) || ((pelotaY == (ALTO - 1)) && (dirY == 1))) 
                {                                                 //Si la pelota llega a alguno de los bordes con intencion de chocar
                    dirY = -dirY;
                }

                if ((pelotaX == 0) || (pelotaX == (ANCHO)))
                {                                                 //Si llega a alguna de las "porterias"
                    if (pelotaX == 0) puntos2++;
                    if (pelotaX == ANCHO) puntos1++;
                    pelotaX = ANCHO / 2 + 1;
                    pelotaY = ALTO / 2;
                    dirX = 1;
                    dirY = 1;
                }
                if (pelotaX == 2)
                {                                                 //Si el jugador llega al jugador 1 (x = 2)
                    for (int j = 0; j < LONG_PALA; j++)
                    {
                        if (pelotaY == pala1Y + j) dirX = -dirX;
                    }
                }
                    
                if (pelotaX == ANCHO - 3)
                {                                                 //Si el jugador llega al jugador 2 (ANCHO - 3)
                    for (int j = 0; j < LONG_PALA; j++)
                    {
                        if (pelotaY == pala2Y + j) dirX = -dirX;
                    }
                }

                pelotaX = pelotaX + dirX;
                pelotaY = pelotaY + dirY;

                // COLISIÓNES: MUROS SUPERIOR E INFERIOR, SALIDA DEL ÁREA DE JUEGO (PUNTO JUGADOR), PALAS 

                // RENDERIZADO

                Console.Clear();
                
                Console.BackgroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(LEFT, TOP);   // Muro superior
                for (int i = 0; i < ANCHO; i++)
                {                                       // Comienza en i (0) y avanza hasta el ANCHO (71), [0,70]
                    Console.SetCursorPosition(i, TOP);
                    Console.Write(" ");
                }
                Console.ResetColor();

                
                Console.BackgroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(LEFT, ALTO);  //Muro inferior
                for (int i = 0; i < ANCHO; i++)
                {                                       // Comienza en i (0) y avanza hasta el ANCHO (71), [0,70]
                    Console.SetCursorPosition(i, ALTO);
                    Console.Write(" ");
                }
                Console.ResetColor();


                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(ANCHO/2, (1));        //Red
                for (int i = 0; i < (ALTO - 1); i++)
                {                                               // Se coloca en la mitad (p35)
                    Console.SetCursorPosition(ANCHO/2, ( 1 + i));
                    Console.Write("|");
                }
                Console.ResetColor();

                Console.ResetColor();                           //Puntos de caad jugador
                Console.SetCursorPosition(1,ALTO + 2);          
                Console.Write("Jugador 1 : " + puntos1 + "  Jugador 2: " + puntos2 + "  (Q para salir)");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition( 1, pala1Y);          //Jugador 1
                for (int i = 0; i < LONG_PALA; i++)
                {                                               //Se coloca en el medio ()
                    Console.SetCursorPosition( 1, pala1Y + i);
                    Console.Write("|");
                }
                Console.ResetColor();


                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(ANCHO - 2 , pala2Y);
                for (int i = 0; i < LONG_PALA; i++)                 //Jugador 2
                { 
                    Console.SetCursorPosition(ANCHO - 2, pala2Y + i);
                    Console.Write("|");
                }
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(pelotaX, pelotaY);       //Pelota
                Console.Write("O");
                Console.ResetColor();


                // dibujo red central, pelota, palas, puntos jugadores

                // Retardo
                System.Threading.Thread.Sleep(90);

                // borrado de la pelota y palas para el siguiente frame

            }
        }
    }
}
