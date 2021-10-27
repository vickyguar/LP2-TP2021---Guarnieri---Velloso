///////////////////////////////////////////////////////////
//  Rey.cs
//  Implementation of the Class Rey
//  Generated by Enterprise Architect
//  Created on:      06-oct.-2021 08:09:07
//  Original author: vguar
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class Rey : Ficha
{
    #region CONSTRUCTOR & DESTRUCTOR

    /// <summary>
    /// Constructor de la clase <see cref="Rey"/>.
    /// </summary>
    /// <param name="_Nombre"></param>
    public Rey(string _Nombre) : base(_Nombre)
    {

    }

    /// <summary>
    /// Destructor de la clase <see cref="Rey"/>.
    /// </summary>
    ~Rey()
    {

    }

    #endregion

    #region ATAQUE 

    /// <summary>
    /// Ataque de <see cref="Rey"/>.
    /// </summary>
    /// <param name="Ataque"></param>
    /// <param name="Pos"></param>
    /// <param name="Fatal"></param>
    public override void Atacar(Tablero Ataque, Casilla Pos)
    {
        uint i = Pos.GetFila();
        uint j = Pos.GetColumna();

        //No verificamos si lo que pinta est� fuera del Tablero, porque nosostras le restringimos al Rey a posicionarse en el Cuadrado3 (ver Main)

        Ataque.Matriz[i + 1, j].SetAtacada(true);
        Ataque.Matriz[i - 1, j].SetAtacada(true);
        Ataque.Matriz[i, j + 1].SetAtacada(true);
        Ataque.Matriz[i, j - 1].SetAtacada(true);
        Ataque.Matriz[i + 1, j + 1].SetAtacada(true);
        Ataque.Matriz[i + 1, j - 1].SetAtacada(true);
        Ataque.Matriz[i - 1, j + 1].SetAtacada(true);
        Ataque.Matriz[i - 1, j - 1].SetAtacada(true);

    }

    #endregion

} //end Rey