<b><h1>Recycle Game</h1></b>

A video game by Alessandro Maggi and Davide Orengo

*Computer graphics arch exam - games and simulation*

<b><h2>Targets</h2></b>

Create a game, in particular a serious game (game not only of entertainment but also educational), with the aim of sensitizing and teaching how separate waste, thus creating an educational, effective and pleasant experience for the player.

<b><h2>Introduction</h2></b>

The development of the project was carried out using the Monogame framework and Visual Studio taking advantage of the knowledge learned during the COMPUTER GRAPHICS ARCH.- GAMES AND SIMULATION course and deepening the concepts necessary for the realization.

The name of the game is "RecycleGame" as the topic is recycling.

This theme was chosen because waste disposal is an increasingly felt problem both by citizens and administrations, due to the incredible amount of garbage that modern society produces, consequently one of the main solutions is the recycling which thanks to the collectivity can bring significant benefits.

The game is aimed especially at children but also to anyone who wants to learn the basics of waste separation in such a way to apply the same approach in the real world. The game indicates to the player if he is correctly recycling the waste by providing various tools that indicate the progress of the game and also allows you to consult an information booklet for more details on European recycling.


The player can explore the external surroundings of a town using his character and can collect various waste that he finds and at any time he can go to a bin to deposit the waste of a specific type.

The game features a score that represents the player's experience and changes depending on the right or wrong choices in waste sorting. So if the action of depositing a waste in a specific bin is wrong, points will be removed instead if it is correct they will be added.

The game is divided into levels and each level is represented by a different score range, the first level provides the player with a limited number of types of waste and the corresponding bins, consequently the subsequent levels will unlock new types of waste and bins to interact with.

The interaction between waste and bins takes place through a game window that allows you to choose between the collected waste and which to throw in the corresponding bin.

<b><h2>MonoGame framework</b></h2>

Recycle Game was created using MonoGame, an open-source framework for cross-platform videogames: it allows videogames porting to many platforms including Windows, Linux, iOS and android. MonoGame allows the development of videogames by means of the C# object programming language by offering numerous classes that contain indispensable tools.

The structure of a game created with Monogame is essentially based on three classes:

-   *Game*: the heart of the videogame which is also the entry point of the program. It contains the interfaces of the essential methods for the game and called by the base class:

    -   *Inizialize()*: contains the basic initialization of the graphics and input sector;
    -   *LoadConten()*: upload multimedia content 
    -   *Update()*: method called before rendering of each frame. It takes as input a GameTime object used to get a lot of information about the game's time status
    -   *Draw()*: renders all the elements of the game on the screen. As Update(), it takes a GameTime object as argument
-   *Graphics.SpriteBatch*: allows the rendering of all Texture2D objects 
-   *Content.ContentManage*r: allows to manage all the multimedia resources of the game. For this purpose, the framework provides the Content Pipeline tool which, by means of a graphic interface, allows an easier management of the contents.

<b><h2>The structure of Recycle Game</b></h2>

Recycle Game was created using many different classes and often Inheritance and Polymorphism were used in order to manage numerous graphic objects with multiple behaviors and different interactions. Below is the diagram of the most significant classes of the videogame:

![Class Diagram](media/diagrammaClassi.png)

<b><h2>The main classes</b></h2>

***Component***: is the base class of the video game, contains the basic references that each component must have, namely ContentManager, GraphicsDeviceManager, Game. Almost all other classes derive from this class.

***State:*** game operation is based on state machine logic which is implemented within the Game1 class, the latter derived directly from Game. Each state inherit from the State class, Draw() and Update() methods which perform the same function as the homonymous methods contained in the Game1 class. The Game1 class contains two properties _currentState and _nextState which refer to the previous and next states. The first is changed in Game1's Update() method whenever the _nextState variable contains a new state of the game. The next state variable is changed within the same states whenever it is needed. In another paragraph of this report the individual states will be discussed.

***Game1***: derived from Game and so it is the entry-point class of the game. Contains game initializations, the state and level management. The Update() and Draw() methods of the MonoGame framework are implemented within it.

***Sprite***: an object of this class represents a single sprite whose properties are multiple among which the most important are:
    -   *texture*: the texture of the sprite
    -   *rect*: the rectangle in which the sprite is contained
    -   *gamePos*: position of the sprite in relation to the game map
    -   *displayPos*: position of the sprite relative to the screen
    -   *scale*: measure of texture scaling
    
The sprite class contains all the methods necessary to manage the movement and position of each single element:

-   setPos(), setGamePos(): change the position absolutely
-   stepPos(), stepGamePos(): change the position of a certain amount
-   setTilePos(): changes the position with reference to the tile map

It also has its respective get\#\#\#() methods that are used to get the values. Five other classes derive from this class. The sprite class contains a basic Draw() function which is often replaced by a more specific Draw() in derived classes.

***Animated Sprite***: award to each individual sprite an animation given by the succession of frames which are grouped in a single texture. The animation technique is based on showing only a portion of the texture in which the frame you want to view is present.

***Character***: derived from sprites, this class allows to create a character capable of moving in the game and interacting with the obstacles on the map. Each Character object can be manipulated from the keyboard with WASD commands thanks to the method keyboardMgnt present inside the MainState 

***Item***: this class allows to manage the individual objects in the game that represent waste like Sprite. Each Item has a type property that confers the type of waste starting from an enumerative list. Furthermore, thanks to a lookup table of the sine function, each item is able to move up and down giving an animated effect useful for identifying the presence of an item within the map.

![Class Diagram](media/floating.png)

***SpeechBubble***: creates a speech bubble containing any text that is displayed each time it is activated using an enable variable. This class is used inside the window where all the symbols of the recycling are located. Each time the mouse arrow passes over a symbol, a speechBubble is activated which contains its description. The speechBubble can also be used with a Character to give the idea that it is speaking.

![](media/speech-ex.png)

***Vocabulary***: this class creates a window in the form of a notebook in the center of the screen which shows in sequence the recycling symbols with their descriptions, all the bins and all the objects present in the game. To move from one page to another, use the arrows located at the base of the notebook. The management of which pages to show is done through the use of delegates: the delegate that is called within the Draw() is modified within Update() by inserting a method that will show the correct page.

***Windows***: creates an interactive window that allows to interact with the waste that has been collected and the available bin.
To build the window, the Windows class was written which includes the Draw() and Update() methods and others in order to manage its interaction with the player. 
The window has a background texture on which various elements are positioned, on the left:

-   una texture “card” che comprende l’immagine del rifiuto e i simboli specifici per quel tipo di riciclaggio;

NOTA: quando non è presente nessun rifiuto compare una card di default con scritto “nessun rifiuto”.

-   un numerino rappresentato dalla classe *Banner* che mostra il numero della “card” che si sta visualizzando (la sequenza delle card seguono l’ordine di raccolta)
-   una freccia destra e una freccia sinistra entrambe rappresentate dalla classe *Button* che, quando una viene cliccata, permette di passare da una card a un’altra così si possono raggiungere i rifiuti da depositare nel bidone disponibile

A destra è presente un “puzzle” composto da quadratini con ciascuno il nome di una tipologia di rifiuto, ciascun quadratino è rappresentato dalla classe *Button* poi è presente anche una texture di un riquadro con la scritta “prossimamente” che cambia rispetto al livello in cui si trova lo stato del gioco. Il riquadro indica che nel livello successivo saranno disponibili nuovi quadratini per nuove tipologie di rifiuto.

La classe presenta due texture di contorno una verde e una rossa che verranno mostrate lampeggiando al posto del contorno bianco quando il giocatore depositerà un rifiuto, se la risposta è corretta appare quella verde al contrario quello rossa.

Per visualizzare la finestra il giocatore deve posizionarsi davanti a un bidone e premere “spazio” sulla tastiera così verrà modificata la variabile di abilitazione, ciò verrà effettuato utilizzando i tile di cui si parlerà in seguito. Per uscire dall finestra bisogna premere il tasto Esc che modifica a sua volta l’abilitazione.

Aperta la finestra sarà possibile premere con il mouse solo il quadratino riferito a quel bidone specifico per depositare il rifiuto visualizzato, invece gli altri quadratini appariranno scoloriti e quindi non sarà possibile utilizzarli.

![Class Diagram](media/window-ex.png)

<b><h2>Gli stati del gioco</b></h2>

Come già anticipato il RecycleGame è strutturato come una macchina a stati i quali sono tutti oggetti della classe State e ognuno implementa una propria Draw() e Update(). Lo stato iniziale viene impostato all’interno della classe Game1 nel metodo *LoadContent* e nei metodi Draw e Update di Game1 vengono chiamati i corrispettivi all’interno del *_currentState*.

***Gestione degli input***

Ogni stato si occupa, autonomamente di gestire gli input da tastiera per mezzo dell’oggetto KeyBoardState che viene passato come parametro al metodo Update all’interno di Game1. Ogni stato, se è necessario, implementa un metodo chiamato keyboardMgnt() che ha lo scopo di analizzare l’oggetto KeyBoardState e compiere le azioni necessarie per far funzionare il gioco.

***MenuState***: è il primo stato del gioco e ha lo scopo di mostrare il menù che comprende tre bottoni:

-   *New Game*: porta lo stato in MainState ovvero fa partire il gioco
-   *Load Game*: è un bottone disattivato, ma in futuro avrà lo scopo di caricare uno stato del gioco che è stato salvato in precedenza
-   *Exit*: chiama la il metodo Exit() della classe Game che chiude il gioco

***MainState***: è lo stato principale di tutto il video gioco. All’interno di esso si generano:

-   il personaggio principale comandato dall’utente tramite la tastiera
-   tutti gli oggetti del gioco che devono essere differenziati
-   la mappa del gioco

All’interno di questo stato vengono gestite tutte le interazioni fra il personaggio e gli oggetti tramite il gestore degli eventi. La funzione Draw() mostra a schermo i personaggi, la UI e tutti gli oggetti presenti sulla mappa. Il metodo Update() oltre che occuparsi di aggiornare la posizione della mappa e del personaggio, posizione casualmente all’interno del gioco gli oggetti in determinate zone definite nella matrice dei Tile. In seguito, verrà dedicato un paragrafo alla spiegazione di come vengono gestite le suddette matrici.

***TutorialState e ChooseBucket***: sono due stati semplici e molto simili, si occupano di mostrare a schermo gli oggetti delle classi Tutorial e Windows rispettivamente.

<b><h2>Tiled e le matrici di tile</b></h2>

Tiled è un software gratuito e open source che è utilizzato per creare mappe 2D per videogiochi mediante la tecnica dei tile. Un tile è una porzione quadrata di schermo con una dimensione in genere di 16x16px o 32x32px come in RecycleGame. Ogni tile viene riempito con una texture fino a comporre una griglia la quale rappresenta la mappa del gioco, detta TileMap. Tiled permette la progettazione della mappa mediante layer di tile sovrapposti. L’unico layer visualizzato però sarà quello più in alto degli altri. Le TileMap vengono salvate in un file .tmx che contiene tutti i layer in codifica xml contenente sotto forma di array bidimensionali, righe e colonne della tilemap. Ogni elemento degli array è un numero che rappresenta una precisa texture di 32x32px.

Per utilizzare una mappa creata con Tiled in monogame è sufficiente esportare la mappa creata in formato .png o .jpg e poi importare quest’ultima nel LoadContent. Nell’immagine esportata però saranno visibili solo i tile dei layer che sono stati resi visibili.

In RecycleGame è stato implementato un metodo per leggere il file .tmx della tilemap e codificare tutti i layer in liste bidimensionali. In questo modo sapendo che la mappa è una griglia di 32x32px è possibile in runtime associare ogni singolo pezzetto della mappa a tutti i tile in quel punto per ogni layer. Così si possono creare dei layer nascosti che hanno lo scopo di indentificare delle zone della mappa particolari, come ad esempio gli ostacoli.

Per fare un esempio, nella figura di sinistra vi è un pezzo della mappa visibile nel gioco, nella figura di destra invece la stessa mappa ma con il layer contenente gli ostacoli che è stato reso visibile.

![Class Diagram](media/mask-ex.png)

Il Character quando si sposta all’interno della mappa analizza tutti i tile con cui entra a contatto. Grazie ai layer nascosti è possibile capire se si trova davanti ad un ostacolo oppure no.

Con la stessa tecnica è stata resa possibile l’interazione diversa con altri elementi sparsi nel gioco: è presente un layer che contiene tile diversi uno per ogni bidone presente sulla mappa e in questo modo è possibile fare interagire il personaggio in maniera diversa in base al tile in cui si trova.

Di seguito a sinistra la mappa visibile nel gioco mentre a destra è visibile il layer che contiene i tile con cui il personaggio interagisce.

![Class Diagram](media/ostacoli-ex.png)

Altri layer nascosti sono stati creati per identificare delle zone particolari della mappa in cui far apparire solo un certo tipo di rifiuti, come ad esempio i rifiuti plastici che appaiono solo nella spiaggia.

<b><h2>Conclusioni e sviluppi futuri</b></h2>

Lo sviluppo di questo gioco è stato molto importante per la comprensione e la pratica della creazione di giochi 2D utilizzando il linguaggio C\# e Monogame. L’uso della programmazione ad oggetti ci ha permesso di affinare le nostre capacità da programmatore utilizzando tecniche quali polimorfismo ed ereditarietà.

Il lavoro di gruppo non sarebbe stato possibile se non avessimo usato il sistema di controllo versione (VCS) Git. In corso d’opera abbiamo fatto diversi cambiamenti e grazie a sempre nuove idee abbiamo migliorato il gioco passo dopo passo.

Recycle Game può essere ancora migliorato e ampliato. Abbiamo pensato che sarebbe interessante implementare un sistema di salvataggio dello stato del gioco in modo da recuperare una partita interrotta. Inoltre, con lo scopo di ampliare le prospettive del gioco, potrebbe essere utile aggiungere nuove mappe, nuovi oggetti e la possibilità del giocatore di usare degli *strumenti* che permettano una raccolta differenziata più efficiente.
