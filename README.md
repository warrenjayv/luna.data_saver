# luna.data_saver
`a compact static save and load for unity`

example:
```csharp
using luna.data_saver;

public class save_test {

  // data containing objects
  saved_data data;
  
  void Awake() {
  
      data = new saved_data ();
      saver.identity ( data ); 
  }
  
  public void save ( )  {
  
       saved_object player1 = new saved_object ( );
       saved_object player2 = new saved_object ( );
       
       player1.status.health = 80.0f;
       player2.status.health = 50.0f;
       
       data.add(player1);
       data.add(player2);
       
       saver.serialize("savefile.data");
  }
  
  public void load ( ) {
  
       saved_object player1 = null;
       saved_object player2 = null;
       
       saver.deserialize("savefile.data");
       
       player1 = saver.obtain_data().get(0);
       player2 = saver.obtain_data().get(1);
             
       saver.log("player 1 health: " + player1.status.health);
       saver.log("player 2 health: " + player2.status.health); 
  }
  
  
}
```
