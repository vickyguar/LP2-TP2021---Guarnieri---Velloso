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

public abstract class Ficha
{
    #region ATRIBUTOS

    /// <summary>
    /// Nombre de la <see cref="Ficha"/>.
    /// </summary>
    protected string Name;

    #endregion

    #region CONSTRUCTOR & DESTRUCTOR

    /// <summary>
    /// Constructor de la clase <see cref="Ficha"/>.
    /// </summary>
    /// <param name="_Name"></param>
    public Ficha(string _Name)
    {
        Name = _Name;
    }

    /// <summary>
    /// Destructor de la clase <see cref="Ficha"/>.
    /// </summary>
    ~Ficha()
    {

    }

    #endregion

    #region ATAQUES

    /// <summary>
    /// Método polimórfico de la clase <see cref="Ficha"/>. Indica qué Casillas están siendo atacadas. Por default, el ataque no es Fatal.
    /// </summary>
    /// <param name="Ataque"></param>
    /// <param name="Pos"></param>
    /// <param name="Fatal"></param>
    public abstract void Atacar(Tablero Ataque, Casilla Pos, bool Fatal = false); //El bool fatal ya no lo necesitaríamos

    /// <summary>
    /// Ataca la diagonal ↘ del Tablero Ataque, desde la Casilla que le llega por parámetro. Por default, el ataque no es Fatal.
    /// </summary>
    /// <param name="Ataque"></param>
    /// <param name="Pos"></param>
    /// <param name="Fatal"></param>
    public void Diagonal1(Tablero Ataque, Casilla Pos, bool Fatal)
    {
        uint j = Pos.GetColumna() + 1;
        uint i = Pos.GetFila() + 1;

        while (i < 8 && j < 8)
        {
            if (!Ataque.Matriz[i, j].GetOcupada())
            {
                Ataque.Matriz[i, j].SetAtacadaFatalmente(true);
            }
            Ataque.Matriz[i, j].SetAtacada(true);

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
    public void Diagonal2(Tablero Ataque, Casilla Pos, bool Fatal)
    {
        uint j = Pos.GetColumna() + 1; //No estamos considerando la posición!
        int i = (int)Pos.GetFila() - 1;


        while (i >= 0 && j < 8)
        {
            Ataque.Matriz[i, j].SetAtacada(true);

            if (Fatal && Ataque.Matriz[i, j].GetOcupada())
                break;

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
    public void Diagonal3(Tablero Ataque, Casilla Pos, bool Fatal)
    {
        int j = (int)Pos.GetColumna() - 1;
        int i = (int)Pos.GetFila() - 1;

        while (i >= 0 && j >= 0)
        {
            Ataque.Matriz[i, j].SetAtacada(true);

            if (Fatal && Ataque.Matriz[i, j].GetOcupada())
                break;

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
    public void Diagonal4(Tablero Ataque, Casilla Pos, bool Fatal)
    {
        int j = (int)Pos.GetColumna() - 1;
        uint i = Pos.GetFila() + 1;

        while (i < 8 && j >= 0)
        {
            Ataque.Matriz[i, j].SetAtacada(true);

            if (Fatal && Ataque.Matriz[i, j].GetOcupada())
                break;

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
    public void Horizontal1(Tablero Ataque, Casilla Pos, bool Fatal)
    {
        uint j = Pos.GetColumna() + 1;
        uint i = Pos.GetFila();

        while (j < 8)
        {
            Ataque.Matriz[i, j].SetAtacada(true);

            if (Fatal && Ataque.Matriz[i, j].GetOcupada())
                break;

            j++;
        }
    }

    /// <summary>
    /// Ataca la horizontal ← del Tablero Ataque, desde la Casilla que le llega por parámetro. Por default, el ataque no es Fatal.
    /// </summary>
    /// <param name="Ataque"></param>
    /// <param name="Pos"></param>
    /// <param name="Fatal"></param>
    public void Horizontal2(Tablero Ataque, Casilla Pos, bool Fatal)
    {
        int j = (int)Pos.GetColumna() - 1;
        uint i = Pos.GetFila();

        while (j >= 0)
        {
            Ataque.Matriz[i, j].SetAtacada(true);

            if (Fatal && Ataque.Matriz[i, j].GetOcupada())
                break;

            j--;
        }
    }

    /// <summary>
    /// Ataca la vertical ↓ del Tablero Ataque, desde la Casilla que le llega por parámetro. Por default, el ataque no es Fatal.
    /// </summary>
    /// <param name="Ataque"></param>
    /// <param name="Pos"></param>
    /// <param name="Fatal"></param>
    public void Vertical1(Tablero Ataque, Casilla Pos, bool Fatal)
    {
        uint j = Pos.GetColumna();
        uint i = Pos.GetFila() + 1;

        while (i < 8)
        {
            Ataque.Matriz[i, j].SetAtacada(true);

            if (Fatal && Ataque.Matriz[i, j].GetOcupada())
                break;

            i++;
        }
    }

    /// <summary>
    /// Ataca la vertical ↑ del Tablero Ataque, desde la Casilla que le llega por parámetro. Por default, el ataque no es Fatal.
    /// </summary>
    /// <param name="Ataque"></param>
    /// <param name="Pos"></param>
    /// <param name="Fatal"></param>
    public void Vertical2(Tablero Ataque, Casilla Pos, bool Fatal)
    {
        uint j = Pos.GetColumna();
        int i = (int)Pos.GetFila() - 1;

        while (i >= 0)
        {
            Ataque.Matriz[i, j].SetAtacada(true);

            if (Fatal && Ataque.Matriz[i, j].GetOcupada())
                break;

            i--;
        }
    }

    #endregion

    #region GETTERS

    public string GetName() { return Name; }

    #endregion

} //end Ficha