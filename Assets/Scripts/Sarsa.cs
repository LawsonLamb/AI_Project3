using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace AI{
	
public class Sarsa{

	double alpha = 0.4;
	double gamma = 0.7;
	double lambda =0.9;
	double epsilon =0.9;
	double delta =0;
	
	public Vector2 oldState;
		public 	int oldAction;
	int reward;
	public Vector2 currentState;
	public int currentAction;

		public Tile[,] Tiles = new Tile[20, 20];
	// Q table
	//
	public State[,] Qtable = new State[20,20];

	public State[,] Etable = new State[20, 20];

	public Sarsa(TileType[,] tiles){


		// FILL QTABLE WITH S,A

		for (int x = 0; x < 20; x++) {
			for (int y = 0; y < 20; y++) {

				Qtable [x, y] = new State (tiles [x, y]);
					Etable [x, y] = new State ();

			}



		}

		
	}
		public Sarsa(Tile[,] tiles){

			for (int x = 0; x < 20; x++) {
				for (int y = 0; y < 20; y++) {

					Qtable [x, y] = new State (tiles [x, y].type);
					Etable [x, y] = new State ();
					Tiles [x, y] = tiles [x, y];

				}



			}
			LoadTables ();


		}



	public bool takeStep(){
			ResetTiles ();
			oldState.x = (int)Random.Range (0, 19);
			oldState.y = (int)Random.Range (0, 19);
		while (Qtable [(int)oldState.x, (int)oldState.y].m_type == TileType.Path) {
				// RESET TILE ACTIONS TO NONE
			currentState = move (oldState);
			currentAction = getStateinQTable (currentState).ChooseAction (epsilon);
			reward = getStateinQTable (currentState).getReward ();
			delta = getDelta ();
			Etable [(int)oldState.x, (int)oldState.y].actions [oldAction] += 1;
			// for all or 

			Qtable [(int)oldState.x, (int)oldState.y].actions [oldAction] = UpdateQtable (oldAction, oldState);
			Etable [(int)oldState.x, (int)oldState.y].actions [oldAction] = UpdateEtable (oldAction, oldState);
			oldState = currentState;
			oldAction = currentAction;
			Tiles [(int)oldState.x, (int)oldState.y].action = (Action)oldAction;

		}
			if (epsilon > .01) {
				epsilon-= 0.0001;

			}
			//SaveTables ();
		return true;
	}

	double UpdateQtable(int action,Vector2 state){


		//Qtable [currentState.x, currentState.y].actions [currentAction]
		//+= alpha * delta * Etable [currentState.x, currentState.y].actions [currentAction];

		return Qtable [(int)state.x, (int)state.y].actions [oldAction] += alpha * delta * Etable [(int)state.x, (int)state.y].actions [action];


		


	}
	double UpdateEtable(int action ,Vector2 state){
		return gamma * lambda * Etable [(int)state.x, (int)state.y].actions [action];

	}
	double getDelta(){

		return reward + (gamma * (Qtable[(int)currentState.x,(int)currentState.y].actions[currentAction] - Qtable[(int)oldState.x,(int)oldState.y].actions[oldAction] ));

	}

	State getStateinQTable(Vector2 state){

		return Qtable [(int)state.x, (int)state.y];

	}


	
		// set all episodes (s,a)  = 0



	Vector2 move(Vector2 oldState){

		//UP
		if (oldAction == 0) {

			return new Vector2 (oldState.x - 1, oldState.y);

		}
		//DOWN

		else if (oldAction == 1) {


			return new Vector2 (oldState.x + 1, oldState.y);
		} else if (oldAction == 2) {


			return new Vector2 (oldState.x, oldState.y + 1);
		} else if (oldAction == 3) {
			return new Vector2 (oldState.x, oldState.y - 1);


		} else {

			return new Vector2 (0, 0);
		}


	}


		void ResetTiles(){


			for (int x = 0; x < 20; x++) {
				for (int y = 0; y < 20; y++) {


					Tiles [x, y].action = Action.NONE;

				}

			}
		}

   


	public void SaveTables(){


			StreamWriter Qwriter = new StreamWriter ("/Users/ItBNinja/AI_Project3/Assets/QTable.txt");
			StreamWriter Ewriter = new StreamWriter ("/Users/ItBNinja/AI_Project3/Assets/ETable.txt");
				

			Qwriter.WriteLine (epsilon.ToString());

		//	string currentLine = reader.ReadLine ();

			string Qline = "";
			string Eline = "";
			for (int x = 0; x < 20; x++) {
				
				for (int y = 0; y < 20 ;y ++) {

					for (int z = 0; z < 4; z++) {

						Qline += Qtable [x, y].actions [z].ToString () + " , ";
						Eline += Etable [x, y].actions [z].ToString () + " , ";
					}

					Qwriter.WriteLine (Qline);
					Ewriter.WriteLine (Eline);

					Qline = "";
					Eline = "";

				}
			}
		

			Qwriter.Close ();
			Ewriter.Close ();
		}


	public void LoadTables(){

			StreamReader Qreader = new StreamReader ("/Users/ItBNinja/AI_Project3/Assets/QTable.txt");
			StreamReader Ereader = new StreamReader ("/Users/ItBNinja/AI_Project3/Assets/ETable.txt");


			string QLine = Qreader.ReadLine ();
			string ELine = "";
			epsilon = double.Parse(QLine);
			for (int x = 0; x < 20; x++) {
				for (int y = 0; y < 20; y++){
					
				QLine = Qreader.ReadLine ();
				ELine = Ereader.ReadLine ();
				List<string> qList =   QLine.Split (',').ToList ();
				List<string> eList =   ELine.Split (',').ToList ();
				for (int z = 0; z < qList.Count - 1; z++) {

						Qtable [x, y].actions [z] = double.Parse(qList [z]);
						Etable [x, y].actions [z] = double.Parse (eList [z]);


				}

			}
		}
			Qreader.Close ();
			Ereader.Close ();


		}




  









}
}
