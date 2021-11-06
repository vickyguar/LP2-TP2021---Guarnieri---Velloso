﻿///////////////////////////////////////////////////////////
//  Ficha.cs
//  Implementation of the Class Ficha
//  Generated by Enterprise Architect
//  Created on:      06-oct.-2021 08:09:07
//  Original author: vguar
///////////////////////////////////////////////////////////

/*using System;
using System.Collections.Generic;
using System.Text;
using System.IO;*/
using System.Drawing;

public abstract class Ficha
{
    #region ATRIBUTOS

    /// <summary>
    /// Nombre de la <see cref="Ficha"/>.
    /// </summary>
    protected string Name;

    /// <summary>
    /// Columna de la <see cref="Casilla"/>.
    /// </summary>
    protected int columna;

    /// <summary>
    /// Fila de la <see cref="Casilla"/>
    /// </summary>
    protected int fila;

    /// <summary>
    /// Imagen de <see cref="Ficha"/>.
    /// </summary>
    public Image Imagen;

    #endregion

    #region CONSTRUCTOR & DESTRUCTOR

    /// <summary>
    /// Constructor de la clase <see cref="Ficha"/>.
    /// </summary>
    /// <param name="_Name"></param>
    public Ficha(string _Name, Image _Imagen)
    {
        Name = _Name;
        fila = -1;
        columna = -1;
        Imagen = _Imagen;
    }

    /// <summary>
    /// Constructor por copia de la clase <see cref="Ficha"/>.
    /// </summary>
    /// <param name="newFichita"></param>
    public Ficha(Ficha newFichita)
    {
        Name = newFichita.Name;
        fila = newFichita.Fila;
        columna = newFichita.columna;
        Imagen = newFichita.Imagen;
    }

    /// <summary>
    /// Destructor de la clase <see cref="Ficha"/>.
    /// </summary>
    ~Ficha()
    { }
    #endregion

    #region ATAQUES

    /// <summary>
    /// Método polimórfico de la clase <see cref="Ficha"/>. Indica qué Casillas están siendo atacadas. Por default, el ataque no es Fatal.
    /// </summary>
    /// <param name="Ataque"></param>
    /// <param name="Pos"></param>
    /// <param name="Fatal"></param>
    public abstract void Atacar(Tablero Ataque, Casilla Pos); 

    /// <summary>
    /// Ataca la diagonal ↘ del Tablero Ataque, desde la Casilla que le llega por parámetro. Por default, el ataque no es Fatal.
    /// </summary>
    /// <param name="Ataque"></param>
    /// <param name="Pos"></param>
    /// <param name="Fatal"></param>
    public void Diagonal1(Tablero Ataque, Casilla Pos)
    {
        int j = Pos.GetColumna() + 1;
        int i = Pos.GetFila() + 1;
        bool stop = false;

        while (i < Global.N_ && j < Global.N_)
        {
            Ataque.Matriz[i, j].SetAtacada(true); //Atacamos levemente

            if (!stop)
                Ataque.Matriz[i, j].SetAtacadaFatalmente(true);

            if (Ataque.Matriz[i, j].GetOcupada())
                stop = true; //pinto hasta que me encuentre con una ficha (de manera fatal)

            i++;
            j++;
        } 
    }

    /// <summary>
    /// Ataca la diagonal ↗ del Tablero Ataque, desde la Casilla que le llega por parámetro. Por default, el ataque no es Fatal.
    /// </summary>
    /// <param name="Ataque"></param>
    /// <param name="Pos"></param>
    /// <param name="Fatal"></param>
    public void Diagonal2(Tablero Ataque, Casilla Pos)
    {
        int j = Pos.GetColumna() + 1; //No estamos considerando la posición!
        int i = Pos.GetFila() - 1;
        bool stop = false;

        while (i >= 0 && j < Global.N_)
        {
            Ataque.Matriz[i, j].SetAtacada(true); //Atacamos levemente

            if (!stop)
                Ataque.Matriz[i, j].SetAtacadaFatalmente(true);

            if (Ataque.Matriz[i, j].GetOcupada())
                stop = true; //pinto hasta que me encuentre con una ficha (de manera fatal)

            i--; //Si i es uint, cuando i = 0 y hace i-- nos devuelve un número ALTÍSIMO (y tiene razón jajaja)
            j++;
        }
    }

    /// <summary>
    /// Ataca la diagonal ↖ del Tablero Ataque, desde la Casilla que le llega por parámetro. Por default, el ataque no es Fatal.
    /// </summary>
    /// <param name="Ataque"></param>
    /// <param name="Pos"></param>
    /// <param name="Fatal"></param>
    public void Diagonal3(Tablero Ataque, Casilla Pos)
    {
        int j = Pos.GetColumna() - 1;
        int i = Pos.GetFila() - 1;
        bool stop = false;

        while (i >= 0 && j >= 0)
        {
            Ataque.Matriz[i, j].SetAtacada(true); //Atacamos levemente

            if (!stop)
                Ataque.Matriz[i, j].SetAtacadaFatalmente(true);

            if (Ataque.Matriz[i, j].GetOcupada())
                stop = true; //pinto hasta que me encuentre con una ficha (de manera fatal)

            i--;
            j--;
        }
    }

    /// <summary>
    /// Ataca la diagonal ↙ del Tablero Ataque, desde la Casilla que le llega por parámetro. Por default, el ataque no es Fatal.
    /// </summary>
    /// <param name="Ataque"></param>
    /// <param name="Pos"></param>
    /// <param name="Fatal"></param>
    public void Diagonal4(Tablero Ataque, Casilla Pos)
    {
        int j = Pos.GetColumna() - 1;
        int i = Pos.GetFila() + 1;
        bool stop = false;

        while (i < Global.N_ && j >= 0)
        {
            Ataque.Matriz[i, j].SetAtacada(true); //Atacamos levemente

            if (!stop)
                Ataque.Matriz[i, j].SetAtacadaFatalmente(true);

            if (Ataque.Matriz[i, j].GetOcupada())
                stop = true; //pinto hasta que me encuentre con una ficha (de manera fatal)

            i++;
            j--;
        }
    }

    /// <summary>
    /// Ataca la horizontal → del Tablero Ataque, desde la Casilla que le llega por parámetro. Por default, el ataque no es Fatal.
    /// </summary>
    /// <param name="Ataque"></param>
    /// <param name="Pos"></param>
    /// <param name="Fatal"></param>
    public void Horizontal1(Tablero Ataque, Casilla Pos)
    {
        int j = Pos.GetColumna() + 1;
        int i = Pos.GetFila();
        bool stop = false;

        while (j < Global.N_)
        {
            Ataque.Matriz[i, j].SetAtacada(true); //Atacamos levemente

            if (!stop)
                Ataque.Matriz[i, j].SetAtacadaFatalmente(true);

            if (Ataque.Matriz[i, j].GetOcupada())
                stop = true; //pinto hasta que me encuentre con una ficha (de manera fatal)

            j++;
        }
    }

    /// <summary>
    /// Ataca la horizontal ← del Tablero Ataque, desde la Casilla que le llega por parámetro. Por default, el ataque no es Fatal.
    /// </summary>
    /// <param name="Ataque"></param>
    /// <param name="Pos"></param>
    /// <param name="Fatal"></param>
    public void Horizontal2(Tablero Ataque, Casilla Pos)
    {
        int j = Pos.GetColumna() - 1;
        int i = Pos.GetFila();
        bool stop = false;

        while (j >= 0)
        {
            Ataque.Matriz[i, j].SetAtacada(true); //Atacamos levemente

            if (!stop)
                Ataque.Matriz[i, j].SetAtacadaFatalmente(true);

            if (Ataque.Matriz[i, j].GetOcupada())
                stop = true; //pinto hasta que me encuentre con una ficha (de manera fatal)

            j--;
        }
    }

    /// <summary>
    /// Ataca la vertical ↓ del Tablero Ataque, desde la Casilla que le llega por parámetro. Por default, el ataque no es Fatal.
    /// </summary>
    /// <param name="Ataque"></param>
    /// <param name="Pos"></param>
    /// <param name="Fatal"></param>
    public void Vertical1(Tablero Ataque, Casilla Pos)
    {
        int j = Pos.GetColumna();
        int i = Pos.GetFila() + 1;
        bool stop = false;

        while (i < Global.N_)
        {
            Ataque.Matriz[i, j].SetAtacada(true); //Atacamos levemente

            if (!stop)
                Ataque.Matriz[i, j].SetAtacadaFatalmente(true);

            if (Ataque.Matriz[i, j].GetOcupada())
                stop = true; //pinto hasta que me encuentre con una ficha (de manera fatal)

            i++;
        }
    }

    /// <summary>
    /// Ataca la vertical ↑ del Tablero Ataque, desde la Casilla que le llega por parámetro. Por default, el ataque no es Fatal.
    /// </summary>
    /// <param name="Ataque"></param>
    /// <param name="Pos"></param>
    /// <param name="Fatal"></param>
    public void Vertical2(Tablero Ataque, Casilla Pos)
    {
        int j = Pos.GetColumna();
        int i = Pos.GetFila() - 1;
        bool stop = false;

        while (i >= 0)
        {
            Ataque.Matriz[i, j].SetAtacada(true); //Atacamos levemente

            if (!stop)
                Ataque.Matriz[i, j].SetAtacadaFatalmente(true);

            if (Ataque.Matriz[i, j].GetOcupada())
                stop = true; //pinto hasta que me encuentre con una ficha (de manera fatal)

            i--;
        }
    }

    #endregion

    #region GETTERS & SETTERS

    public string GetName() { return Name; }

    public int Fila { get => fila; set => fila = value; }

    public int Columna { get => columna; set => columna = value; }

    #endregion

} //end Ficha