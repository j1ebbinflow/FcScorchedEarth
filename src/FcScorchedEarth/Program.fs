open OpenTK
open OpenTK.Graphics
open Domain

[<EntryPoint>]
let main _ = 
    let getAspectRatio (gw : GameWindow) = float gw.Width / float gw.Height
    use game = new GameWindow(700, 700, GraphicsMode.Default, "Scorched Earth Clone")

    let load _ = GameConfig.onLoadSetup game

    let resize _ = GameConfig.onResizeSetup game

    let updateFrame (state: GameState) = 
        match state.Running with 
        | Continue -> ()
        | Stop -> game.Exit()

    let renderFrame (state: GameState)  = 
        GameConfig.preRenderConfigure()
        Render.renderState state
        game.SwapBuffers() 

    let updatedStateStream = 
        let stateTransitionTriggerStream = 
            let timeUpdateStream = game.UpdateFrame |> Observable.map (fun args -> Domain.TimeUpdate <| LanguagePrimitives.FloatWithMeasure args.Time)
            let aspectRatioStream = game.Resize |> Observable.map (fun _ -> Domain.AspectRatioUpdate <| getAspectRatio game)
            let keyboardTriggerStream = Keyboard.createKeyboardTriggerStream game 

            keyboardTriggerStream
            |> Observable.merge timeUpdateStream 
            |> Observable.merge aspectRatioStream 
            |> Observable.map GameEvent.StateChange
        
        let gameOutputTriggerStream = 
            let renderFrameTriggerStream = game.RenderFrame |> Observable.map (fun _ -> GameActionTrigger.TriggerRenderFrame)
            let updateFrameTriggerStream = game.UpdateFrame |> Observable.map (fun _ -> GameActionTrigger.TriggerUpdateFrame)

            renderFrameTriggerStream 
            |> Observable.merge updateFrameTriggerStream 
            |> Observable.map GameEvent.GameAction

        stateTransitionTriggerStream 
        |> Observable.merge gameOutputTriggerStream        
        |> Observable.scan DomainTransitions.processGameEvent (StateAction.UpdateState <| Domain.initialGameState)

    use renderFrameSubscription = 
        updatedStateStream
        |> Observable.choose(function | StateAction.RenderFrame state -> Some(state) | _ -> None)
        |> Observable.subscribe(fun state -> renderFrame state)

    use updateFrameSubscription = 
        updatedStateStream
        |> Observable.choose(function | StateAction.UpdateFrame state -> Some(state) | _ -> None)
        |> Observable.subscribe(fun state -> updateFrame state)

    use loadSubscription = game.Load.Subscribe load
    use resizeSubscription = game.Resize.Subscribe resize  
        
    game.Run(60.0)
    0 
