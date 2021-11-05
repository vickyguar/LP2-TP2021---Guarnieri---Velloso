﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LP2_TP2021___Guarnieri___Velloso
{
    public class Solucion
    {
        List<Ficha> Posiciones;
        TipoSolucion Tipo;

        public List<Ficha> Posiciones_ { get => Posiciones; set => Posiciones = value; }
        public TipoSolucion Tipo_ { get => Tipo; set => Tipo = value; }

        public Solucion(List<Ficha> Lista, TipoSolucion type_)
        {
            Posiciones = Lista;
            Tipo = type_;
        }

    }

    public partial class Carátula : Form
    {
      
        public Carátula()
        {
            InitializeComponent();
        }

        private void Carátula_Load(object sender, EventArgs e)
        {

        }

        #region BOTON

        /// <summary>
        /// Evento click en el botón GENERAR de <see cref="Carátula"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_generar_Click(object sender, EventArgs e)
        {
            List<Tablero> ListaSoluciones = Programa();
            Soluciones FormSoluciones = new Soluciones(ListaSoluciones, this);
            FormSoluciones.Show(); //Abrimos el form de soluciones
            this.Hide(); //Cerramos el Form de Carátula
        }

        #endregion

        #region PROGRAMA

        /// <summary>
        /// Algoritmo
        /// </summary>
        /// <returns></returns>
        static List<Solucion> Programa()
        {
            int ID = -1;
            Tablero Juego = new Tablero(ID);
            Random rnd = new Random(); //Es un random que usamos para luego elegir qué cuadrado darle a la Torre2
            List<Solucion> Solutions = new List<Solucion>();

            #region FICHAS

            //Creamos Fichas
            Ficha Reina = new Reina("Reina");
            Ficha Rey = new Rey("Rey");
            Ficha Torre1 = new Torre("Torre1");
            Ficha Torre2 = new Torre("Torre2");
            Ficha Alfil1 = new Alfil("Alfil1");
            Ficha Alfil2 = new Alfil("Alfil2");
            Ficha Caballo1 = new Caballo("Caballo1");
            Ficha Caballo2 = new Caballo("Caballo2");

            #endregion

            List<Casilla> Cuadrado1 = new List<Casilla>(4); //5e, 5d, 4e, 4d ROJO
            List<Casilla> Cuadrado2 = new List<Casilla>(12); //6c, 3c, 3f, 6f VIOLETA
            List<Casilla> Cuadrado3 = new List<Casilla>(20); //7b, 2b, 7g, 2g AZUL
            List<Casilla> Cuadrado4 = new List<Casilla>(28); //8a, 1a, 8h, 1a VERDE

            //ROJO, VIOLETA, AZUL, VERDE -> según las regiones del tablero que marcamos en nuestro pseudocódigo

            List<Tablero> ListaSoluciones = new List<Tablero>(); //Esta es la que se retorna

            while (Solutions.Count < 11)
            {
                #region LISTAS

                for (uint i = 3; i <= 4; i++)
                {
                    for (uint j = 3; j <= 4; j++)
                        Cuadrado1.Add(new Casilla(i, j));
                }

                //FILA FIJA
                for (uint i = 2; i <= 5; i++)
                {
                    Cuadrado2.Add(new Casilla(2, i));
                    Cuadrado2.Add(new Casilla(5, i));
                }

                //COLUMNA FIJA
                for (uint i = 3; i < 5; i++)
                {
                    Cuadrado2.Add(new Casilla(i, 2));
                    Cuadrado2.Add(new Casilla(i, 5));
                }

                //FILA FIJA
                for (uint i = 1; i <= 6; i++)
                {
                    Cuadrado3.Add(new Casilla(1, i));
                    Cuadrado3.Add(new Casilla(6, i));
                }

                //COLUMNA FIJA
                for (uint i = 2; i < 6; i++)
                {
                    Cuadrado3.Add(new Casilla(i, 1));
                    Cuadrado3.Add(new Casilla(i, 6));
                }

                //FILA FIJA
                for (uint i = 0; i < Global.N_; i++)
                {
                    Cuadrado4.Add(new Casilla(0, i));
                    Cuadrado4.Add(new Casilla(7, i));
                }

                //COLUMNA FIJA
                for (uint i = 1; i < 7; i++)
                {
                    Cuadrado4.Add(new Casilla(i, 0));
                    Cuadrado4.Add(new Casilla(i, 7));
                }

                #endregion

                #region POSICIONAMIENTO DE LAS FICHAS

                Juego.Posicionar(Reina, Cuadrado1);
                Juego.Posicionar(Alfil1, Cuadrado1);
                Juego.Posicionar(Alfil2, Cuadrado2);
                Juego.Posicionar(Caballo1, Cuadrado2);
                Juego.Posicionar(Rey, Cuadrado3);
                Juego.Posicionar(Torre1, Cuadrado4);

                switch (Convert.ToInt32(rnd.Next(1, 4))) //A la torre 2 le paso eliminar
                {
                    case 1:
                        Juego.Posicionar(Torre2, Cuadrado2, false); break;
                    case 2:
                        Juego.Posicionar(Torre2, Cuadrado3, false); break;
                    case 3:
                        Juego.Posicionar(Torre2, Cuadrado4, false); break;
                }

                Juego.Posicionar(Caballo2, Cuadrado2, false);

                #endregion

                #region GENERACIÓN DE SOLUCIONES

                if (Juego.VerificarSolucion())
                {
                    Tablero Table = new Tablero( ++ID); //#1 //////////////////////////////////////////////////// CONSTRUCTOR POR COPIA
                    Solutions.Add(new Solucion(CopiaLista(Juego.ListaPosicionadas_), Juego.Type));

                    Table.ImprimirOutput(); //Vemos si funciona nuestro código, mirandolo en nuestro Debug
                   // ListaSoluciones.Add(Table);

                    #region ROTADO DE ORIGINAL

                    //TABLERO ROTADO 1 (90°) 
                    Tablero Rotado1 = new Tablero(++ID); //#2
                    //Tablero Rotado1 = new Tablero(++ID); //#2
                    Table.Rotar90(Rotado1);
                    ListaSoluciones.Add(Rotado1);
                    Solutions.Add(new Solucion(CopiaLista(Rotado1.ListaPosicionadas_), Rotado1.Type));

                    Rotado1.ImprimirOutput();

                    //TABLERO ROTADO 2 (180°)
                    Tablero Rotado2 = new Tablero(++ID); //#3
                    Rotado1.Rotar90(Rotado2);
                    Solutions.Add(new Solucion(CopiaLista(Rotado2.ListaPosicionadas_), Rotado2.Type));

                    ListaSoluciones.Add(Rotado2);
                   // Rotado2.ImprimirOutput();

                    //TABLERO ROTADO 3 (270°)
                    Tablero Rotado3 = new Tablero(++ID); //#4
                    Rotado2.Rotar90(Rotado3);
                    Solutions.Add(new Solucion(CopiaLista(Rotado3.ListaPosicionadas_), Rotado3.Type));

                    ListaSoluciones.Add(Rotado3);
                    //Rotado3.ImprimirOutput();

                    #endregion

                    #region ESPEJADO ORIGINAL
                    //ESPEJADO 1
                    Tablero Espejado = new Tablero(++ID); //#5
                    Table.Espejar(Espejado);////////////////////////////////////////////////////////// ACA SE ESTA USANDO JUEGO
                    Solutions.Add(new Solucion(CopiaLista(Espejado.ListaPosicionadas_), Espejado.Type));

                    ListaSoluciones.Add(Espejado);
                    //Espejado.ImprimirOutput();

                    //ESPEJADO ROTADO 1 (90)
                    Tablero EspejadoRotado1 = new Tablero( ++ID); //#6
                    Espejado.Rotar90(EspejadoRotado1);
                    Solutions.Add(new Solucion(CopiaLista(EspejadoRotado1.ListaPosicionadas_), EspejadoRotado1.Type));

                    ListaSoluciones.Add(EspejadoRotado1);
                   // EspejadoRotado1.ImprimirOutput();

                    //ESPEJADO ROTADO 2 (180)
                    Tablero EspejadoRotado2 = new Tablero( ++ID); //#7
                    EspejadoRotado1.Rotar90(EspejadoRotado2);
                    Solutions.Add(new Solucion(CopiaLista(EspejadoRotado2.ListaPosicionadas_), EspejadoRotado2.Type));

                    ListaSoluciones.Add(EspejadoRotado2);
                   // EspejadoRotado2.ImprimirOutput();

                    //ESPEJADO ROTADO 3 (270)
                    Tablero EspejadoRotado3 = new Tablero( ++ID); //#8
                    EspejadoRotado2.Rotar90(EspejadoRotado3);
                    Solutions.Add(new Solucion(CopiaLista(EspejadoRotado3.ListaPosicionadas_), EspejadoRotado3.Type));

                    ListaSoluciones.Add(EspejadoRotado3);
                    //EspejadoRotado3.ImprimirOutput();

                    #endregion

                    #region INTERCAMBIO TORRES
                    //INTERCAMBIO
                    Tablero Intercambiado = new Tablero( ++ID);  //#9
                    Table.IntercambiarTorres(Intercambiado);
                    Solutions.Add(new Solucion(CopiaLista(Intercambiado.ListaPosicionadas_), Intercambiado.Type));

                    ListaSoluciones.Add(Intercambiado);
                  //  Intercambiado.ImprimirOutput();

                    //INTERCAMBIO ROTADO 1 (90)
                    Tablero IntercambioRotado1 = new Tablero(++ID); //#10
                    Intercambiado.Rotar90(IntercambioRotado1);
                    Solutions.Add(new Solucion(CopiaLista(IntercambioRotado1.ListaPosicionadas_), IntercambioRotado1.Type));

                    ListaSoluciones.Add(IntercambioRotado1);
                   // IntercambioRotado1.ImprimirOutput();

                    //INTERCAMBIO ROTADO (180)
                    Tablero IntercambioRotado2 = new Tablero(++ID); //#11
                    IntercambioRotado1.Rotar90(IntercambioRotado2);
                    Solutions.Add(new Solucion(CopiaLista(IntercambioRotado2.ListaPosicionadas_), IntercambioRotado2.Type));

                    ListaSoluciones.Add(IntercambioRotado2);
                   // IntercambioRotado2.ImprimirOutput();


                    //INTERCAMBIO ROTADO (270)
                    Tablero IntercambioRotado3 = new Tablero( ++ID); //#12
                    IntercambioRotado2.Rotar90(IntercambioRotado3);
                    Solutions.Add(new Solucion(CopiaLista(IntercambioRotado3.ListaPosicionadas_), IntercambioRotado3.Type));

                    ListaSoluciones.Add(IntercambioRotado3);
                   // IntercambioRotado3.ImprimirOutput();

                    #endregion

                    #endregion

                    #region VERIFICAR SOLUCIONES DISTINTAS 

                    //for (int j = 0; j < ListaSoluciones.Count; ++j)
                    //{
                    //    Tablero Solucion = ListaSoluciones[j];

                    //    for (int i = 0; i < ListaSoluciones.Count; ++i)
                    //    {
                    //        if (!Solucion.VerificarSolucionesDistintas(ListaSoluciones[i]))
                    //        {
                    //            ListaSoluciones.Remove(Solucion);
                    //        }
                    //    }
                    //}

                    #endregion
                }

                #region LIMPIAMOS LOS TABLEROS

                Juego.Limpiar();

                #endregion

                #region VACIAMOS CUADRADOS

                Cuadrado1.Clear();
                Cuadrado2.Clear();
                Cuadrado3.Clear();
                Cuadrado4.Clear();

                #endregion
            }

            for (int i = 0; i < ListaSoluciones.Count; ++i)
            {
                ListaSoluciones[i].ImprimirOutput();
            }

            return Solutions;

        }

        #endregion

        /*
         * Steps para el Form:
         * Main() -> Cartula
         * Cuando se precione le boton generar, hay que correr el programa
         * Se tiene que abrir un form con un boton de next con las soluciones
         */

        static List<Ficha> CopiaLista(List<Ficha> OldList)
        {
            List<Ficha> Nueva = new List<Ficha>();
            foreach (Ficha Fichita in OldList)
            {
                if (!Nueva.Contains(Fichita))
                {
                    if (Fichita is Reina)
                        Nueva.Add(new Reina((Reina)Fichita));
                    else if (Fichita is Rey)
                        Nueva.Add(new Rey((Rey)Fichita));
                    else if (Fichita is Alfil)
                        Nueva.Add(new Alfil((Alfil)Fichita));
                    else if (Fichita is Torre)
                        Nueva.Add(new Torre((Torre)Fichita));
                    else if (Fichita is Caballo)
                        Nueva.Add(new Caballo((Caballo)Fichita));
                    else
                        Nueva.Add(null);
                }
            }

            return Nueva;
        }

    }
}