#version 330 core
#define NUM_TAPS 8

uniform vec4 timeColor;
uniform float mouseX;
uniform float mouseY;
uniform float aspect;
uniform sampler2D modelTexture;

in vec2 vTexCoord;
in float blur;

out vec4 color;


void main()
{ 
	//color = texture(modelTexture, vTexCoord);
	float x = vTexCoord.x*aspect;
	float y = vTexCoord.y;
	if(((x-mouseX)*(x-mouseX) + (y-mouseY)*(y-mouseY) < 0.01)){
		color = vec4(1.0, 1.0, 1.0, 1.0);
	}else{
		color = vec4(0.0, 0.0, 0.0, 1.0);
	}
	//color = vec4(vTexCoord.x, vTexCoord.y, 1.0, 1.0);//texture(churchTexture, vTexCoord); 
};