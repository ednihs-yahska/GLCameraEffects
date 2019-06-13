#version 330 core
uniform sampler2D tDiffuse;

in vec2 vTexCoord;
out vec4 color;

void main(){
	vec4 uScale = vec4(10, 10, 10, 1);
	vec4 uBias = vec4(-0.9, -0.9, -0.9, 0);
	color = max(vec4(0.0), texture(tDiffuse, vTexCoord)+uBias)*uScale;
}