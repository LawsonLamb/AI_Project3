using UnityEngine;
using System.Collections;

public class State{
	public TileType m_type;
	public double[] actions;
	int row;
	int col;
	public int Reward;
	public	State(TileType Type){
		m_type = Type;
		actions =  new double [4];
		initActionsRandom ();
		Reward = getReward ();

	}
	public State(){
		
		actions = new double[4];
		initActionsZero ();


	}


	//SET reward Based on Tiletype
	public int getReward(){

		switch (m_type) {

		case TileType.Wall:

			return 0;



		case TileType.Path:

			return 1;


		case TileType.Goal:
			return 2;




		}

		return 0;

	}

	// sets Actions to small random number used for q table
	public double[] initActionsRandom(){

		System.Random  ran = new System.Random ();
	
		for (int i = 0; i < actions.Length; i++) {

			actions[i] = ran.NextDouble () * 0.09 + 0.01;

		}
		return actions;
	}

	// sets actions to Zero for Etabel;
	public double[] initActionsZero(){

		for (int i = 0; i < actions.Length; i++) {

			actions [i] = 0.0;
		}
		return actions;

	}

	public int getRandomAction(){


		return Random.Range (0, 4);
	}

	public int bestAction(){

		int best = 0;

		for (int i = 0; i < actions.Length; i++) {

			if (actions [best] < actions [i]) {

				best = i;
			}

		}
		return best;

	}

	public int ChooseAction(double epslilon){

		if (Random.Range (0, 1) > epslilon) {
			
			return Random.Range (0, 4);
		} else
			return bestAction ();





	}



}
