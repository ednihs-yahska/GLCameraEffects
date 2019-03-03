#version 330 core

uniform vec4 timeColor;
uniform sampler2D screenTexture;

in vec2 vTexCoord;


out vec4 color;

void main()
{ 
	color = vec4(vec3(1.0 - texture(screenTexture, vTexCoord)), 1.0);
	//color = vec4(1.0, 1.0, 0.0, 1.0);
	//color = vec4(vTexCoord.x, vTexCoord.y, 1.0, 1.0);//texture(churchTexture, vTexCoord); 
};