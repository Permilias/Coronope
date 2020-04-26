Shader "Unlit/Shader_CharacterFace"
{
    Properties
    {
        _MouthTexture("MouthTexture", 2D) = "white" {}
                _EyesTexture("EyesTexture", 2D) = "white" {}
                        _LashesTexture("LashesTexture", 2D) = "white" {}
                                               _LidsTexture("LidsTexture", 2D) = "white" {}
                                               _BlinkTexture("BlinkTexture", 2D) = "white" {}
                                               _Blinks("Blinks", float) = 0
        _SkinColor("SkinColor", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MouthTexture;
                        sampler2D _EyesTexture;
                                    sampler2D _LashesTexture;
                                    sampler2D _LidsTexture;
                                    sampler2D _BlinkTexture;
            float4 _MouthTexture_ST;
            uniform float4 _SkinColor;
            uniform float _Blinks;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MouthTexture);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                //fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 col = _SkinColor;

                //mouth
                fixed4 mouthAddition = tex2D(_MouthTexture, i.uv);
                if(mouthAddition.a > 0)
                {
                     col = mouthAddition;
				}

                //Blinks
                if(_Blinks == 1)
                {
                                fixed4 blinkAddition = tex2D(_BlinkTexture, i.uv);
                if(blinkAddition.a > 0)
                {
                                    if(blinkAddition.x >= 0.2f)
                    {
                        blinkAddition = _SkinColor;
					}
                     col = blinkAddition;
				}
                                return col;
				}

                                //eyes
                fixed4 eyesAddition = tex2D(_EyesTexture, i.uv);
                if(eyesAddition.a > 0)
                {
                     col = eyesAddition;
				}

                                //lashes
                fixed4 lashesAddition = tex2D(_LashesTexture, i.uv);
                if(lashesAddition.a > 0)
                {
                     col = lashesAddition;
				}

                                                //lids
                fixed4 lidsAddition = tex2D(_LidsTexture, i.uv);
                if(lidsAddition.a > 0)
                {
                    if(lidsAddition.x >= 0.2f)
                    {
               lidsAddition = _SkinColor;
					}
                     col = lidsAddition;
				}


                return col;
            }
            ENDCG
        }
    }
}
