module Domain

open Physics
open TriangleSprite
open Terrain
open TankSprite

type GameRunning =
    | Continue
    | Stop   

type GameState = {
    Running : GameRunning
    AspectRatio : float
    TriangleSprite : TriangleSprite
    PlayerTank : TankSprite
    TargetTank : TankSprite
    Terrain : Terrain
    TimeSinceLastUpdate : float<s>
}

type StateChangeTrigger = 
    | EndGame
    | TimeUpdate of float<s>
    | AspectRatioUpdate of float
    | NoChange

type GameActionTrigger = 
    | TriggerRenderFrame
    | TriggerUpdateFrame

type GameEvent = 
    | StateChange of StateChangeTrigger
    | GameAction of GameActionTrigger

type StateAction = 
    | UpdateState of GameState
    | RenderFrame of GameState
    | UpdateFrame of GameState

let initialGameState = { 
    Running = Continue
    AspectRatio = 1.0
    TriangleSprite = TriangleSprite.initialTriangle
    PlayerTank = TankSprite.create {X = 1.; Y = -1.5;} {R=0.0; G=0.0; B=1.0} 
    TargetTank = TankSprite.create {X = -1.; Y = -1.5;} {R=1.0; G=0.0; B=0.0} 
    Terrain = Terrain.initialTerrain
    TimeSinceLastUpdate = 0.<s>
}



