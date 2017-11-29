module TankSprite

open Geometry

type TankSprite = {
    Position: Point2d
    Body : ColoredPoint2d list
}

let create position colour =
    {
        Position = position
        Body = 
            [
                {Color=colour; Point= {X = -0.1; Y = 0.1;}}
                {Color=colour; Point= {X = 0.1; Y = 0.1;}}
                {Color=colour; Point= {X = 0.1; Y = -0.1;}}
                {Color=colour; Point= {X = -0.1; Y = -0.1;}}
            ]
    }


