#ifdef GL_ES
precision mediump float;
#endif

uniform float u_time;
uniform vec2 u_resolution;

float rand(vec2 st) {
    return fract(sin(dot(st.xy,
                         vec2(12.9898,78.233)))*
        43758.5453123);
}

void main(){
    vec2 st = gl_FragCoord.xy / u_resolution;
    st *=  100.;
    vec2 index = floor(st);
    st.x += u_time * (.5- rand(index)) * 10.;
    index = floor(st);
    float t = rand(index);
    t = step(0.2,t);
    vec3 color = vec3(t);
    gl_FragColor = vec4(color,1.0); 
}