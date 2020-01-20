
#include <LiquidCrystal.h>
#include <FlexiTimer2.h>


const int phPin = A1;
LiquidCrystal lcd(12, 11, 5, 4, 3, 2); //initialize two digital pins(12,11), and the pwm's (5,4,3,& 2)
float phVal = 0;
//int hour=8, minute=32, second;

void setup() {
  // put your setup code here, to run once:
  lcd.begin(16,2);
  startingAnimation();
//  FlexiTimer2::set(1000,timerInt);
//  FlexiTimer2::start();
  Serial.begin(9600);
  Serial.println("Uno is ready!");
}

void loop() {
  // put your main code here, to run repeatedly:
  float phRead = analogRead(phPin);
  phVal = remap(phRead, 170.0f, 443.0f, 2.7f, 8.2f);
//   if (second >= 60){
//    second =0;
//    minute++;
//    if (minute >= 60) {
//      minute =0;
//      hour++;
//      if (hour >= 24){
//        hour = 0;
//      }
//    }
//  }
  lcdDisplay();
  delay(200);
}

//void timerInt() {
//  second++;
//}

float remap(float s, float a1, float a2, float b1, float b2)
{
    return b1 + (s-a1)*(b2-b1)/(a2-a1);
}

void lcdDisplay() {
  lcd.setCursor(0,0);
  lcd.print("phVal: ");
  lcd.print(phVal);
}

void startingAnimation() {
  for (int i = 0; i < 16; i++) {
    lcd.scrollDisplayRight();
  }
  lcd.print("starting...");
  for (int i = 0; i < 16; i++){
    lcd.scrollDisplayLeft();
    delay(300);
  }
  lcd.clear();
}
