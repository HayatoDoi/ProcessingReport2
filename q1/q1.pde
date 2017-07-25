PImage pic0; //pic0.jpg
PImage pic1; //pic1.jpg


void setup(){
  size(660, 500);
  frameRate(30);
  pic0 = loadImage("pic0.jpg");
  pic1 = loadImage("pic1.jpg");
}

void draw() {
  image(pic0, 0, 0, pic0.width / 2, pic0.height / 2);
  image(pic1, 0, pic0.height / 2, pic1.width / 2, pic1.height / 2);
}