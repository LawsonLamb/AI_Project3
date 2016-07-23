using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Grid : MonoBehaviour {
	public GameObject CellPrefab;
	public Vector2 offset;

	AI.Sarsa sarsa;

		
	public Tile[,] Tiles;



	
	// Use this for initialization
	void Start () {
		MakeImages ();

		// = new AI.Sarsa (Tiles);
		sarsa = new AI.Sarsa (Tiles);


		sarsa.SaveTables ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
           
              


                for(int i =0; i < 10000; i++)
                {
                sarsa.takeStep();
                  }
            sarsa.SaveTables();

              print("DEAD");




            //  print("SAVE");
            //  sarsa.SaveTables();

            /*
			if (sarsa.takeStep ()) {

			//	print ("DEAD");
			}
			*/

        }

		if (Input.GetKeyDown (KeyCode.P)) {

			sarsa.SaveTables ();
		}
		if (Input.GetKeyDown (KeyCode.L)) {

			sarsa.LoadTables ();

		}
	}

	void GetActionsFromSaras(){



	}
	[ContextMenu("take Step")]
	void RunSarsa(){
		sarsa.takeStep ();
		//map [(int)sarsa.oldState.x, (int)sarsa.oldState.y].GetComponent<Tile> ().action = (Action)sarsa.oldAction;
	}


	[ContextMenu("Make Images")]
	void MakeImages(){

		Tiles = new Tile[20, 20];

		StreamReader reader = new StreamReader(@"C:\Users\Ninja\Documents\AI_Project3\Assets\map.txt");


		string currentLine = reader.ReadLine();

		for (int x = 0; x < 20; x++) {
			List<string> world = currentLine.Split (',').ToList ();
			for (int y = 0; y < world.Count-1; y++)
			{
		//	for (int y = 0; y < 20; y++) {

				GameObject go =   (GameObject)	Instantiate (CellPrefab, new Vector2 (0.0f,0.0f), Quaternion.identity);
				go.transform.SetParent (this.transform);
				go.name = "Tile " + x + " , " +y ;

				Tiles [x, y] = go.GetComponent<Tile> ();
				Tiles[x,y].type = (TileType) int.Parse (world [y]);
				go.transform.GetComponent<Tile> ().type = Tiles [x, y].type;



			}


			currentLine = reader.ReadLine();


		}



	}


}
