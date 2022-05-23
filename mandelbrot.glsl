precision mediump float;

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

vec2 complexSqr(vec2 z){
    vec2 w = vec2(0.0);
    w.x = z.x * z.x - z.y * z.y;
    w.y = 2.0 * z.x * z.y;
    return w;
}

vec2 mandelStep(vec2 z, vec2 c){
    return complexSqr(z) + c;
}

void main() {
    vec2 st = gl_FragCoord.xy / u_resolution.yy;
    float zoom = pow(u_time,3.0);

    vec2 mathCoord = (st - 0.5)/zoom + vec2(-0.77568377, 0.13646737);
    // mathCoord /= (u_time+0.5);
    vec2 z = vec2(0.0);

    vec3 color = vec3(0.0);
    for (int i=0 ; i < 3000 ; i++){
        z = mandelStep(z,mathCoord);
        if (length(z) > 2.0){
            color.r = smoothstep(0.0,300.0,float(i));
            color.g = smoothstep(150.0,600.0,float(i));
            color.b = smoothstep(300.0,1000.0,float(i));
            break;
        }
    }

    // gl_FragColor = vec4(mathCoord.x, mathCoord.y, 0.0, 1.0);
    gl_FragColor = vec4(color, 1.0);
}