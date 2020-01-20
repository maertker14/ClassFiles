#include <LiquidCrystal.h>
#include <FlexiTimer2.h>

LiquidCrystal lcd(12, 11, 5, 4, 3, 2);

int tempPin = A4;
float tempVal;
int hour=8, minute=32, second;

void setup() {
  lcd.begin(16,2);
  startingAnimation();
  FlexiTimer2::set(1000,timerInt);
  FlexiTimer2::start();
  Serial.begin(9600);
  Serial.println("Uno is ready!");
  Serial.println("Input hour, minute, second to set time.");
}

void loop() {
  tempVal = analogRead(tempPin);
  tempVal = ((5.0*tempVal*100)/1024.0-5);
  float bla = tempVal;
  bla = (bla *1.8) + 32;
  tempVal = bla;
  if (second >= 60){
    second =0;
    minute++;
    if (minute >= 60) {
      minute =0;
      hour++;
      if (hour >= 24){
        hour = 0;
      }
    }
  }
  lcdDisplay();
  delay(200);
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

void timerInt() {
  second++;
}

void serialEvent() {
  int inInt[3];
  while (Serial.available()) {
    for (int i = 0; i < 3; i++){
      inInt[i] = Serial.parseInt();
    }
    Serial.print("Your input is: ");
    Serial.print(inInt[0]);
    Serial.print(", ");
    Serial.print(inInt[1]);
    Serial.print(", ");
    Serial.print(inInt[2]);

    hour = inInt[0];
    minute = inInt[1];
    second = inInt[2];

    Serial.print("\nTime now is: ");
    Serial.print(hour/10);
    Serial.print(hour%10);
    Serial.print(" :");
    Serial.print(minute/10);
    Serial.print(minute%10);
    Serial.print(" :");
    Serial.print(second/10);
    Serial.println(second%10);
  }
}

void lcdDisplay() {
  lcd.setCursor(0,0);
  lcd.print("Temp: ");
  lcd.print(tempVal);
  lcd.print("F");
  lcd.setCursor(0,1);
  lcd.print("Time: ");
  lcd.print(hour/10);
  lcd.print(hour%10);
  lcd.print(" :");
  lcd.print(minute/10);
  lcd.print(minute%10);
  lcd.print(" :");
  lcd.print(second/10);
  lcd.print(second%10);
}
