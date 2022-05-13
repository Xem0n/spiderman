#version 330 core

uniform vec2 resolution;

vec3 transform(vec3 pos)
{
	return vec3(pos.x, pos.y, pos.z);
}