#include <dht.h>

dht tempHSensor;

const int phPin = A0;
const int tempHPin = A1;
const int moistAnaPin = A2;
const int motor = 2;
const int waterPumpPin = 3; // water pump relay set to digital pin 3
bool waitTenMin = false;

float moistureHigh = 315;
float moistureLow = 590;


void setup() 
{
  pinMode(waterPumpPin, OUTPUT); // new code supposed to output to d_pin 3 to a relay for water pump
  pinMode(motor, OUTPUT);
  Serial.begin(9600);
}

void loop() 
{
  tempHSensor.read11(tempHPin);
  float phRead = analogRead(phPin);

  Serial.println("Humidity: " + String(tempHSensor.humidity));
  Serial.println("Temperature: " + String(CtoF(tempHSensor.temperature)));
  float phValue = remap(phRead, 170.0f, 433.0f, 2.7f, 8.2f);
  Serial.println("PH: " + String(phValue));
  float x = getMoisture();
  Serial.println(x);
  // waterPumpWrite(x); ///// refer to this function to see specifications on how/when to call it
  // phSensorWrite(phValue); //refer to this function to see specifications on how/when to call it
  // if (waitTenMin) delay(600000); 
  digitalWrite(motor, HIGH);
  delay(1000);
  digitalWrite(motor, LOW);
  delay(1000);  
}

void waterPumpWrite(float z) { // don't actually call this function until data for how often the 
                               // moisture sensor gets kicked is recorded/known
  if (z > 35) {
    digitalWrite(waterPumpPin, HIGH);
    delay(10000); // delay 10seconds
    digitalWrite(waterPumpPin, LOW); 
    waitTenMin = true;
  }
  else {
    waitTenMin = false;
    digitalWrite(waterPumpPin, LOW);
  }
}

void phSensorWrite(float phVal) { //don't actually call this function until data for how 
                                  //quickly ph saturates with water pump in place is known
  if (phVal >= 7.2){
  digitalWrite(motor, HIGH);
  delay(5000);
  digitalWrite(motor, LOW);
  waitTenMin = true;
//  delay(600000); // 10min delay (change this to interval it takes water pump to saturate ph down)
  }
  else {
    waitTenMin = false;
    digitalWrite(motor, LOW);
  }
}

float getMoisture() {
  float sum = 0;
  float val = 0;
  for (int i = 0; i < 100; ++i){
    sum += analogRead(moistAnaPin);;
  }
  val = remap(sum/100,moistureLow, moistureHigh, 0, 100);
  return val;
}

float CtoF(float c)
{
  return 1.8f*c + 32;
}

float remap(float s, float a1, float a2, float b1, float b2)
{
    return b1 + (s-a1)*(b2-b1)/(a2-a1);
}
