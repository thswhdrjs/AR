Shader "Portal/PortalDoor"
{
    Properties
    {
        [Enum(Equl, 3, NotEkqual, 6)] sTest("Stencil Test", int) = 3
    }

    SubShader
    {
        ColorMask 0
        ZWrite Off

        Stencil
        {
            Ref 1
            Pass Replace
        }

        Pass
        {

        }
    }
}
