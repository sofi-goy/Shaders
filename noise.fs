#ifdef GL_ES
precision mediump float;
#endif

uniform float u_time;
uniform vec2 u_resolution;

float rand(float x) {
    return fract(sin(x)*100000.0);
}

float noise(float x){
    float i = floor(x);
    float f = fract(x);

    float a = rand(i);
    float b = rand(i+1.);

    f = (-2.) * pow(f,3.0) + 3. * pow(f,2.0);
    // f = smoothstep(0.0,1.0,f);
    return b * f + a * (1.-f);
}

void main(){
    vec2 st = gl_FragCoord.xy / u_resolution;
    st -= .5;
    st *= 10.;

    float r = noise(st.x * abs(st.x) * st.y + u_time);
    float g = noise(2.* st.x * abs(st.x) * st.y - u_time);
    float b = noise(10. * st.x - st.y * st.x - u_time);

    gl_FragColor = vec4(r,g,b,1.0);
}