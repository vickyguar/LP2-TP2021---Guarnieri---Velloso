///////////////////////////////////////////////////////////
//  Reina.cs
//  Implementation of the Class Reina
//  Generated by Enterprise Architect
//  Created on:      06-oct.-2021 08:09:07
//  Original author: vguar
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



public class Reina : Ficha {

	public Reina(string _Nombre): base(_Nombre)
	{

	}

	~Reina(){

	}

	public override void Atacar(Tablero Ataque)
	{
		Diagonal1(Ataque);
		Diagonal2(Ataque);
		Diagonal3(Ataque);
		Diagonal4(Ataque);
		Horizontal1(Ataque);
		Horizontal2(Ataque);
		Vertical1(Ataque);
		Vertical2(Ataque);
	}

}//end Reina