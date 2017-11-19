module TriangleSprite

open Geometry

type TriangleSprite = { 
    Position: Point2d
    BodyWrtOrigin : ColoredTriangle2d
    Heading: float<degree>
} 

let initialTriangle =     
    { 
        Position = Physics.neutralPosition
        BodyWrtOrigin = 
            {
            P1 ={Color={R=0.2; G=0.9; B=1.0}; Point= {X = 0.0; Y = 0.1;}}; 
            P2 ={Color={R=1.0; G=0.0; B=0.0}; Point= {X = -0.1; Y = -0.1;}}; 
            P3 ={Color={R=1.0; G=0.0; B=0.0}; Point= {X = 0.1; Y = -0.1;}};
            }
        Heading = Physics.neutralHeading
    }
