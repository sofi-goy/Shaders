#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;  // Canvas size (width,height)
uniform vec2 u_mouse;       // mouse position in screen pixels
uniform float u_time;       // Time in seconds since load

#define PI 3.1415926538

float f(float x) {
    return mod(x,1.0) * abs(sin(x*PI));
}

float plot(vec2 st) {
    return smoothstep(0.0, 0.02, (st.y - f(st.x)));
}

void main() {
    vec2 st = (gl_FragCoord.xy / u_resolution - vec2(0.5)) * 5.0;
    st.x += (u_time * (PI + .2) + cos(u_time * PI));
    vec3 color1 = vec3(0.0, 0.7647, 1.0);
    vec3 color2 = vec3(0.0, 0.0, 0.0);
    float t = plot(st);
    // t = (t - 0.5) * cos(u_time * PI) + 0.5;
    vec3 color = mix(color1,color2,t);
	gl_FragColor = vec4(color,1.0);
}
