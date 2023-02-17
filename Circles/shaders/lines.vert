﻿#version 330 core

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aColor;

out vec3 Color;

uniform mat4 projection;
uniform float one;

void main(){
	gl_Position = projection * vec4(aPos, 1);
	Color = aColor;
}