Shader "Antosha_Project/NoFading"
{
	Properties
	{

	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100
		Pass
		{
			//ZWrite Off

			Stencil
			{
				Ref 1
				Comp NotEqual
			}
		}
	}
}
