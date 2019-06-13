#version 330 core
#define NUM_TAPS 8

uniform vec4 timeColor;
uniform sampler2D modelTexture;

in vec2 vTexCoord;
in float blur;

out vec4 color;


void main()
{ 
	color = texture(modelTexture, vTexCoord);
	//color = vec4(blur, blur, blur, 1.0);
	//color = vec4(vTexCoord.x, vTexCoord.y, 1.0, 1.0);//texture(churchTexture, vTexCoord); 
};