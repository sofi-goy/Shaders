#ifdef GL_ES
precision mediump float;
#endif

#define PI 3.1415926538
#define TWO_PI 6.28318530718

uniform float u_time;
uniform vec2 u_resolution;

void main(){
    vec2 st = gl_FragCoord.xy / u_resolution;
    st *= 10.;
    st.x += step(1., mod(st.y,2.0)) * u_time;
    st.x -= (1.- step(1., mod(st.y,2.0))) * u_time;
    st = fract(st);
    float d = abs(st.x - .5) + abs(st.y -.5);
    float t = smoothstep(.499,.5,d);
    gl_FragColor = vec4(vec3(t),1.);
}