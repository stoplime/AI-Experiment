using System;
using System.Collections.Generic;
using System.IO;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Input;

namespace GameAlpha
{
	public class InputPlayer
	{
		public StreamWriter sw = null;
		
		private int counter;
		
		public InputPlayer ()
		{
			counter = 0;
			try{
				sw = new StreamWriter("Documents/PlayerInputs.txt");
				
			}catch(FileNotFoundException){
				File.CreateText ("Documents/PlayerInputs.txt");
				sw = new StreamWriter("Documents/PlayerInputs.txt");
			}

		}
		
		public void Update (GamePadData gpd)
		{
			if (counter >= 10) {
				counter = 0;
				sw.WriteLine("");
			}
			
			List<Input> currentInput = ReadInputs (gpd);
			
			for (int i = 0; i < currentInput.Count; i++) {
				sw.Write(currentInput[i].ToString("d"));
				if (i < currentInput.Count-1) {
					sw.Write(",");
				}
			}
			if (currentInput.Count > 0) {
				sw.Write(";");
				counter++;
			}
			
		}
		
		public List<Input> ReadInputs (GamePadData gamePadData)
		{
			List<Input> L = new List<Input>();
			if((gamePadData.Buttons & GamePadButtons.Left) != 0){
				L.Add(Input.Left);
			}
			if((gamePadData.Buttons & GamePadButtons.Right) != 0){
				L.Add(Input.Right);
			}
			if((gamePadData.Buttons & GamePadButtons.Up) != 0){
				L.Add(Input.Up);
			}
			if((gamePadData.Buttons & GamePadButtons.Down) != 0){
				L.Add(Input.Down);
			}
			if((gamePadData.Buttons & GamePadButtons.R) != 0){
				L.Add(Input.R);
			}
			if((gamePadData.Buttons & GamePadButtons.L) != 0){
				L.Add(Input.L);
			}
			if((gamePadData.Buttons & GamePadButtons.Cross) != 0){
				L.Add(Input.Cross);
			}
			if((gamePadData.Buttons & GamePadButtons.Circle) != 0){
				L.Add(Input.Circle);
			}
			if((gamePadData.Buttons & GamePadButtons.Triangle) != 0){
				L.Add(Input.Triangle);
			}
			if((gamePadData.Buttons & GamePadButtons.Square) != 0){
				L.Add(Input.Square);
			}
			if((gamePadData.Buttons & GamePadButtons.Start) != 0){
				L.Add(Input.Start);
			}
			if((gamePadData.Buttons & GamePadButtons.Select) != 0){
				L.Add(Input.Select);
			}
			return L;
		}
		
		public void Dispose ()
		{
			if (sw != null) {
				sw.Close();
			}
		}
	}
}

