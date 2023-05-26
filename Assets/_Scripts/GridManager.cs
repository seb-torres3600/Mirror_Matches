using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GridManager : MonoBehaviour, Subject {

    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform _cam;
    [SerializeField] private Pawn _pawnPrefab;
    [SerializeField] private PawnFactory PF; 
    public static SidePanel redpanel;
    public static SidePanel bluepanel;
    // private List<Command> _executions = List<Command>();
    public List<List<Tile>> _tileGrid = new List<List<Tile>>();
    private Camera m_Camera;
    private Mouse mouse;
    private int melee_pawns = 4; 
    private int pistol_pawns = 4; //hard coding these values for now
    private int rifle_pawns = 4;
	private bool _winCondition = false;
    public static string _winningTeam = "N/A";
	private bool start_game = false;
    private bool isRedTurn = false;
    private bool isBlueTurn = false;
    public bool attackPressed = false;
    public bool movePressed = false;
    public bool waitPressed = false;
    private bool buttonPressed = false;
    private bool position = false;
    private Tile setPosition;
    private Pawn targetPawn;
    private Pawn currentPawn;


    [SerializeField] private List<Pawn> redTeam;
    [SerializeField] private List<Pawn> blueTeam;

    private List<PawnCommand> attackMoves = new List<PawnCommand>();
    private List<PawnCommand> moveMoves = new List<PawnCommand>();
    private List<Observer> observers = new List<Observer>();

    // OBSERVER PATTERN 
    public void addObserver(Observer o){
        this.observers.Add(o);
    }

    public void removeObserver(Observer o){
        this.observers.Remove(o);
    }
    public void notifyObservers(bool redCond, Tuple<bool, double> result, Pawn attacker, Pawn target){
        foreach(var o in observers){
            o.update(redCond, result, attacker, target);
            Debug.Log("Notifying observers");
        }
    }

    void generateGrid(){
        // //pre-initialize grid to make randomization logic easier
        // for(int k = 0; k < _width; k++){
        //     _tileGrid.Add(new List<Tile>(new Tile[_width])); 
        // }

        for(int w = 0; w < _width; w++){
            _tileGrid.Add(new List<Tile>());
            for(int h = 0; h < _height; h++){ //_width and _height are given in Unity editor

                //create a temp Tile object and give it a name
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(w, h), Quaternion.identity);
                spawnedTile.name = $"Tile {w} {h}";

                //decide if new tile is part of a sidePanel 
                if((w <= 3) || (w > (_width-5))){
                    spawnedTile.Init(new Color(0.9f,0.9f,0.9f));
                    _tileGrid[w].Add(spawnedTile);
                }else{
                    //init a Tile with type of sprite (dirt, shrub, crate : 1, 2, 3)
                    // spawnedTile.Init(2);
                    // _tileGrid[w].Add(spawnedTile); 
                    if( (h,w) == (7,7) || (h,w) == (6,7) || (h,w) == (7, _width-8) || (h,w) == (6, _width-8)
                     || (h,w) == (12,14) || (h,w) == (12, 11) || (h,w) == (3, 14) || (h,w) == (3, 11) || (h,w) == (7, 12) || (h,w) == (7, 13)){
                        spawnedTile.Init(3);
                        _tileGrid[w].Add(spawnedTile);
                    }else if( (w-6 == _height - h || w-4 == h+1) && h%2 == 0 ){ //&& w%2 == 0
                        spawnedTile.Init(2);
                        _tileGrid[w].Add(spawnedTile);
                    }else{
                        spawnedTile.Init(1);
                        _tileGrid[w].Add(spawnedTile);
                    }

                }
                
            }
        }
        //set camera position to see the board
        _cam.transform.position = new Vector3(_width/2f - 0.5f, _height/2f - 0.5f, -10);
        
        //create side panels
        //initSidePanels(_width, _height);

    }
    
    // Get the input from when we pick teams
    public void GetPawns()
    {
		// #if !UNITY_EDITOR
		    melee_pawns = PickTeams.melee_input;
		    pistol_pawns = PickTeams.pistol_input;
		    rifle_pawns = PickTeams.rifle_input;
		// #endif
        //spawning all melee pawns
        // PAWN FACTORY
        for(int i = 0; i < melee_pawns; i++){ 
            //red team 
            // PAWN FACTORY
            var spawnedMeleeR = PF.makeRed(1);
            spawnedMeleeR.name = $"Red Melee {i}";
            spawnedMeleeR.transform.position = new Vector3(4, i + _height/2 - melee_pawns/2);
            redTeam.Add(spawnedMeleeR);

            //blue team
            // PAWN FACTORY
            var spawnedMeleeB = PF.makeBlue(1);
            spawnedMeleeB.name = $"Blue Melee {i}";
            spawnedMeleeB.transform.position = new Vector3(_width - 5, i + _height/2 - melee_pawns/2);
            blueTeam.Add(spawnedMeleeB);
        }

        //spawning all pistol pawns
        // PAWN FACTORY
        for(int i = 0; i < pistol_pawns; i++){
            //red team 
            // PAWN FACTORY
            var spawnedMeleeR = PF.makeRed(2);
            spawnedMeleeR.name = $"Red Pistol {i}";
            spawnedMeleeR.transform.position = new Vector3(5, i + _height/2 - pistol_pawns/2);
            redTeam.Add(spawnedMeleeR);

            //blue team
            // PAWN FACTORY
            var spawnedMeleeB = PF.makeBlue(2);
            spawnedMeleeB.name = $"Blue Pistol {i}";
            spawnedMeleeB.transform.position = new Vector3(_width - 6, i + _height/2 - pistol_pawns/2);
            blueTeam.Add(spawnedMeleeB);
        }

        //spawning all rifle pawns
        // PAWN FACTORY
        for(int i = 0; i < rifle_pawns; i++){
            //red team 
            // PAWN FACTORY
            var spawnedMeleeR = PF.makeRed(3);
            spawnedMeleeR.name = $"Red Rifle {i}";
            spawnedMeleeR.transform.position = new Vector3(6, i + _height/2 - rifle_pawns/2);
            redTeam.Add(spawnedMeleeR);

            //blue team
            // PAWN FACTORY
            var spawnedMeleeB = PF.makeBlue(3);
            spawnedMeleeB.name = $"Blue Rifle {i}";
            spawnedMeleeB.transform.position = new Vector3(_width - 7, i + _height/2 - rifle_pawns/2);
            blueTeam.Add(spawnedMeleeB);
        }
    }
    //to check if win condition met
    void checkWin(List<Pawn> reds, List<Pawn> blues){
        
        bool blueLoses = true;
        foreach(Pawn p in blues){
            if(p.health > 0){ blueLoses = false; }
        }
        
        bool redLoses = true;
        foreach (Pawn p in reds){
            if(p.health > 0){
                redLoses = false;
            }
        }

        if(redLoses){
            _winningTeam = "Blue";
            _winCondition = true;
        }else if(blueLoses){
            _winningTeam = "Red";
            _winCondition = true;
        }else{
            _winCondition = false;
        }
        // Debug.Log(_winCondition);
    }

    IEnumerator getTurns(){
        //loop through team
        //add each command to a list of commands
        //execute each command in a different function        
        //while win condition hasn't been met
        while (!_winCondition)
        {
            // red turn first
            bluepanel.NotTurn();
            redpanel.Turn();
            // go through team
            foreach (Pawn p in redTeam)
            {
                // if pawn has no health skip it 
                if(p.health <= 0){
                    continue;
                }
                currentPawn = p;
                position = false;
                redpanel.imageComponent.sprite = p._renderer.sprite;
                redpanel.textComponent.text = p.name + "\nHealth: " + p.health + "/100 \n"; 
                // Set background color of sprite to black to highlight pawn being used
                p._renderer.color = Color.black;
                // while no button/action has been taken skip frame
                while (!attackPressed && !movePressed && !waitPressed)
                {
                    yield return null; // skip frame
                }
                // once we exit the loop we notify that we can start looking 
                // for mouse clicks
                buttonPressed = true;
                // yield return null;
                // find which button was pressed
                // put in while loop until they press a square tomove/attack
                if (attackPressed)
                {   
                    // stuck in while loop until we get a position with the mouse
                    // Sets the pawn we want to attack
                    while (!position)
                    {
                        yield return null;
                    }
                    // COMMAND PATTERN
                    attackMoves.Add(new PawnCommand(p, targetPawn));
                    // reset attackPresed for next pawn
                    attackPressed = false;
                }
                // if we want to move
                if (movePressed)
                {
                    // wait until we get a mouse position
                    // in this case we're getting a tile object
                    while (!position)
                    {
                        yield return null;
                    }
                    // COMMAND PATTERN
                    moveMoves.Add(new PawnCommand(p,setPosition));
                    // reset for next turn
                    movePressed = false;
                }
                // skip turn if we want to wait on the specific pawn
                if (waitPressed)
                {
                    waitPressed = false;
                }
                // set pawn color back to original
                p._renderer.color = Color.white;
            }
            isRedTurn = false;

            //blue turn
            bluepanel.Turn();
            redpanel.NotTurn();
            // go through team
            foreach (Pawn p in blueTeam)
            {
                 if(p.health <= 0){
                    continue;
                }
                currentPawn = p;
                position = false;
                bluepanel.imageComponent.sprite = p._renderer.sprite;
                bluepanel.textComponent.text = p.name + "\nHealth: " + p.health + "/100 \n"; 
                // Set background color of sprite to black to highlight pawn being used
                p._renderer.color = Color.black;
                // while no button/action has been taken skip frame
                while (!attackPressed && !movePressed && !waitPressed)
                {
                    yield return null; // skip frame
                }
                // once we exit the loop we notify that we can start looking 
                // for mouse clicks
                buttonPressed = true;
                // yield return null;
                // find which button was pressed
                // put in while loop until they press a square tomove/attack
                if (attackPressed)
                {   
                    // stuck in while loop until we get a position with the mouse
                    // Sets the pawn we want to attack
                    while (!position)
                    {
                        yield return null;
                    }
                    
                    // COMMAND PATTERN
                    attackMoves.Add(new PawnCommand(p, targetPawn));
                    
                    // reset attackPresed for next pawn
                    attackPressed = false;
                }
                // if we want to move
                if (movePressed)
                {
                    // wait until we get a mouse position
                    // in this case we're getting a tile object
                    while (!position)
                    {
                        yield return null;
                    }
                    // COMMAND PATTERN
                    moveMoves.Add(new PawnCommand(p,setPosition));
                    movePressed = false;
                }
                // skip turn if we want to wait on the specific pawn
                if (waitPressed)
                {
                    waitPressed = false;
                }
                // set pawn color back to original
                p._renderer.color = Color.white;
            }

            isBlueTurn = false;
            bluepanel.NotTurn();
            redpanel.NotTurn();
            yield return null;
            executeTurns();
        }
        // return when win condition is met
        yield return new WaitUntil(() => _winCondition);
    }

    void executeTurns()
    {
        foreach (var moves in moveMoves)
        {
            Pawn p = moves.actor;
            Tile t = moves.location;
            if(p.move(t))
            {
                _tileGrid[(int)p.transform.position.x][(int)p.transform.position.y].occupied = false;
                p.transform.position = t.transform.position;
                t.occupied = true;
            }
            t._renderer.color = Color.white;
        }

        foreach (var moves in attackMoves)
        {
            Pawn p = moves.actor;
            Pawn p_two = moves.target;

            if(p.health <= 0){ //check if pawn was killed this turn
                continue;
            }

            Tuple<bool, double> res = p.attack(p_two, _tileGrid);
            if(p_two.health <= 0){ //if target died...
                //move it to the graveyard
                p_two.transform.position = new Vector3(-10,-10);
            }
            //log result info
            // OBSERVER PATTERN
            this.notifyObservers(p.isredpawn, res, p, p_two);
        }
        attackMoves = new List<PawnCommand>(); 
        moveMoves = new List<PawnCommand>();
    }
    
    // Start is called before the first frame update
    void Start()
    {   
        m_Camera = Camera.main;
        mouse = Mouse.current;
        GetPawns();
        generateGrid();
        redpanel = new SidePanel(_width, _height,1);
        bluepanel = new SidePanel(_width, _height,0);
        _winCondition = false;
        redpanel.CreateTurn(1,(float)_width,(float)_height);
        bluepanel.CreateTurn(0,(float)_width,(float)_height);
        StartCoroutine(getTurns());
        this.addObserver(new RedLogger());
        this.addObserver(new BlueLogger());
    }

    // Update is called once per frame
    void Update()
    {
        checkWin(redTeam, blueTeam);
        if(_winCondition){
            // Time.timeScale = 0;
            SceneManager.LoadScene("Winner");
        }else{
            isRedTurn = true;
            isBlueTurn = true;
            
            // https://learn.unity.com/tutorial/onmousedown#
            // if we have pressed a button, look for a button click
            if (mouse.leftButton.wasPressedThisFrame && buttonPressed) //this line is giving null reference exception
             {
                 Vector3 mousePosition = mouse.position.ReadValue();
                 Ray ray = m_Camera.ScreenPointToRay(mousePosition);
                 // get object being hit with mouse
                 if (Physics.Raycast(ray, out RaycastHit hit))
                 {
                         // Debug.Log(hit.collider.gameObject.name);
                         if (hit.collider.GetComponentInParent<Tile>() != null && movePressed)
                         {
                             setPosition = hit.collider.GetComponentInParent<Tile>();
                             // if move is valid, set tile to green highlight
                             // and notify that a move has been made
                            if(currentPawn.move(setPosition)){
                                setPosition._renderer.color = Color.green;
                                position = true;
                            }
                         }

                         if (hit.collider.GetComponentInParent<Pawn>() != null && attackPressed)
                         {
                            targetPawn = hit.collider.GetComponentInParent<Pawn>();
                            position = true;
                         }
                         // Use the hit variable to determine what was clicked on.
                 }
             }
             
        }

    }
}
