#version 330 core
uniform sampler2D tDiffuse;
uniform sampler2D tLensDirt;
uniform sampler2D tLensColor;
uniform sampler2D tLensStar;
uniform mat4 tLensStarMatrix;
uniform float artefactScale;
uniform float opacity;
uniform float mixRatio;

in vec2 vTexCoord;
out vec4 color;

void main(){
	vec4 artefactColor = texture2D(tLensDirt, vTexCoord);
	vec2 lensStarUv = (tLensStarMatrix * vec4(vTexCoord.x, vTexCoord.y, 0.0, 1.0)).xy;
	artefactColor += texture2D(tLensStar, lensStarUv);
	artefactColor += vec4(vec3(artefactScale), 1.0);
	vec4 texelLensColor = texture2D(tLensColor, vTexCoord)*artefactColor;
	vec4 texelDiffuse = texture2D(tDiffuse, vTexCoord);
	color = opacity * mix(texelDiffuse, texelLensColor, mixRatio);
}