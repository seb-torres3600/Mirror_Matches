## MirrorMatch game

- This was a Unity game that was a simple shooter game.
- Project for OOAD Class

## Names of Team Members:
- Jack Ashburn
- Sebatian Torres

## Language Version:
- C# 9.0 (standard to Unity version 2021.3.4f1, which we used)

## GAMERULES
- general:
    - the number of each pawn is limited to 6. If you enter more than 6, the game will not start.
    - pawns currently being given a command will be shaded black.
    - Once all the pawns on a team die, that team loses.
    - All commands are given to both teams' pawns, then are executed simultaneously after all commands have been received.
        - All move commands will execute before any attack commands.
    - If you install Unity (version listed below), start the game from the StartPage scene. 
- movement:
    - after choosing move, select a destination tile.
    - cannot have destination be inside
    a shrub or crate.
    - If you select an invalid move tile, you will be forced to select again. 
    Once you select a valid Tile, it will light up green to show that it has been accepted.
    - Melee pawns can move 3 tiles, pistol pawns can move 2, and rifles 1 tile (any direction, like the king in chess).
- shooting:
    - after choosing an attack option, click a pawn.
    - if the pawn is out of range, the accuracy will be 0%. Don't waste an attack!
- cover effectiveness and hit %:
    - base chance of hitting shot: 
        1-((distance to target / weapon range) * 0.75)
        //at max range: 0.25% base chance
    - if target is behind cover:
        (base chance * (1 - coverEffectiveness))
        //crate almost cuts hit % in half with cE = 0.56 
        //shrub has cE = 0.22 making accuracy 78% of base 
- when does the cover bonus apply?
    - if a Pawn is adjacent to a Tile that provides cover, then
    the cover bonus is applied for all shots incoming from 
    the opposite side of the cover Tile.
        - ex: a Pawn on the west side of a Crate gets the cover bonus
        for all shots coming from east of the crate (regardless of 
        the incoming shot's north/south positioning).