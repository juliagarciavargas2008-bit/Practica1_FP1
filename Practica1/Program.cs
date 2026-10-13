namespace Practica1
{
    internal class Program
    {
        const int ANCHO = 71, ALTO = 20,  // ancho y alto del área de juego (sin contar la parte de puntuación)              
              LONG_PALA = 4, LEFT = 0, TOP = 0;         // longitud de las palas

        static Random rnd = new Random(); // para aleatorizar la dirección de la pelota
        static void Main()
        {
            Console.CursorVisible = false; // para no dibujar el cursor en pantalla

            int pala1Y, pala2Y,    // posición vertical de las palas
                pelotaX, pelotaY,  // posición de la pelota
                dirX, dirY1,       // dirección de la pelota
                puntos1, puntos2;  // puntos de cada jugador


            // Inicialización de las variables de estado del juego
            pala1Y = (ALTO / 2) - 1;
            pala2Y = (ALTO / 2) - 1;

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

                // COLISIÓNES: MUROS SUPERIOR E INFERIOR, SALIDA DEL ÁREA DE JUEGO (PUNTO JUGADOR), PALAS 

                // RENDERIZADO
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Green;

                Console.SetCursorPosition(LEFT, TOP);
                for (int i = 0; i <= ANCHO; i++)                    //Muro superior
                {
                    Console.SetCursorPosition(i, TOP);
                    Console.Write(" ");
                }
                Console.ResetColor();

                
                Console.BackgroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(LEFT, ALTO);
                for (int i = 0; i <= ANCHO; i++)                    //Muro inferior
                {
                    Console.SetCursorPosition(i, ALTO);
                    Console.Write(" ");
                }
                Console.ResetColor();


                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(ANCHO/2, (1));
                for (int i = 0; i < (ALTO - 1); i++)                //Red
                {
                    Console.SetCursorPosition(ANCHO/2, ( 1 + i));
                    Console.Write("|");
                }
                Console.ResetColor();


                
                Console.SetCursorPosition( 1, pala1Y);
                for (int i = 0; i < LONG_PALA; i++)                 //Jugador 1
                {
                    Console.SetCursorPosition( 1, pala1Y + i);
                    Console.Write("|");
                }
                Console.ResetColor();


                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(ANCHO - 1 , pala2Y);
                for (int i = 0; i < LONG_PALA; i++)                 //Jugador 2
                { 
                    Console.SetCursorPosition(ANCHO - 1, pala2Y + i);
                    Console.Write("|");
                }
                Console.ResetColor();
               

                
                                 // dibujo red central, pelota, palas, puntos jugadores

                // Retardo
                System.Threading.Thread.Sleep(90);

                // borrado de la pelota y palas para el siguiente frame

            }
        }
    }
}
