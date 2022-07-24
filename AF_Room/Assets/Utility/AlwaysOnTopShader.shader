Shader "Custom/AlwaysOnTopShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        // always pass ztest, so always on top
        ZTest Always
        
        Tags { "RenderType"="Opaque" }
        LOD 200

        Color [_Color]
        Pass {}
    }
    FallBack "Diffuse"
}
