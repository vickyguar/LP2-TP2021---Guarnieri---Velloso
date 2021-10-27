///////////////////////////////////////////////////////////
//  Alfil.cs
//  Implementation of the Class Alfil
//  Generated by Enterprise Architect
//  Created on:      06-oct.-2021 08:09:07
//  Original author: vguar
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class Alfil : Ficha
{

    #region CONSTRUCTOR & DESTRUCTOR

    /// <summary>
    /// Constructor de la clase <see cref="Alfil"/>.
    /// </summary>
    /// <param name="_Nombre"></param>
    public Alfil(string _Nombre) : base(_Nombre)
    {

    }

    /// <summary>
    /// Destructor de la clase <see cref="Alfil"/>.
    /// </summary>
    ~Alfil()
    {

    }

    #endregion

    #region ATAQUE 

    /// <summary>
    /// Ataque de <see cref="Alfil"/>.
    /// </summary>
    /// <param name="Ataque"></param>
    /// <param name="Pos"></param>
    /// <param name="Fatal"></param>
    public override void Atacar(Tablero Ataque, Casilla Pos)
    {
        Diagonal1(Ataque, Pos);
        Diagonal2(Ataque, Pos);
        Diagonal3(Ataque, Pos);
        Diagonal4(Ataque, Pos);
    }

    #endregion

} //end Alfil