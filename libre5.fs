#ifdef GL_ES
precision mediump float;
#endif

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

float random (in float x) {
    return fract(sin(x)*1e4);
}

// Based on Morgan McGuire @morgan3d
// https://www.shadertoy.com/view/4dS3Wd
float noise (in vec3 p) {
    const vec3 step = vec3(110.0, 241.0, 171.0);

    vec3 i = floor(p);
    vec3 f = fract(p);

    // For performance, compute the base input to a
    // 1D random from the integer part of the
    // argument and the incremental change to the
    // 1D based on the 3D -> 1D wrapping
    float n = dot(i, step);

    vec3 u = f * f * (3.0 - 2.0 * f);
    return mix( mix(mix(random(n + dot(step, vec3(0,0,0))),
                        random(n + dot(step, vec3(1,0,0))),
                        u.x),
                    mix(random(n + dot(step, vec3(0,1,0))),
                        random(n + dot(step, vec3(1,1,0))),
                        u.x),
                u.y),
                mix(mix(random(n + dot(step, vec3(0,0,1))),
                        random(n + dot(step, vec3(1,0,1))),
                        u.x),
                    mix(random(n + dot(step, vec3(0,1,1))),
                        random(n + dot(step, vec3(1,1,1))),
                        u.x),
                u.y),
            u.z);
}

float noise(vec3 p, int octaves){
    float n = 0.0;
    float amp = 0.5;
    for (int i = 0; i < 100; i++) {
        if (i >= octaves)
        break;

        n += amp * noise(p / amp);
        amp /= 2.0;
    }
    return n / (1.0 - pow(2.0, -float(octaves)));
}

void main(){
    vec2 st = gl_FragCoord.xy/u_resolution.xy;
    st -= 0.5;
    st.y *= u_resolution.y/u_resolution.x;
    st *= 10.0;

    float t = noise(vec3(st.xy,u_time / 7.0),10);
    float d = step(0.6,t) - step(0.75,t);
    gl_FragColor = vec4(vec3(d),1.0);
}