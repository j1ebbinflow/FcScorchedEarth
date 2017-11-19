module Domain

open Physics
open TriangleSprite

type GameRunning =
    | Continue
    | Stop   

type GameState = {
    Running : GameRunning
    AspectRatio : float
    TriangleSprite : TriangleSprite
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
    TimeSinceLastUpdate = 0.<s>
}



