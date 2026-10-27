namespace Práctica1
{
    internal class Program
    {
        const int ANCHO = 81, ALTO = 20,  // ancho y alto del área de juego (sin contar la parte de puntuación)              
              LONG_PALA = 4;          // longitud de las palas

        static Random rnd = new Random(); // para aleatorizar la dirección de la pelota

        static void Main()
        {
            Console.CursorVisible = false; // para no dibujar el cursor en pantalla

            int pala1Y = (ALTO / 2 - LONG_PALA / 2),            // posición vertical de las palas
                pala2Y = (ALTO / 2 - LONG_PALA / 2),
                pelotaX = ANCHO / 2 + 1,                        // posición de la pelota
                pelotaY = ALTO / 2,
                dirX = -1, dirY = 1,                            // dirección de la pelota
                puntos1 = 0, puntos2 = 0,                       // puntos de cada jugador
                retardo = 90,                                   //Esto contralará la velocidad de la pelota
                tiempo = 0;

            bool salir = false,
                 pause = false;
            
            while (!salir)
            {
                for (int i = 1; i < ALTO; i++)
                {
                    Console.SetCursorPosition(1, i);
                    Console.Write(" ");
                    Console.SetCursorPosition(ANCHO - 1, i);
                    Console.Write(" ");
                }

                // LEER TECLADO
                while (Console.KeyAvailable)
                { // ver si se ha pulsado alguna tecla
                    ConsoleKeyInfo tecla = Console.ReadKey(true); // recoger pulsación de tecla
                    //Movimiento Jugador 1
                    if ((tecla.Key == ConsoleKey.W) && (pala1Y > 1)) pala1Y--;              //La pala 1 solo sube si no choca con el muro
                    else if ((tecla.Key == ConsoleKey.S) && (pala1Y < ALTO - 5)) pala1Y++;  //La pala 1 solo baja si no choca con el muro

                    //Movimiento Jugador 2
                    else if ((tecla.Key == ConsoleKey.I) && (pala2Y > 1)) pala2Y--;         //La pala 2 solo sube si no choca con el muro
                    else if ((tecla.Key == ConsoleKey.K) && (pala2Y < ALTO - 5)) pala2Y++;  //La pala 2 solo sube si no choca con el muro

                    //Terminar el juego
                    else if (tecla.Key == ConsoleKey.Q) salir = true; //El bucle se rompe, es decir, el juego ya no continua
                    if (tecla.Key == ConsoleKey.P)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            Console.SetCursorPosition((ANCHO / 2) - 2 + i, ALTO / 2);
                            Console.Write(" ");
                        }
                        pause = !pause; //Cambia el estado de juego, es decir, si está en funcionamiento o en pausa
                    }
                }

                if (!pause)          //Si el juego no está en pausa
                {
                    // MOVIMIENTO PELOTA
                    Console.SetCursorPosition(pelotaX, pelotaY);
                    Console.Write(" ");
                    pelotaX = pelotaX + dirX;
                    pelotaY = pelotaY + dirY;


                    // COLISIÓNES:
                    //Con los muros
                    if ((pelotaY == ALTO - 1) || (pelotaY == 1)) dirY = -dirY;
                    

                    //Con las palas
                    if (pelotaX == 2)           //Posición justo al frente de la pala 1
                    {
                        for (int i = 0; i < LONG_PALA; i++)     //Detecta toda la longitud de la pala 1
                        {
                            if (pelotaY == pala1Y + i) dirX = -dirX;     //Si está a la altura de la pala 1, cambia el sentido horizontal
                        }
                    }

                    if (pelotaX == ANCHO - 2)   //Posición justo al frente de la pala 2
                    {
                        for (int i = 0; i < LONG_PALA; i++)     //Detecta toda la longitud de la pala 2
                        {
                            if (pelotaY == pala2Y + i) dirX = -dirX;     //Si está a la altura de la pala 2, cambia el sentido horizontal
                        }
                    }

                    //Con las porterías
                    if((pelotaX == 0) || (pelotaX == ANCHO))
                    {
                        if (pelotaX == 0) puntos1++;
                        else if (pelotaX == ANCHO) puntos2++;
                        pelotaX = ANCHO / 2 + 1;
                        pelotaY = ALTO / 2;
                        
                    }

                    
                    // RENDERIZADO DE ELEMENTOS ESTÁTICOS
                    
                    //Muro superior
                    Console.BackgroundColor = ConsoleColor.Green;
                    for (int i = 0; i <= ANCHO; i++)               //Desde 0 hasta el ancho del muro para que se pinte todo el muro
                    {
                        Console.SetCursorPosition(i, 0);            //Se pinta toda la línea 0
                        Console.Write(" ");
                    }
                    
                    //Muro inferior
                    for (int i = 0; i <= ANCHO; i++)                //Desde 0 hasta el ancho del muro para que se pinte todo el muro
                    {
                        Console.SetCursorPosition(i, ALTO);         //Se pinta toda la línea ALTO
                        Console.Write(" ");
                    }
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Cyan;     //Red
                    for (int i = 1; i <= ALTO - 1; i++)              //Se usa el ALTO - 1 porque se tiene en cuenta los muros
                    {
                        Console.SetCursorPosition(ANCHO / 2, i);
                        Console.Write("|");
                    }
                    Console.ResetColor();

                    //RENDERIZADO DE ELEMENTOS MÓVILES
                    Console.ForegroundColor = ConsoleColor.Red;     //Jugador 1
                    for (int i = 0; i <= LONG_PALA; i++)            //Abarca todo el tamaño del jugador 1
                    {
                        Console.SetCursorPosition(1, pala1Y + i);       //Se coloca el cursor en esta zona para que la pala quede en el medio y en el lado izquierdo
                        Console.Write("|");
                    }
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Blue;     //Jugador 2
                    for (int i = 0; i <= LONG_PALA; i++)             //Abarca todo el tamaño del jugador 2
                    {
                        Console.SetCursorPosition(ANCHO - 1, pala2Y + i);       //Se coloca el cursor en esta zona para que la pala quede en el medio y en el lado derecho
                        Console.Write("|");
                    }
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.White;   //Pelota
                    //Pelota
                    Console.SetCursorPosition(pelotaX, pelotaY);
                    Console.Write("O");


                    //Puntuación Jugador 1
                    Console.SetCursorPosition(0, ALTO + 1);       //Se coloca el debajo del muro inferior y a la izquierda
                    Console.Write("Puntuación Jugador 1: " + puntos1);

                    //Puntuación Jugador 2
                    Console.SetCursorPosition(ANCHO / 2 + 1, ALTO + 1);       //Se coloca el debajo del muro inferior y a la derecha
                    Console.Write("Puntuación Jugador 2: " + puntos2);
                    Console.ResetColor();
                    // dibujo red central, pelota, palas, puntos jugadores

                    //Retardo
                    System.Threading.Thread.Sleep(retardo);

                    // borrado de la pelota y palas para el siguiente frame

                }
                else        //Modo de pausa
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    for(int i = 0; i <= 2; i++)
                    {
                        Console.SetCursorPosition((ANCHO / 2),(ALTO / 2 - 1) + i);
                        Console.Write(" ");
                    }
                    Console.SetCursorPosition((ANCHO/ 2) - 2, ALTO/2);
                    Console.Write("Pausa");
                }
            }
        }
    }
}
