#include<Uduino.h>
Uduino uduino("myEncoder"); // Declare and name your object

int encoder_a = 2;
int encoder_b = 3;
long encoder = 0;

void setup() {
  Serial.begin(9600);
  pinMode(encoder_a, INPUT_PULLUP);
  pinMode(encoder_b, INPUT_PULLUP);

  attachInterrupt(0, encoderPinChangeA, CHANGE);
  attachInterrupt(1, encoderPinChangeB, CHANGE);
}

void loop() {
  uduino.update();
  if (uduino.isConnected()) {
      Serial.println(encoder);
  }

}

void encoderPinChangeA() {
  encoder += digitalRead(encoder_a) == digitalRead(encoder_b) ? 1 : -1;
}

void encoderPinChangeB() {
  encoder += digitalRead(encoder_a) != digitalRead(encoder_b) ? 1 : -1;
}