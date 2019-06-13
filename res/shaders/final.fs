#version 330 core

uniform sampler2D tMotionBlur;
uniform sampler2D tLensFlare;
uniform float mixRatio;

in vec2 vTexCoord;


out vec4 color;


void main()
{ 
	vec4 first = texture(tMotionBlur, vTexCoord);
	vec4 second = texture(tLensFlare, vTexCoord);
	color = vec4(mix(first.xyz, second.xyz, mixRatio), 1.0);
};