#version 330 core

out vec2 vTexCoord;

layout(location = 0) in vec4 position;
layout (location = 1) in vec2 aTexCoord;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;
uniform mat4 uTransform;

void main() 
{ 
	vTexCoord = aTexCoord;
	gl_Position = projection * view * model * uTransform * position;
};