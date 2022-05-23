#ifdef GL_ES
precision mediump float;
#endif

#define TWO_PI 6.28318530718

uniform vec2 u_resolution;
uniform float u_time;

//  Function from Iñigo Quiles
//  https://www.shadertoy.com/view/MsS3Wc
vec3 hsb2rgb(vec3 c) {
  vec3 rgb =
      clamp(abs(mod(vec3(c.x) * 6.0 + vec3(0.0, 4.0, 2.0), 6.0) - 3.0) - 1.0,
            0.0, 1.0);
  rgb = rgb * rgb * (3.0 - 2.0 * rgb);
  return c.z * mix(vec3(1.0), rgb, c.y);
}

float f(float x) {
    return pow(x/TWO_PI, x/2.0) * TWO_PI;
    // return pow(x/TWO_PI,2.0) * TWO_PI;
}

float g(float r){
    return pow(2.71,-r*r);
}

void main() {
  vec2 st = gl_FragCoord.xy / u_resolution;
  vec3 color = vec3(0.0);

  // Use polar coordinates instead of cartesian
  vec2 toCenter = st - vec2(0.5);
  float angle = mod(atan(toCenter.y, toCenter.x) + u_time, TWO_PI);
  angle = f(angle);
  float radius = length(toCenter) * 3.0;

  // Map the angle (-PI to PI) to the Hue (from 0 to 1)
  // and the Saturation to the radius
  color = hsb2rgb(vec3((angle / TWO_PI) + 0.5, radius, g(radius)));

  gl_FragColor = vec4(color, 1.0);
}
