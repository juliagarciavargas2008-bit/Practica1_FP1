namespace Practica1
{
    internal class Program
    {
        const int ANCHO = 71, ALTO = 20,  // ancho y alto del área de juego (sin contar la parte de puntuación)              
              LONG_PALA = 4, LEFT = 0, TOP = 0;        // longitud de las palas

        static Random rnd = new Random(); // para aleatorizar la dirección de la pelota
        static void Main()
        {
            Console.CursorVisible = false; // para no dibujar el cursor en pantalla

            int pala1Y, pala2Y,    // posición vertical de las palas
                pelotaX, pelotaY,  // posición de la pelota
                dirX, dirY,        // dirección de la pelota
                puntos1, puntos2,  // puntos de cada jugador
                retardo, time;


            // Inicialización de las variables de estado del juego

            pala1Y = (ALTO - LONG_PALA) / 2;
            pala2Y = (ALTO - LONG_PALA) / 2;

            pelotaX = ANCHO / 2 + 1;    //Posicion de la pelota (inicio)
            pelotaY = ALTO / 2;             
            dirX = 1;                   //Direccion de la pelota (inicio)
            dirY = 1;

            puntos1 = 0;    //Puntuaciones
            puntos2 = 0;

            retardo = 90;
            time = 0;       //Tiempo desde que inició la partida

            bool salir = false;
            bool pause = false;

            while (!salir)
            {
                //BORRADO DE ELEMENTOS INTERACTIVOS
                for (int i = 0; i < LONG_PALA; i++)
                {                                               
                    Console.SetCursorPosition(1, pala1Y + i);       //Borra ambas palas
                    Console.Write(" ");
                    Console.SetCursorPosition(ANCHO - 2, pala2Y + i);
                    Console.Write(" ");
                }


                // LEER TECLADO

                while (Console.KeyAvailable)
                { // ver si se ha pulsado alguna tecla
                    ConsoleKeyInfo tecla = Console.ReadKey(true); // recoger pulsación de tecla
                    if ((tecla.Key == ConsoleKey.W) && (pala1Y != 2)) pala1Y--;         //La pala 1 sube

                    else if ((tecla.Key == ConsoleKey.S) && (pala1Y != ((ALTO - 1) - LONG_PALA))) pala1Y++;  //La pala 1 baja

                    else if((tecla.Key == ConsoleKey.I) && (pala2Y != 2)) pala2Y--;         //La pala 2 sube

                    else if((tecla.Key == ConsoleKey.K) && (pala2Y != (ALTO - 1) - LONG_PALA)) pala2Y++;    //La pala 2 baja
                    
                    if (tecla.Key == ConsoleKey.Q) salir = true;                   //Hace que el juego se termine (AKA: lo rompe) (No hay vuelta atrás)
                
                    if (!pause)
                    {
                        if (tecla.Key == ConsoleKey.P)
                        {
                            pause = true;
                            Console.SetCursorPosition((ANCHO / 2), (ALTO / 2) - 1);
                            for (int i = 0; i < 3; i++)                                     //Pausa el juego, parando todo el bucle
                            {                                                               //Escribe pausa borrando una posición arriba y abajo
                                Console.SetCursorPosition(ANCHO / 2, (ALTO / 2) - 1 + i);
                                Console.Write(" ");
                            }
                            Console.SetCursorPosition((ANCHO / 2) - 2, ALTO / 2);
                            Console.Write("PAUSE");
                        }
                    } 
                    else
                    {
                        if (tecla.Key == ConsoleKey.P)
                        {
                            for (int i = 0; i < 5; i++)                                    
                            {                                                               //Borra la línea de pause
                                Console.SetCursorPosition(ANCHO / 2 - 2 + i, (ALTO / 2));
                                Console.Write(" ");
                            }
                            pause = false;                   //Si se pulsa P de nuevo se despausa, devuelve el false al bool
                           
                        }
                    }
                }

                if (!pause) {

                    //BORRADO DE ELEMENTOS MÓVILES

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(pelotaX, pelotaY);       //Pelota
                    Console.Write(" ");
                    Console.ResetColor();

                    // MOVIMIENTO PELOTA

                    if (((pelotaY == 1) && (dirY == -1)) || ((pelotaY == (ALTO - 1)) && (dirY == 1))) 
                    {                                                 //Si la pelota llega a alguno de los bordes con intencion de chocar
                        dirY = -  dirY;
                    }

                    if ((pelotaX == 0) || (pelotaX == (ANCHO)))
                    {                                                 //Si llega a alguna de las "porterias"
                        if (pelotaX == 0) puntos2++;                  //Básicamente suma los puntos dependiendo de a que portería haya llegado
                        if (pelotaX == ANCHO) puntos1++;
                        pelotaX = ANCHO / 2 + 1;
                        pelotaY = ALTO / 2;
                        dirX = dirY = 1;
                        dirY = 1;
                        if (time > 0) time -= 100;       //Deja más el tiempo antes de que vaya aumentando la velocidad
                        if (retardo < 90) retardo += 20;
                    }

                    // COLISIÓNES: MUROS SUPERIOR E INFERIOR, SALIDA DEL ÁREA DE JUEGO (PUNTO JUGADOR), PALAS

                    if ((pelotaX == 2) && (pala1Y <= pelotaY) && (pelotaY <= (pala1Y + LONG_PALA))) dirX = -dirX;

                    if ((pelotaX == ANCHO - 3) && (pala2Y <= pelotaY) && (pelotaY <= (pala2Y + LONG_PALA))) dirX = -dirX;


                    pelotaX = pelotaX + dirX;           //Movemos la pelota
                    pelotaY = pelotaY + dirY;

                    if (time > 200)                 //Cuenta las veces que se ha repetido el bucle
                    {
                        if (retardo > 20)           //Si supera las 200 va haciendo que el juego vaya más rápido
                        {                           //No vuelve a contar hasta 200, le doy ventaja
                            retardo -= 5;           //así una vez que empieza a subir la velocidad no para
                            time = 175;
                            
                        }
                    }

                    // RENDERIZADO

                    //Renderizado de constantes

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
                    Console.SetCursorPosition(ANCHO / 2, (1));        //Red
                    for (int i = 0; i < (ALTO - 1); i++)
                    {                                               // Se coloca en la mitad (p35)
                        Console.SetCursorPosition(ANCHO / 2, (1 + i));
                        Console.Write("|");
                    }
                    Console.ResetColor();

                            
                    Console.SetCursorPosition(1,ALTO + 2);          //Puntos de cada jugador
                    Console.Write($"Jugador 1: {puntos1, -7} Jugador 2: {puntos2, -7} (Q para salir)");
                    Console.ResetColor();

                    //Renderizado de elementos móviles

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


                    // Retardo
                    time++;

                    System.Threading.Thread.Sleep(retardo);

                    // borrado de la pelota y palas para el siguiente frame
                }
            }
        }
    }
}
