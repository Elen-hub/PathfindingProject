Shader "Hidden/FogOfWar"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
		_ExploreTex("Explore Texture", 2D) = "white" {}
        _posX("PosX", float) = 0
        _posY("PosY", float) = 0
        _rectSize("RectSize", float) = 0
        _textureSize("TextureSize", float) = 0
    }
    SubShader
    {
		Tags 
		{
			"Queue" = "Transparent+1"
		}
        Pass
        {
			Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
			sampler2D _ExploreTex;
            float _posX;
            float _posY;
            float _rectSize;
            float _textureSize;

            fixed4 frag(v2f i) : SV_Target
            {
                if (i.uv.x < _rectSize/ _textureSize*0.5f || i.uv.x > 1 - _rectSize / _textureSize * 0.5f || i.uv.y < _rectSize / _textureSize * 0.5f || i.uv.y > 1- _rectSize / _textureSize * 0.5f)
                    return fixed4(0, 0, 0, 0);

                i.uv.x += _posX;
                i.uv.y += _posY;
				fixed4 col = tex2D(_MainTex, i.uv) + tex2D(_ExploreTex, i.uv);
				col.a =0.75f - col.r * 0.75f;
                return fixed4(0,0,0,col.a);
            }
            ENDCG
        }
    }
}
