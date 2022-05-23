#ifdef GL_ES
precision  mediump float;
#endif

#define PI 3.1415926538
#define TWO_PI 6.28318530718

uniform vec2 u_resolution;
uniform float u_time;

mat2 rotate2d(float _angle){
    return mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle));
}

mat2 shear(float amount){
    return mat2(1., amount,
                0., 1.);
}


mat2 inv(mat2 A){
    float a = A[0][0];
    float b = A[0][1];
    float c = A[1][0];
    float d = A[1][1];

    float det = (a*d-b*c);
    return 1. / det * mat2(d, -c,
                    -b, a);
}

float grid(vec2 st){
    float w = 0.03;
    return smoothstep(0.0,w,mod(st.x+w/2.,1.)) * smoothstep(0.0,w,mod(st.y+w/2.,1.));
}

void main(){
    vec2 st = gl_FragCoord.xy / u_resolution;
    st = (st - vec2(.5)) * 2. * 5.;
    float s = abs(cos(u_time));
    mat2 id = mat2(1.,0.,0.,1.);
    mat2 A = inv( mat2(
            2.0, 3.0,
            -1.2, 0.1
        )); 
    st = (s*id + (1. - s) * A) * st;

    float t = grid(st);
    vec3 color = (1.-t) * vec3(0.0, 0.7647, 1.0);
    color = mix(color, vec3(1.), 1.- smoothstep(0.,.2,distance(st, vec2(0.0,0.0))));
    color = mix(color, vec3(1.,0.,0.), 1.- smoothstep(0.,0.2,distance(st, vec2(1.0,0.0))));
    color = mix(color, vec3(0.,1.,0.), 1.- smoothstep(0.,0.2,distance(st, vec2(0.0,1.0))));
    gl_FragColor = vec4(color,1.0);
}