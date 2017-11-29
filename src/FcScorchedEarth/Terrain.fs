module Terrain

open Geometry

type Terrain = { 
    Body : ColoredPoint2d * ColoredPoint2d
} 

let initialTerrain =
    {
        Body = {Color={R=0.2; G=0.8; B=0.0}; Point= {X = -2.; Y = -1.5;}}, {Color={R=0.2; G=0.8; B=0.0}; Point= {X = 2.; Y = -1.5;}}
    }
