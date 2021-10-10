﻿///////////////////////////////////////////////////////////
//  Ficha.cs
//  Implementation of the Class Ficha
//  Generated by Enterprise Architect
//  Created on:      06-oct.-2021 08:09:07
//  Original author: vguar
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



public abstract class Ficha
{
    protected string Nombre;
    protected Casilla Pos;
    //protected Casilla Posicion;

    public bool Ocupar()
    {
        if (!Pos.GetOcupada() && !(this is Torre)) //el dynamic_cast en C#
            return false; //si estoy ocupada, no se puede posicionar una ficha

        Pos.SetOcupada(true); //la casilla estaba desocupada, entonces la ocupo
        return true; //la pude ocupar
    }

    public Ficha(string _Nombre)
    {
        Nombre = _Nombre;
        Pos = null; //Inicializamos en NULL
    }

    ~Ficha()
    {

    }

    public abstract void Atacar(Tablero Ataque);

    public void Diagonal1(Tablero Ataque) // ↘ 
    {
        uint j = Pos.GetColumna() + 1;
        uint i = Pos.GetFila() + 1;

        do
        {
            Ataque.Matriz[i, j].SetAtacada(true);
            i++;
            j++;
        } while (i < 8 || j < 8);
    }

    public void Diagonal2(Tablero Ataque) // ↗ 
    {
        uint j = Pos.GetColumna() + 1;
        uint i = Pos.GetFila() - 1;

        do
        {
            Ataque.Matriz[i, j].SetAtacada(true);
            i--;
            j++;
        } while (i >= 0 || j < 8);
    }

    public void Diagonal3(Tablero Ataque) // ↖ 
    {
        uint j = Pos.GetColumna() - 1;
        uint i = Pos.GetFila() - 1;

        do
        {
            Ataque.Matriz[i, j].SetAtacada(true);
            i--;
            j--;
        } while (i >= 0 || j >= 0);
    }

    public void Diagonal4(Tablero Ataque) // ↙ 
    {
        uint j = Pos.GetColumna() - 1;
        uint i = Pos.GetFila() + 1;

        do
        {
            Ataque.Matriz[i, j].SetAtacada(true);
            i++;
            j--;
        } while (i < 8 || j >= 8);
    }

    public void Horizontal1(Tablero Ataque) // → 
    {
        uint j = Pos.GetColumna() + 1;
        uint i = Pos.GetFila();

        do
        {
            Ataque.Matriz[i, j].SetAtacada(true);
            j++;
        } while (j < 8);
    }

    public void Horizontal2(Tablero Ataque) // ← 
    {
        uint j = Pos.GetColumna() - 1;
        uint i = Pos.GetFila();

        do
        {
            Ataque.Matriz[i, j].SetAtacada(true);
            j--;
        } while (j >= 0);
    }

    public void Vertical1(Tablero Ataque) // ↓ 
    {
        uint j = Pos.GetColumna();
        uint i = Pos.GetFila() + 1;

        do
        {
            Ataque.Matriz[i, j].SetAtacada(true);
            i++;
        } while (i < 8);
    }

    public void Vertical2(Tablero Ataque) // ↑ 
    {
        uint j = Pos.GetColumna();
        uint i = Pos.GetFila() - 1;

        do
        {
            Ataque.Matriz[i, j].SetAtacada(true);
            i--;
        } while (i >= 0);
    }

    //public Posicion GetPos() { return Pos; }

}//end Ficha