precision mediump float;

uniform vec2 u_resolution;
uniform vec2 u_mouse;
uniform float u_time;

struct Triangle {
    vec2 p;
    vec2 q;
    vec2 r;
};

float distSqr(vec2 u, vec2 v){
    vec2 diff = u - v;
    return (abs(diff.x) + abs(diff.y))/10.0;
}

bool inTriangle (Triangle t, vec2 x) {
    vec2 a = t.q - t.p;
    vec2 b = t.r - t.p;
    x -= t.p;

    float det = (a.x * b.y - a.y * b.x);

    float coord_a = sign(det) * (b.y * x.x - b.x * x.y);
    float coord_b = sign(det) * (-a.y * x.x + a.x * x.y);

    return ((coord_a > 0.0) && (coord_b > 0.0) && (coord_a + coord_b < abs(det)));
}

void main() {
    vec2 st = gl_FragCoord.xy / u_resolution.xy * 4.0;
    vec2 mouse = u_mouse / u_resolution.xy * 4.0;

    Triangle t = Triangle(vec2(1.0, 2.0), vec2(2.0,1.0), mouse);
    vec3 color = vec3(inTriangle(t, st));

    gl_FragColor = vec4(color,1.0);
}