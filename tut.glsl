precision mediump float;

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

float distSqr(vec2 u, vec2 v){
    vec2 diff = u - v;
    return (abs(diff.x) + abs(diff.y))/10.0;
}

void main() {
    vec2 st = gl_FragCoord.xy / u_resolution.xy;

    vec3 color = vec3(0.0);
    float grid = 200.0;
    float arg = distSqr(mod(gl_FragCoord.xy - u_mouse + grid/2.0,grid), vec2(grid/2.0,grid/2.0)) - pow(u_time,1.2);
    color = vec3(0.5*tan(arg), 0.5*cos(arg) + 0.5, 0.5*sin(arg) + 0.5);

    gl_FragColor = vec4(color*(0.2*sin(3.14*(st.x + st.y + u_time)) + 0.25), 1.0);
}