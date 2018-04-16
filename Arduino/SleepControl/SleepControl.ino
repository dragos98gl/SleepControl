#include <Metro.h>
#include <math.h>

Metro m = Metro(60000);

int ultimaValoare=0;
long int Timp=0;
int Batai=0;
int LowPoint;
int IsFire=1;

bool DataReceived = false;
bool CanRead=true;

void setup() {
  pinMode(14,INPUT);
  pinMode(17,INPUT);
  pinMode(20, INPUT);
  Serial.begin(9600);
}

void loop() {
  if (Serial.available() > 0)
  {
    char Packet = Serial.read();
    DataReceived = true;
  }

  if (DataReceived)
  {
    int valoareActuala = analogRead(14);
    
    int offset = valoareActuala;//+ ultimaValoare;

    if (analogRead(17)>300)
    {
      digitalWrite(4,HIGH);
      IsFire=2;
    }

    if ( analogRead(20)<800)
    {
      Serial.println(offset);
      Serial.println('!');
    }
    
    if(offset >100&&CanRead)
    {
      digitalWrite(13,HIGH);
      Batai++;
      CanRead=false;
     } else if (offset<10&&!CanRead)
     {
      digitalWrite(13,LOW);
      CanRead=true; 
     }
     
     ultimaValoare=valoareActuala;

    if (m.check())
    {
       // Serial.println(Batai);
  
        int bataiPeMinut = Batai;
        bataiPeMinut = IsFire + bataiPeMinut*10;
        
        char Packet[3];
        ((String)bataiPeMinut).toCharArray(3,3);
        
        //Serial.println(Packet);

        if (analogRead(20)>900)
        {
          Serial.write(Packet);    
          Serial.write('!');
        }
        
        Timp = 0;
        Batai = 0;
    }
  }
}
