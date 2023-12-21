#include <ESP8266WiFi.h>
#include <SPI.h>
#include <MFRC522.h>
#include <MySQL_Generic.h>
 
IPAddress server_addr(192, 168, 99, 103);  // IP of the MySQL *server* here
char userDB[] = "html";              // MySQL user login username
char passwordDB[] = "password";        // MySQL user login password
int server_port = 3306;
char database[] = "sensors";
 
const char *ssid = "OS1";
const char *password = "12345678";
  
 
#define RST_PIN         0          // Configurable, see typical pin layout above
#define SS_PIN          15         // Configurable, see typical pin layout above
 
MFRC522 mfrc522(SS_PIN, RST_PIN);  // Create MFRC522 instance
 
    // Use this for WiFi instead of EthernetClient
MySQL_Connection conn((Client *)&client);  
 
void setup()
{
    Serial.begin(115200);
    WiFi.begin(ssid, password);
 
    while (WiFi.status() != WL_CONNECTED)
    {
        delay(1000);
        Serial.println("Connecting to WiFi...");
    }
 
    Serial.println("Connected to WiFi");
 
 
  // print out info about the connection:
    Serial.println("Connected to network. My IP address is: ");
    Serial.println(WiFi.localIP());
 
    // Connect to MySQL server
    if (conn.connect(server_addr, server_port, userDB, passwordDB, database)) {
      Serial.println("Connected to MySQL server");
    } else {
      Serial.println("Connection failed.");
      return;
    }
 
    SPI.begin();      // Init SPI bus
    mfrc522.PCD_Init();   // Init MFRC522
    delay(4);       // Optional delay. Some board do need more time after init to be ready, see Readme
    mfrc522.PCD_DumpVersionToSerial();  // Show details of PCD - MFRC522 Card Reader details
    Serial.println(F("Scan PICC to see UID, SAK, type, and data blocks..."));
}
 
String GetUID()
{
    // Look for new cards
  if (!mfrc522.PICC_IsNewCardPresent()) {
    return "";
  }
 
  // Select one of the cards
  if (!mfrc522.PICC_ReadCardSerial()) {
    return "";
  }
 
  // Concatenate UID into a string
  String cardUID = "";
  for (byte i = 0; i < mfrc522.uid.size; i++) {
    cardUID += (mfrc522.uid.uidByte[i] < 0x10 ? "0" : "") + String(mfrc522.uid.uidByte[i], HEX);
  }
 
  return cardUID;
}
 
 
void loop()
{  
  String uid = GetUID();
  if(uid != "")
  {
    Serial.println(uid);
 
    MySQL_Query query_mem = MySQL_Query(&conn);
 
    String queryStr = "UPDATE `reader` SET `Uid`='" + uid + "',`Time`= NOW()";
 
    char query[100];
    queryStr.toCharArray(query, sizeof(query));
   
    if ( !query_mem.execute(query) )
        Serial.print("Error");
    else
        Serial.print("OK"); 
  }
  delay(500);
}
