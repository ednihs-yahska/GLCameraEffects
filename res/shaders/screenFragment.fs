#version 330 core

uniform vec4 timeColor;
uniform mat4 uInverseViewProjection;
uniform mat4 uPreviousProjectionM;
uniform sampler2D screenTexture;
uniform sampler2D depthTexture;

in vec2 vTexCoord;


out vec4 color;

vec4 DownSampleFrame(sampler2D uniformSampler, vec2 TexCoords)
{
	vec2 iRes = textureSize(screenTexture, 0);
    vec2 pixelOffset = vec2(1/iRes.s, 1/iRes.t);
	float scaleFactor = 4.0;
    vec3 downScaleColor = vec3(0.0f, 0.0f, 0.0f);
    downScaleColor += texture(uniformSampler, vec2(TexCoords.x - scaleFactor * pixelOffset.x, TexCoords.y)).xyz;
    downScaleColor += texture(uniformSampler, vec2(TexCoords.x + scaleFactor * pixelOffset.x, TexCoords.y)).xyz;
    downScaleColor += texture(uniformSampler, vec2(TexCoords.x, TexCoords.y - scaleFactor * pixelOffset.y)).xyz;
    downScaleColor += texture(uniformSampler, vec2(TexCoords.x, TexCoords.y + scaleFactor * pixelOffset.y)).xyz;
    downScaleColor *= 0.25f;
    return (vec4(downScaleColor, 1.0f));
}

void main()
{ 
	vec4 colorTexture = texture(screenTexture, vTexCoord);
	float zOverW = texture(depthTexture, vTexCoord).z;
	vec4 zOver = texture(depthTexture, vTexCoord);
	vec4 H = vec4(vTexCoord.x * 2 - 1, (1 - vTexCoord.y) * 2 - 1, zOverW, 1);
	vec4 worldPos = uInverseViewProjection * H;
	worldPos /= worldPos.w;
	vec4 prevPosition = uPreviousProjectionM * worldPos;
	prevPosition /= prevPosition.w;
	vec2 velocity =  (H - prevPosition).xy;
	float l = length(velocity);
	velocity = velocity /2;
	vec2 blurCoord = vTexCoord + velocity;
	for(int i=0; i<4; ++i, blurCoord+=velocity){
		vec4 currentColor = texture(screenTexture, blurCoord);
		colorTexture += currentColor;
	}
	color = colorTexture / 4;//DownSampleFrame(screenTexture, vTexCoord); 
};