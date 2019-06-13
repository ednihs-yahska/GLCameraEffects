#version 330 core

out vec2 vTexCoord;
out float blur;

layout(location = 0) in vec4 position;
layout (location = 1) in vec2 aTexCoord;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;
uniform mat4 uTransform;

vec4 vDofParams = vec4(-4., -8., -12., 1);

float ComputeDepthBlur(float depth){
	float f;
	if(depth < vDofParams.y) {
		//f = (depth - vDofParams.y)/(vDofParams.y - vDofParams.x);
		f = ( vDofParams.y - depth ) / ( vDofParams.x - vDofParams.y );
	}else {
		//f = (depth - vDofParams.y)/(vDofParams.z - vDofParams.y);
		f = (vDofParams.y - depth)/(vDofParams.y - vDofParams.x);
		f = clamp(f, 0, vDofParams.w);
	}
	return f * 0.5f + 0.5f;
}

void main() 
{ 
	vTexCoord = aTexCoord;
	vec4 position = view * model * uTransform * position;
	gl_Position = projection * position;
};