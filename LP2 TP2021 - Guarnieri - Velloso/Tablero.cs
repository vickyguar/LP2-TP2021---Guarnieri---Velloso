﻿///////////////////////////////////////////////////////////
//  Tablero.cs
//  Implementation of the Class Tablero
//  Generated by Enterprise Architect
//  Created on:      06-oct.-2021 08:09:08
//  Original author: vguar
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Drawing.Drawing2D;

/// <summary>
/// Enum para definir el Color de las Casillas.
/// </summary>
public enum eColor : int
{
    BLANCO = 0,
    NEGRO

} //end eColor

/// <summary>
/// Enum para definir el tipo de Solucion que es un <see cref="Tablero"/>.
/// </summary>
public enum TipoSolucion : int
{
    NO_SOLUCION = 0,
    LEVE,
    FATAL

} //end TipoSolucion

public class Tablero
{
    #region ATRIBUTOS

    /// <summary>
    /// Matriz de Casillas del <see cref="Tablero"/>.
    /// </summary>
    public Casilla[,] Matriz = new Casilla[Global.N_, Global.N_]; //Acceso publico, para que las fichas se puedan posicionar y atacar

    /// <summary>
    /// Lista de Fichas (se llena a medida que se posicionan las Ficha) del <see cref="Tablero"/>.
    /// </summary>
    private SortedList<uint,Ficha> ListaPosicionadas = new SortedList<uint, Ficha>(Global.N_);

    /// <summary>
    /// Tipo de solucion que sea (Leve, Fatal, No Solution)
    /// </summary>
    private TipoSolucion type;

    #endregion

    #region CONSTRUCTORES & DESTRUCTORES

    /// <summary>
    /// Constructor de la clase <see cref="Tablero"/>.: inicializa la Matriz de Casillas.
    /// </summary>
    public Tablero()
    {
        //Creamos la matriz de Casillas (tiempo n^2, fors anidados)
        for (int i = 0; i < Global.N_; ++i)
        {
            for (int j = 0; j < Global.N_; ++j)
            {
                Matriz[i, j] = new Casilla(i, j);
            }
        }

        Type = TipoSolucion.NO_SOLUCION;
        LeerArchivo(); //Inicializamos los colores del Tablero
    }

    /// <summary>
    /// Constructor por copia de la clase <see cref="Tablero"/>..
    /// </summary>
    /// <param name="newTablero"></param>
    public Tablero(Tablero newTablero)
    {
        //Variable auxiliar

        //Matriz = newTablero.Matriz; //TODO: preguntar si esto es válido
        for (int i = 0; i < Global.N_; ++i)
        {
            for (int j = 0; j < Global.N_; ++j)
            {

                Matriz[i, j] = new Casilla(newTablero.Matriz[i, j], newFicha(newTablero.Matriz[i, j].Fichita), newFicha(newTablero.Matriz[i, j].Superpuesta));
            }
        }

        this.Type = newTablero.Type;
    }

    /// <summary>
    /// Destructor de la clase <see cref="Tablero"/>.
    /// </summary>
    ~Tablero()
    {

    }

    #endregion

    #region LEER TXT

    /// <summary>
    /// Leemos el archivo con 1's y 0's para determinar el color de las Casillas del Tablero this.
    /// </summary>
    private void LeerArchivo()
    {
        string[] lines = System.IO.File.ReadAllLines("ColoresTablero.txt"); //Leo el archivo de colores
        int j = 0;

        foreach (string line in lines) //para cada linea
        {
            for (int i = 0; i < line.Length; i++)//recorro los caracteres de cada linea
                Matriz[j, i].Colour = line[i] == '0' ? eColor.BLANCO : eColor.NEGRO; //le asigno el color a cada casilla

            j++;
        }
    }

    #endregion

    #region METODOS DE TABLERO

    /// <summary>
    /// Limpia el Tablero this: Las Casillas no están atacadas, ni ocupadas, y las Fichas son null.
    /// </summary>
    public void Limpiar()
    {
        for (uint i = 0; i < Global.N_; i++)
        {
            for (uint j = 0; j < Global.N_; j++)
            {
                Matriz[i, j].SetOcupada(false);

                Matriz[i, j].SetAtacada(false);
                Matriz[i, j].SetAtacadaFatalmente(false);

                Matriz[i, j].SetFicha(null);
                Matriz[i, j].SetSuperpuesta(null);
            }
        }
        Type = TipoSolucion.NO_SOLUCION; 
        ListaPosicionadas.Clear();
    }

    /// <summary>
    /// Ubica a la Ficha que le llega por parámetro en una Casilla de la SubLista que le llega por parámetro.
    /// Segun el parámetro booleano "Remove", quita de la SubLista la Casilla donde se ubico la Ficha.
    /// Llama a la función Ataque de Ficha, con el Tablero Ataque.
    /// </summary>
    /// <param name="Fichita"></param>
    /// <param name="Ataque"></param>
    /// <param name="SubLista"></param>
    /// <param name="Remove"></param>
    public void Posicionar(Ficha Fichita, List<Casilla> SubLista, bool Remove = true) //Se administra desde el main
    {
        //Variable
        Random r = new Random();
        int index = r.Next(SubLista.Count); //Elegimos un índice random de la SubLista 

        int i = SubLista[index].GetFila(); //guardamos la fila correspondiente a la casilla
        int j = SubLista[index].GetColumna(); //guardamos la columna correspondiente a la casilla

        if (Fichita.GetName() == "Alfil2") //si estamos posicionando el segundo alfil
        {
            Alfil FichaAux = (Alfil)ListaPosicionadas[1]; //el ultimo (antes de posicionar el alfil 2) simpre es Alfil1
            
            //Matriz[i, j].SetFicha(Fichita);

            while (Matriz[i,j].Colour == Matriz[FichaAux.Fila, FichaAux.Columna].Colour) //Mientras los dos alfiles sean del mismo color
            {
                index = r.Next(SubLista.Count); //Elegimos un índice random de la SubLista 

                i = SubLista[index].GetFila(); //nos guardamos nuevamente la fila
                j = SubLista[index].GetColumna(); //nos guardamos nuevamente la columna
            }
        }

        //Ocupamos la casilla con la fichita
        Matriz[i, j].SetFicha(Fichita);

        //Condiciones para poder superponer:
        /* Que !Remove -> no me borraron la posicion del tablero en la lista de los cuadrados
         * La ficha que se va a superponer es un caballo
         * Torre2.Fila == i
         * Torre2.Columna == j
         */

        if (!Remove && Fichita is Caballo)
        {
            if (ListaPosicionadas[6].Fila == i && ListaPosicionadas[6].Columna == j)
                Matriz[i, j].SetSuperpuesta(Fichita); //La unica que se puede superponer es Caballo2
        }

        //ListaPosicionadas.Add(Fichita); //agrego a la lista la ficha que posicioné
        Agregar(Fichita, ListaPosicionadas);

        if (Remove)
            SubLista.RemoveAt(index); //Sacamos de la lista al elemento ocupado, para que otros no lo puedan ocupar.

    }

    /// <summary>
    /// Imprime en el output las fichas del <see cref="Tablero"/>
    /// </summary>
    public void ImprimirOutput()
    {
        Debug.Write("\n\n");

        for (uint i = 0; i < Global.N_; ++i)
        {
            Debug.Write("|");
            for (uint j = 0; j < Global.N_; ++j)
            {
                if (Matriz[i, j].Fichita == null)
                {
                    Debug.Write(" # |");
                }
                else if (Matriz[i, j].Superpuesta != null)
                {
                    Debug.Write(" S |");
                }
                else
                {
                    if (Matriz[i, j].Fichita.GetName() == "Reina")
                        Debug.Write(" Q |");
                    else if (Matriz[i, j].Fichita.GetName() == "Rey")
                        Debug.Write(" K |");
                    else if (Matriz[i, j].Fichita.GetName() == "Torre1" || Matriz[i, j].Fichita.GetName() == "Torre2")
                        Debug.Write(" T |");
                    else if (Matriz[i, j].Fichita.GetName() == "Caballo1" || Matriz[i, j].Fichita.GetName() == "Caballo2")
                        Debug.Write(" C |");
                    else
                        Debug.Write(" A |");
                }

            }
            Debug.Write("\n");
        }
        Debug.Write("\n\n");

    }

    /// <summary>
    /// Retorna una nueva Ficha del tipo de la Fichita que le llega por parámetro.
    /// </summary>
    /// <param name="Fichita"></param>
    /// <returns></returns>
    public Ficha newFicha(Ficha Fichita)
    {
        if (Fichita is Reina)
            return new Reina((Reina)Fichita);
        else if (Fichita is Rey)
            return new Rey((Rey)Fichita);
        else if (Fichita is Alfil)
        {
            Alfil A = (Alfil)Fichita;
            return new Alfil((Alfil)Fichita);
        }
        else if (Fichita is Caballo)
            return new Caballo((Caballo)Fichita);
        else if (Fichita is Torre)
            return new Torre((Torre)Fichita);
        else
            return null; //Just in case

    }

    #endregion

    #region MULTIPLICADORES DE SOLUCIONES

    /// <summary>
    /// Rota 90° el tablero.
    /// </summary>
    /// <returns></returns>
    public void Rotar90()
    {
        int N = Global.N_ - 1;

        for (int i = 0; i < Global.N_ / 2; i++)
        {
            for (int j = i; j < Global.N_ - i - 1; j++)
            {
                Casilla aux = Matriz[i, j]; //Variable auxiliar        

                Matriz[i, j] = Matriz[j, N - i]; // Movemos Casillas de derecha a arriba

                Matriz[j, N - i] = Matriz[N - i, N - j]; // Movemos Casillas de abajo a la derecha

                Matriz[N - i, N - j] = Matriz[N - j, i]; // Movemos Casillas de izquierda a abajo

                Matriz[N - j, i] = aux;

            }
        }

        for (int i = 0; i < Global.N_; ++i)
        {
            for (int k = 0; k < Global.N_; ++k)
            {
                Matriz[i, k].SetFila(i);
                Matriz[i, k].SetColumna(k);

                if (Matriz[i, k].Fichita != null)
                {
                    Matriz[i, k].Fichita.Fila = i;
                    Matriz[i, k].Fichita.Columna = k;

                    //Si la casilla tiene dos fichas -> tengo qye mover las 2 ##################
                    if (Matriz[i, k].Superpuesta != null)
                    {
                        Matriz[i, k].Superpuesta.Fila = i;
                        Matriz[i, k].Superpuesta.Columna = k;
                        //ListaPosicionadas.Add(Matriz[i, k].Superpuesta);
                        Agregar(Matriz[i, k].Superpuesta, ListaPosicionadas);


                    }
                    //##########################################################################

                    //ListaPosicionadas.Add(Matriz[i, k].Fichita);
                    //Agregar(Espejar.Matriz[i, j].Superpuesta, Espejar.ListaPosicionadas);
                    Agregar(Matriz[i, k].Fichita, ListaPosicionadas);


                }
            }
        }
    }

    /// <summary>
    /// Espeja un tablero.
    /// </summary>
    /// <returns></returns>
    public void Espejar(Tablero Espejar)
    {
        Ficha aux, auxSup;

        for (int i = 0; i < Global.N_; ++i) //recorro fila
        {
            for (int j = 0; j < Global.N_; ++j) //recorro columna
            {
                if (Matriz[i, j].Fichita != null) //si hay una ficha en la casilla i,j
                {
                    aux = Matriz[i, j].Fichita; //ficha auxiliar
                    auxSup = Matriz[i, j].Superpuesta;

                    if (Matriz[i, 7 - j].Fichita != null) //si en la casilla "espejo" hay una ficha
                    {
                        Espejar.Matriz[i, j].SetFicha(Matriz[i, 7 - j].Fichita); //realizo el intercambio
                        //Espejar.ListaPosicionadas.Add(Espejar.Matriz[i, j].Fichita); //me guardo la ficha posicionada en la lista
                        Espejar.Agregar(Espejar.Matriz[i, j].Fichita, Espejar.ListaPosicionadas);

                        if (Matriz[i, 7 - j].Superpuesta != null) //#####################################################################
                        {
                            Espejar.Matriz[i, j].SetSuperpuesta(Matriz[i, 7 - j].Superpuesta); //realizo el intercambio
                            Espejar.Agregar(Espejar.Matriz[i, j].Superpuesta, Espejar.ListaPosicionadas);

                            //Espejar.ListaPosicionadas.Add(Espejar.Matriz[i, j].Superpuesta); //me guardo la ficha posicionada en la lista
                        }
                        //##############################################################################################################
                    }
                    else
                    {
                        Espejar.Matriz[i, 7 - j].SetFicha(aux); //espejo
                        Espejar.Agregar(Espejar.Matriz[i, 7 - j].Fichita, Espejar.ListaPosicionadas);
                    }

                    if (Matriz[i, j].Superpuesta != null) //############################################################################
                    {
                        Espejar.Matriz[i, 7 - j].SetSuperpuesta(auxSup); //espejo
                                                                         //Espejar.ListaPosicionadas.Add(Espejar.Matriz[i, 7 - j].Superpuesta); //me guardo la ficha posicionada en la lista
                        Espejar.Agregar(Espejar.Matriz[i, 7 - j].Superpuesta, Espejar.ListaPosicionadas);

                    }//#################################################################################################################

                    //Espejar.ListaPosicionadas.Add(Espejar.Matriz[i, 7 - j].Fichita); //me guardo la ficha posicionada en la lista
                }
            }
        }
    }

    /// <summary>
    /// Intercambia las columnas de las torres.
    /// </summary>
    /// <returns></returns>
    public void IntercambiarTorres(Tablero Intercambio)
    {
        uint T1=5, T2=6, C2=7;
        //SetLista();
        Intercambio.ListaPosicionadas = CopiaLista(this.ListaPosicionadas);
        
        try
        {
            //T1 = Buscar(Intercambio.ListaPosicionadas, "Torre1"); //buscamos las torres en la lista
            //T2 = Buscar(Intercambio.ListaPosicionadas, "Torre2");
            //C2 = Buscar(Intercambio.ListaPosicionadas, "Caballo2");
        }
        catch (Exception ex)
        {
            throw ex;
        }

        int x1 = Intercambio.ListaPosicionadas[T1].Fila;
        int y1 = Intercambio.ListaPosicionadas[T1].Columna;

        int x2 = Intercambio.ListaPosicionadas[T2].Fila;
        int y2 = Intercambio.ListaPosicionadas[T2].Columna;

        if (x1 != x2)
        {
            Casilla aux = Matriz[x2, y2];

            if (Matriz[x2, y1].Fichita == null) //Muevo a T2
            {
                //Intercambio.Matriz[x1, y1].SetFicha(null);
                Intercambio.ListaPosicionadas[T2].Columna = y1;
                //Intercambio.Matriz[x2, y1].SetFicha(ListaPosicionadas[T1]);
            }

            if (Matriz[x1, y2].Fichita == null) //Muevo a T1
            {
                Intercambio.ListaPosicionadas[T1].Columna = y2;

                if (aux.Superpuesta != null)
                {
                    Intercambio.Matriz[x2, y2].SetFicha(aux.Superpuesta);
                    //Matriz[x2, y2].SetSuperpuesta(null); //Está de más
                    Intercambio.ListaPosicionadas[C2].Fila = x2;
                    Intercambio.ListaPosicionadas[C2].Columna = y2;

                    //Intercambio.Matriz[x1, y2].SetFicha(ListaPosicionadas[T2]);
                }
                //else
                //{
                //    //Intercambio.Matriz[x2, y2].SetFicha(null);
                //    Intercambio.Matriz[x1, y2].SetFicha(ListaPosicionadas[T2]);
                //    Intercambio.ListaPosicionadas[T1].Columna = y2;
                //}
            }
            else if (Matriz[x1, y2].Fichita.GetName() == "Caballo2")
            {
                //Intercambio.Matriz[x2, y2].SetFicha(null); //pongo la ficha en null
                Ficha aux2 = Matriz[x1, y2].Fichita;
                Intercambio.ListaPosicionadas[T2].Columna = y2;
                Intercambio.Matriz[x1, y2].SetFicha(ListaPosicionadas[T2]); //Asignamos fichita
                Intercambio.Matriz[x1, y2].SetSuperpuesta(aux2); //primero setteo la superpuesta (el caballo)
            }

            return;
        }
    }

    #endregion

    #region VERIFICACIONES

    /// <summary>
    /// Retorna si es una solución  al problema de la cobertura total del Tablero de Ajedrez y si lo es, retorna true. 
    /// Además explica cuál es el tipo de la solución.
    /// </summary>
    /// <returns></returns>
    public bool VerificarSolucion()
    {
        TipoSolucion Type_ = TipoSolucion.FATAL;
       

        for (int i = 0; i < Global.N_; i++)
        {
            for (int j = 0; j < Global.N_; j++)
            {
                if (!Matriz[i, j].GetAtacadaFatalmente())
                    Type_ = TipoSolucion.LEVE;
                if (!Matriz[i, j].GetAtacada())
                {
                    Type = TipoSolucion.NO_SOLUCION;
                    return false;
                }
            }
        }
        Type = Type_;
        return true;
    }

    #endregion
     
    #region SETTERS & GETTES

    public TipoSolucion Type { get => type; set => type = value; }
    public SortedList<uint, Ficha> ListaPosicionadas_ { get => ListaPosicionadas; set => ListaPosicionadas = value; }

    #endregion

    #region METODOS PARA LA LISTA DE FICHAS

    /// <summary>
    /// Retorna una copia de la lista que le llega por parámetro
    /// </summary>
    /// <param name="OldList"></param>
    /// <returns></returns>
    public SortedList<uint,Ficha> CopiaLista(SortedList<uint, Ficha> OldList)
    {
        SortedList<uint, Ficha> Nueva = new SortedList<uint, Ficha>(8);

        for(uint i = 0; i<OldList.Count; i++)
            Agregar(OldList[i], Nueva);

        return Nueva;
    }

    /// <summary>
    /// Agrega a la SortedList la Ficha que le llega por parámetro
    /// </summary>
    /// <param name="Fichita"></param>
    /// <param name="Lista"></param>
    public void Agregar(Ficha Fichita, SortedList<uint, Ficha> Lista)
    {
        if (Fichita is Reina && !Lista.ContainsKey(0))
            Lista.Add(0, new Reina((Reina)Fichita));

        else if (Fichita is Rey && !Lista.ContainsKey(4))
            Lista.Add(4, new Rey((Rey)Fichita));

        else if (Fichita is Alfil)
        {
            if (Fichita.GetName() == "Alfil1" && !Lista.ContainsKey(1))
                Lista.Add(1, new Alfil((Alfil)Fichita));
            else if (!Lista.ContainsKey(2))
                Lista.Add(2, new Alfil((Alfil)Fichita));
        }

        else if (Fichita is Torre)
        {
            if (Fichita.GetName() == "Torre1" && !Lista.ContainsKey(5))
                Lista.Add(5, new Torre((Torre)Fichita));
            else if (!Lista.ContainsKey(6))
                Lista.Add(6, new Torre((Torre)Fichita));
        }

        else if (Fichita is Caballo)
        {
            if (Fichita.GetName() == "Caballo1" && !Lista.ContainsKey(3))
            {
                Lista.Add(3, new Caballo((Caballo)Fichita));
            }
            else if (!Lista.ContainsKey(7))
            {
                Lista.Add(7, new Caballo((Caballo)Fichita));
            }
        }

    }

    #endregion
}

//end Tablero