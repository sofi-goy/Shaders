#ifdef GL_ES
precision mediump float;
#endif

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
  return n / (1.0 - pow(2.0, - float(octaves)));
}

void main() {
  vec2 st = gl_FragCoord.xy / u_resolution;
  st -= 0.5;
  st *= 8.0;
  float a = noise(vec2(st.x, u_time/4.0), 2) -.5;
  float b = noise(vec2(st.y, u_time/5.0), 2) -.5;

  float r = noise(vec2(st.x + a * 10.0 , st.y + b*20.0),10);
  float g = noise(vec2(st.x + a * 12.0, st.y - b*15.0), 10);
  float bl = noise(vec2(st.x + a * 12.5, st.y - b*15.5),2);

//   bl = smoothstep(min(r,g),max(r,g),bl);
  vec3 color = vec3(r,g,bl);
  color = smoothstep(0.6,0.8,color);

  gl_FragColor = vec4(color, 1.0);
}