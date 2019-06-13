#version 330 core
#define NUM_TAPS 8

uniform sampler2D screenTexture;

in vec2 vTexCoord;
out vec4 color;

uniform sampler2D tSource;
uniform sampler2D tSourceLow;

vec2 poisson[NUM_TAPS];

vec2 pixelSizeHigh;
vec2 pixelSizeLow;

vec2 iResHigh = textureSize(tSource, 0);
vec2 iResLow = textureSize(tSourceLow, 0);

vec2 vMaxCoC = vec2(5.0, 10.0);

float radiusScale = 0.4;


vec4 PoissonDOFFilter(vec2 texCoord){
	vec4 cOut;
	float discRadius, discRadiusLow, centerDepth;

	pixelSizeHigh = 1/iResHigh;
	pixelSizeLow = 1/iResLow;

	cOut = texture2D(tSource, texCoord);
	centerDepth = cOut.a;

	discRadius = abs(cOut.a * vMaxCoC.y - vMaxCoC.x);
	discRadiusLow = discRadius * radiusScale;
	cOut = vec4(0., 0., 0., 0.);

	for(int t = 0; t < NUM_TAPS; t++){
		vec2 coordLow = texCoord + (pixelSizeLow * poisson[t] * discRadiusLow);
		vec2 coordHigh = texCoord + (pixelSizeHigh * poisson[t] * discRadius);

		vec4 tapLow = texture2D(tSource, coordLow);
		vec4 tapHigh = texture2D(tSource, coordHigh);

		float tapBlur = abs(tapHigh.a * 2.0 - 1.0);
		vec4 tap = mix(tapHigh, tapLow, tapBlur);

		tap.a = (tap.a >= centerDepth) ? 1.0 : abs(tap.a * 2.0 - 1.0);

		cOut.rgb += tap.rgb * tap.a;
		cOut.a += tap.a;
	}
	return (cOut / cOut.a);
}

void main()
{ 
	//vec2 scaledCoord = vTexCoord/1.5;
	poisson[0] = vec2(0.15, 0.15); poisson[1] = vec2(0.22, 0.22); poisson[2] = vec2(0.3, 0.3); poisson[3] = vec2(0.35, 0.35); poisson[4] = vec2(0.32, 0.32); 
	poisson[5] = vec2(0.28, 0.28); poisson[6] = vec2(0.17, 0.17); poisson[7] = vec2(0.8, 0.8);
	color = PoissonDOFFilter(vTexCoord);
	//color = vec4(1.0, 1.0, 0.0, 1.0);
	//color = vec4(vTexCoord.x, vTexCoord.y, 1.0, 1.0);//texture(churchTexture, vTexCoord); 
};