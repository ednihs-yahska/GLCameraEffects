#version 330 core

uniform vec4 timeColor;
uniform sampler2D churchTexture;

in vec2 vTexCoord;


out vec4 color;

void main()
{ 
	color = texture(churchTexture, vTexCoord);
	//color = vec4(1.0, 1.0, 0.0, 1.0);
	//color = vec4(vTexCoord.x, vTexCoord.y, 1.0, 1.0);//texture(churchTexture, vTexCoord); 
};