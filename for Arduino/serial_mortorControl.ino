int combuffp = 0;
char combuff[21];

void setup() {
  combuff[20] = 0;
  Serial.begin(9600);

  pinMode(9, OUTPUT);
  pinMode(10, OUTPUT);
  pinMode(11, OUTPUT);
  pinMode(12, OUTPUT);
}

void loop() {
  int inputchar = Serial.read();

  if (inputchar == -1) return;
  if (inputchar == 10 || inputchar == 13) {
    combuff[combuffp] = 0;
    combuffp = 0;

    int len = strlen(combuff);
    if (len == 7 && combuff[0] == 'M' && combuff[1] == '(' && combuff[6] == ')') { // "M(****)" ... 7characters
      Serial.println("correct.");
      digitalWrite(9, combuff[2] == '1' ? HIGH : LOW);
      digitalWrite(10, combuff[3] == '1' ? HIGH : LOW);
      digitalWrite(11, combuff[4] == '1' ? HIGH : LOW);
      digitalWrite(12, combuff[5] == '1' ? HIGH : LOW);
    }
    else {
      Serial.println("incorrect.");
    }
    return;
  }

  combuff[combuffp] = inputchar;
  combuffp = (combuffp + 1) % 20;
}

