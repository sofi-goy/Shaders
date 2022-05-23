#ifdef GL_ES
precision mediump float;
#endif

#define PI 3.1415926538
#define TWO_PI 6.28318530718

uniform vec2 u_resolution;
uniform float u_time;

void main() {
  vec3 colorA = vec3(0.0, 0.0, 0.0);
  vec3 colorB = vec3(0.0, 0.7647, 1.0);

  vec2 st = gl_FragCoord.xy / u_resolution;
  st = (st - vec2(.5)) * 5.;

  // Number of sides of your shape
  int N = 3;

  // Angle and radius from the current pixel
  float angle = atan(st.x,st.y);
  float sector = TWO_PI/float(N);

  // Shaping function that modulate the distance
  float d = cos(floor(.5+angle/sector)*sector-angle)*length(st);
  d = smoothstep(0.4,0.5,d);

  gl_FragColor = vec4(mix(colorA,colorB,d), 1.0);
}