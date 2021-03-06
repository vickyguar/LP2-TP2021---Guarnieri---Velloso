///////////////////////////////////////////////////////////
//  Torre.cs
//  Implementation of the Class Torre
//  Generated by Enterprise Architect
//  Created on:      06-oct.-2021 08:09:08
//  Original author: vguar
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

public class Torre : Ficha
{

    #region CONSTRUCTOR & DESTRUCTOR

    /// <summary>
    /// Constructor de la clase <see cref="Torre"/>.
    /// </summary>
    /// <param name="_Nombre"></param>
    public Torre(string _Nombre) : base(_Nombre, Image.FromFile("Torre.png"))
    {

    }

    /// <summary>
    /// Destructor de la clase <see cref="Torre"/>.
    /// </summary>
    ~Torre()
    {

    }

    #endregion

    #region ATAQUE 

    /// <summary>
    /// Ataque de <see cref="Torre"/>.
    /// </summary>
    /// <param name="Ataque"></param>
    /// <param name="Pos"></param>
    /// <param name="Fatal"></param>
    public override void Atacar(Tablero Ataque, Casilla Pos)
    {
        Horizontal1(Ataque, Pos);
        Horizontal2(Ataque, Pos);
        Vertical1(Ataque, Pos);
        Vertical2(Ataque, Pos);
    }

    #endregion

} //end Torre