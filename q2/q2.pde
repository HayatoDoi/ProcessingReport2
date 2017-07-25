PImage pic0; //pic0.jpg
PImage pic0_filter; //pic0.jpgを2値化したもの
PImage pic1; //pic1.jpg
PImage pic1_filter; //pic1.jpgを2値化したもの

void setup(){
  size(660, 500);
  frameRate(30);
  pic0 = loadImage("pic0.jpg");
  pic0_filter = loadImage("pic0.jpg");
  pic0_filter.filter(THRESHOLD, 0.5);
  
  pic1 = loadImage("pic1.jpg");
  pic1_filter = loadImage("pic1.jpg");
  pic1_filter.filter(THRESHOLD, 0.5);

}

void draw() {
  
  //そのままの画像
  image(pic0, 0, 0, pic0.width / 2, pic0.height / 2);
  image(pic1, 0, pic0.height / 2, pic1.width / 2, pic1.height / 2);
  
  //2値化した画像
  image(pic0_filter, pic0.width / 2, 0, pic0.width / 2, pic0.height / 2);
  image(pic1_filter, pic0.width / 2, pic0.height / 2, pic1.width / 2, pic1.height / 2);
}