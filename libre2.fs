#ifdef GL_ES
precision mediump float;
#endif

#define PI 3.1415926538
#define TWO_PI 6.28318530718

uniform float u_time;
uniform vec2 u_resolution;

float rand(float x) { return fract(sin(x) * 100000.0); }

float rand(vec2 st) {
  return fract(sin(dot(st.xy, vec2(12.9898, 78.233))) * 43758.5453123);
}

float noise(float x) {
  float i = floor(x);
  float f = fract(x);

  float a = rand(i);
  float b = rand(i + 1.);

  f = (-2.) * pow(f, 3.0) + 3. * pow(f, 2.0);
  return b * f + a * (1. - f);
}

float noise(vec2 st) {
  vec2 i = floor(st);
  vec2 f = fract(st);

  float a = rand(i);
  float b = rand(i + vec2(1.0, 0.0));
  float c = rand(i + vec2(0.0, 1.0));
  float d = rand(i + vec2(1.0, 1.0));

  vec2 u = (-2.) * f * f * f + 3. * f * f;
  //   vec2 u = f;

  return u.x * b + (1.0 - u.x) * a + (c - a) * u.y * (1.0 - u.x) +
         (d - b) * u.x * u.y;
}

float noise(vec2 st, int octaves) {
  float n = 0.0;
  float amp = 0.5;
  for (int i = 0; i < 100; i++) {
    if (i >= octaves)
      break;

    n += amp * noise(st / amp);
    amp /= 2.0;
  }
  return n / (1.0 - pow(2.0, -float(octaves)));
}

mat2 rotate2d(float _angle) {
  return mat2(cos(_angle), -sin(_angle), sin(_angle), cos(_angle));
}

void main() {
  vec2 st = gl_FragCoord.xy / u_resolution;
  st -= 0.5;
  st *= 8.0;
  vec2 f = mod(st, 0.9 + noise(u_time)) - 0.5;
  // vec2 f = fract(st) - 0.5;
  vec2 index = st - (f + 0.5);
  vec2 pos = rotate2d(noise(index / 2.0 + u_time / 10.0, 10) * TWO_PI) * f;

  float d = abs(pos.x) + abs(pos.y);
  d = smoothstep(0.1, 0.5, d);
  gl_FragColor = vec4(vec3(d), 1.0);
}