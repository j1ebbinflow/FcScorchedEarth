module Keyboard

open Domain
open OpenTK
open OpenTK.Input

type KeyState = 
    | KeyPressed 
    | KeyReleased

type PairedKeyOptions = 
    | PositiveKey of KeyState
    | NegativeKey of KeyState

type PairedKeyState<'a, 'b> = {
    PositiveKeyState : 'a
    NegativeKeyState : 'a
    Result : 'b
}

let defaultPairedKeyState (defaultResult: 'a) = {PositiveKeyState = KeyReleased; NegativeKeyState = KeyReleased; Result = defaultResult}

let private transformKeyDown = function
    | Key.Escape ->  EndGame
    | _ -> NoChange

let private transformKeyUp = function
    | _ -> NoChange

let private getKey (args: KeyboardKeyEventArgs) = args.Key

let createKeyboardTriggerStream (game : GameWindow) = 

    let keyDownStream = 
        game.KeyDown
        |> Observable.map (getKey >> transformKeyDown)

    let keyUpStream = 
        game.KeyUp
        |> Observable.map (getKey >> transformKeyUp)

    let basicKeyStream = keyDownStream |> Observable.merge keyUpStream
    basicKeyStream
    


