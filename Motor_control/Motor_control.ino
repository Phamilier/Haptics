int motorPin[] = {3, 4, 5, 6, 7, 8, 9, 10}; //motor transistor is connected to pin 3
int CountDown = 0;
String data;
float voltage[] = {0, 0, 0, 0, 0, 0, 0, 0};
float numbers[8];
char *token;
int indexValue = 0;
//float intensity = 0;
unsigned long Time;
void setup()
{
  Serial.begin(115200);
  for(int i = 0; i < sizeof(motorPin)/ sizeof(motorPin[0]); i++)
  {
    pinMode(motorPin[i], OUTPUT);
  }
}
void loop()
{
  indexValue = 0;
  
   if (Serial.available()  > 0) 
   {   // 受信データがあるか？
     //voltage = Serial.readStringUntil('\n').toFloat();
     CountDown = 0;
     //data = Serial.readStringUntil('\n');
     data = Serial.readStringUntil('\n');

     String tempData = data;

    parseData(tempData);    
   }
   
   else
   {
     CountDown += 1;
   }

   if(CountDown > 100)
   {
     initialize();
   }
   
   
  float analogValue[8];
  for(int i = 0; i < sizeof(analogValue)/sizeof(analogValue[0]); i++)
  {
    analogValue[i] = 255/5 * voltage[i];
    analogWrite(motorPin[i], analogValue[i]); //vibrate
    Serial.print(voltage[i]);
    Serial.print(" ");
  }
  Serial.println();

/*
  Serial.print(voltage[0]);
  Serial.print(" ");
  Serial.println(voltage[1]);
 */ 
}

void parseData(String input) {
  int index = 0;
  while (index < 8) {
    int spaceIndex = input.indexOf(' ');
    if (spaceIndex == -1) {
      spaceIndex = input.length();
    }
    voltage[index] = input.substring(0, spaceIndex).toFloat();
    input = input.substring(spaceIndex + 1);
    index++;
  }
}

void initialize()
{
  for(int i = 0; i < sizeof(voltage)/sizeof(voltage[0]); i++)
  {
    voltage[i] = 0;
  }
}