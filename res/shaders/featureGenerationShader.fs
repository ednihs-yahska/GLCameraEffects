#version 330 core
#define MAX_GHOSTS 3

uniform sampler2D tDiffuse;
uniform sampler2D tLensColor;
uniform float uGhostDispersal;
uniform float uDistortion;
uniform float uHaloWidth;
uniform float screenWidth;
uniform float screenHeight;

in vec2 vTexCoord;
out vec4 color;

vec4 textureDistorted(sampler2D tex, vec2 texcoord, vec2 direction, vec3 distortion){
	return vec4(
		texture2D(tex, texcoord+direction*distortion.r).r,
		texture2D(tex, texcoord+direction*distortion.g).g,
		texture2D(tex, texcoord+direction*distortion.b).b,
		1.0
	);
}

void main(){
	vec2 textureSize = vec2(screenWidth/2,screenHeight/2);
	vec2 texcoord = -vTexCoord + vec2(1.0);
	vec2 texelSize = 1.0 / textureSize;
	vec2 ghostVec = (vec2(0.5)-texcoord)*uGhostDispersal;

	vec4 result = vec4(0.0);

	for(int i=0; i<MAX_GHOSTS; ++i){
		vec2 offset = fract(texcoord + ghostVec * float(i));
		float weight = length(vec2(0.5) - offset) / length(vec2(0.5));
		weight = pow(1.0 - weight, 10.0);
		result += texture2D(tDiffuse, offset)*weight;
	}

	float distance2Center = length(vec2(0.5) - vTexCoord) / length(vec2(0.5));
	vec2 uvLensColor = vec2(distance2Center, 1.0);
	result *= texture2D(tLensColor, uvLensColor);

	vec3 distortion = vec3(-texelSize.x * uDistortion, 0.0, texelSize.x*uDistortion);

	vec2 haloVec = normalize(ghostVec) * uHaloWidth;
	float weight = length(vec2(0.5) - fract(texcoord + haloVec)) / length(vec2(0.5));
	weight = pow(1.0-weight, 5.0);

	result += textureDistorted(tDiffuse, fract(texcoord + haloVec), normalize(ghostVec), distortion)*weight;
	color = result;
}