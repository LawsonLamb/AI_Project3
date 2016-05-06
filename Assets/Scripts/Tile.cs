using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public enum TileType{
	
	Wall =0,
	Path =1,
	Goal = 2,
	Start = 3



}
public enum Action{

	UP =0,
	DOWN =1,
	RIGHT= 2,
	LEFT =3,
	NONE =4,



}
//[ExecuteInEditMode]
public class Tile : MonoBehaviour {
	public List<Sprite> TileSprites;
	public TileType  type;
	Image m_image;
	Text m_Text;
	public Action action;
	// Use this for initialization
	void Start () {
		action = Action.NONE;
		m_Text = transform.GetChild (0).GetComponent<Text> ();
		m_image = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		m_Text = transform.GetChild (0).GetComponent<Text> ();

	//	m_Text.text = "";
		switch (action) {

		case Action.UP:

			m_Text.text = "^";
			break;
		case Action.DOWN:

			m_Text.text = "v";

			break;

		case Action.LEFT:

			m_Text.text = "<";
			break;

		case Action.RIGHT:

			m_Text.text =">";

			break;
		case Action.NONE:

			m_Text.text = "";
			break;


		}

		m_image = GetComponent<Image> ();	
		m_image.color = SwitchColor ();

		//this.GetComponent<SpriteRenderer> ().sprite = SwitchSprites ();

	}


	Sprite SwitchSprites(){


		switch (type) {

		case TileType.Wall:
			return TileSprites [0];





		case TileType.Path:
			return TileSprites [1];
		


		case TileType.Goal:


			return TileSprites [2];
		
		

		}

		return TileSprites [0];
	}

	string SwitchText(){


		switch (type) {

		case TileType.Wall:
			return "W";





		case TileType.Path:
			return "P";



		case TileType.Goal:


			return "G";



		case TileType.Start:
			return "S";


		}

		return " ";



	}

	Color SwitchColor(){



		switch (type) {

		case TileType.Wall:
			return  Color.grey;





		case TileType.Path:
			//Color temp =Color.HSVToRGB (208.0f, 193.0f, 159.0f);

			return  new Color (208.0f/255.0f,193.0f/255.0f,159.0f/255.0f);
				



		case TileType.Goal:
			return  new Color (255.0f/255.0f,205.0f/255.0f,0.0f/255.0f);
		//	return Color.HSVToRGB (255.0f, 204.0f, 0.0f);


		
		case TileType.Start:
			return Color.green;

		}

		return Color.white;


	}
}
