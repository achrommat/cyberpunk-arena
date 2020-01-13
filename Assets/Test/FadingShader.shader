Shader "Antosha_Project/FadingShader"
{
    Properties
    {
		
    }

    SubShader
	{
		Tags { "Queue" = "Geometry-1" }
		LOD 100
		Pass
		{
			ZWrite Off
			Cull Off
			ZWrite Off
			ZTest Always

			Stencil
			{
				Ref 1
				Comp Always
				Pass Replace
			}
		}
	}
}
