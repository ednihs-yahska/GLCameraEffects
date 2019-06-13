#version 330 core

out vec2 vTexCoord;

layout(location = 0) in vec4 position;
layout (location = 1) in vec2 aTexCoord;


void main() 
{ 
	vTexCoord = aTexCoord;
	gl_Position = position;
};