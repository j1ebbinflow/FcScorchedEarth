module DomainTransitions

open Domain

let extractState (action : StateAction) = 
    match action with 
    | UpdateState state | RenderFrame state | UpdateFrame state -> state

let updateStateWithTime elapsed (oldState: GameState) = { oldState with TimeSinceLastUpdate = elapsed }

let updateGameState stateAction change = 
    let state = extractState stateAction
    let newState = 
        match change with
        | AspectRatioUpdate newRatio -> {state with AspectRatio = newRatio}
        | TimeUpdate elapsedSeconds-> updateStateWithTime elapsedSeconds state  
        | EndGame -> {state with Running=Stop}
        | NoChange -> state
    UpdateState newState

let processGameAction stateAction action = 
    match action with
    | TriggerRenderFrame -> RenderFrame <| extractState stateAction
    | TriggerUpdateFrame -> UpdateFrame <| extractState stateAction

let processGameEvent (stateAction: StateAction) (gameEvent: GameEvent) = 
    match gameEvent with
    | StateChange change -> updateGameState stateAction change
    | GameAction action -> processGameAction stateAction action