Recycle Game

Un video gioco di Alessandro Maggi e Davide Orengo

Esame di *Computer graphics arch.- games and simulation*

Obiettivi

Creare un gioco, in particolare un serious game (gioco non solo di intrattenimento ma anche educativo), con lo scopo di sensibilizzare e insegnare come si effettua la raccolta differenziata creando quindi un’esperienza formativa, efficace e piacevole per il giocatore.

Introduzione

Lo sviluppo del progetto è stato eseguito utilizzando il framework Monogame e Visual Studio sfruttando le conoscenze apprese nel corso di COMPUTER GRAPHICS ARCH.- GAMES AND SIMULATION e approfondendo i concetti necessari per la realizzazione.

Il nome del gioco è “RecycleGame” in quanto l’argomento trattato è la raccolta differenziata.

È stata scelto questo tema perché lo smaltimento dei rifiuti è un problema sempre più sentito sia dai cittadini che dalle amministrazioni, a causa dell’incredibile quantità di immondizia che la società moderna produce, di conseguenza una delle soluzioni principali è la raccolta differenziata che grazie alla collettività può portare notevoli vantaggi.

Il gioco è rivolto in particolare ai più piccoli ma anche a chiunque voglia apprendere le basi della raccolta differenziata in modo tale da applicare lo stesso approccio nel mondo reale. Il gioco indica al giocatore se sta svolgendo correttamente il riciclo dei rifiuti fornendo vari strumenti che indicano il progresso del gioco ed inoltre permette di consultare un libretto informativo per avere più dettagli sul riciclaggio europeo.

Il giocatore può esplorare i dintorni esterni di una cittadina utilizzando il suo personaggio e può raccogliere i diversi rifiuti che trova in giro e in qualunque momento può dirigersi verso un bidone della spazzatura per depositare i rifiuti di una specifica tipologia.

Il gioco presenta un punteggio che rappresenta l’esperienza del giocatore e cambia dipendentemente dalle scelte giuste o sbagliate nella differenziazione dei rifiuti. Quindi se l’azione di depositare un rifiuto in un bidone specifico è sbagliata verranno tolti dei punti invece se è corretta verranno aggiunti.

Il gioco è diviso per livelli e ogni livello è rappresentato da un diverso range di punteggio, il primo livello fornisce al giocatore un numero limitato di tipologie di rifiuti e i corrispettivi bidoni, di conseguenza i livelli successivi sbloccheranno nuove tipologie di rifiuti e bidoni con cui interagire.

L’interazione tra rifiuti e bidoni avviene attraverso una finestra del gioco che permette di scegliere tra i rifiuti raccolti quali buttare nel bidone corrispondente.

**MonoGame framework**

Recycle Game è stato creato utilizzando MonoGame un framework open-source per videogiochi multipiattaforma: permette di effettuare il porting dei videogiochi per molte piattaforme fra cui Windows, Linux, iOS e android. MonoGame consente lo sviluppo di videogiochi per mezzo del linguaggio di programma zione ad oggetti C\# offrendo numerose classi che contengono indispensabili strumenti.

La struttura di un gioco creato con Monogame è essenzialmente basata su tre classi:

-   *Game*: cuore del videogioco che nonché entry point del programma. Contiene le interfacce dei metodi indispensabili per il funzionamento del gioco chiamati dalla classe base:

    -   *Inizialize() *: contiene l’inizializzazione di base del comparto grafico e di input;
    -   *LoadConten()*: carica i contenuti multimediali
    -   *Update()*: metodo chiamato prima del render di ogni frame. Prende in input un oggetto GameTime utilizzato per ottenere molte informazioni sullo stato temporale del gioco
    -   *Draw()*: esegue il render a schermo di tutti gli elementi del gioco. Come Update() prende in input un oggetto GameTime
-   *Graphics.SpriteBatch*: consente il render di tutti gli oggetti di tipo Texture2D
-   *Content.ContentManage*r: consente di gestire tutte le risorse multimediali del gioco. A questo scopo il framework mette a disposizione lo strumento *Content Pipeline *che per mezzo di una interfaccia grafica permette una gestione facilitata dei contenuti.

La struttura di Recycle Game

Recycle Game è stato creato utilizzando molte classi diverse fra loro e spesso si è fatto uso di Ereditarietà e Polimorfismo al fine di gestire numerosi oggetti grafici con comportamenti molteplici e interazioni differenti. Di seguito è riportato il diagramma delle classi più significative del videogioco:

**Le Classi principali**

***Component***: è la classe base del videogioco, contiene i riferimenti base che ogni componente deve avere ovvero ContentManager, GraphicsDeviceManager, Game. Da questa classe derivano la quasi totalità delle altre classi.

***State:**** il funzionamento del gioco è basato su una logica a macchina a stati la quale è implementata all’interno della classe *Game1*, quest’ultima derivata direttamente da *Game.* Ogni stato è un eredita dalla classe *State* i metodi Draw() e Update() i quali ricoprono la stessa funzione degli omonimi metodi contenuti nella classe Game1. La classe Game1 contiene due proprietà *\_currentState *e *\_nextState *che fanno riferimento allo stato precedente e quello successivo. La prima viene modifica nel metodo Update() di Game1 ogni qualvolta la variabile *\_nextState *contiene uno stato nuovo del gioco. La variabile dello stato successivo viene modificata all’interno degli stessi stati ogni volta che ce n’è bisogno. In un altro paragrafo di questa relazione verranno illustrati i singoli stati.

***Game1***: devira da *Game* e per questo è la classe entry-point del gioco. Contiene le inizializzazioni del gioco, la gestione degli stati e dei livelli. Al suo interno vengono implementati i metodi Update() e Draw() del framework MonoGame.

***Sprite***: un oggetto di questa classe rappresenta un singolo sprite le cui proprietà sono molteplici fra le quali le più importanti sono:
    -   *texture:* la texture dello sprite
    -   *rect:* il rettangolo in cui è contenuto lo sprite
    -   *gamePos:* posizione dello sprite rispetto alla mappa del gioco
    -   *displayPos:* posizione dello sprite rispetto allo schermo
    -   *scale:* misura dello scalamento della texture

La classe sprite contiene tutti i metodi necessari per gestire il movimento e la posizione di ogni singolo elemento:

-   setPos(), setGamePos(): modifica la posizione in modo assoluto
-   stepPos(), stepGamePos(): modifica la posizione di una certa quantità
-   setTilePos(): modifica la posizione con riferimento alla mappa dei tile

Possiede poi dei rispettivi metodi get\#\#\#() che servono per ottenere i valori. Da questa classe derivano 5 altre classi. La classe *sprite *contiene una funziona Draw() basilare che spesso viene rimpiazzata da una Draw() più specifica nelle classi derivate.

***Animated Sprite***:conferisce ad ogni singolo sprite una animazione data dal susseguirsi di frame i quali sono raggruppati in una unica texture. La tecnica di animazione si basa sul mostrare solo una porzione della texture nella quale è presente il frame che si vuole visualizzare.

***Character***: derivata da *Sprite*, questa classe consente di creare un personaggio capace di muoversi nel gioco e interagire con gli ostacoli presenti nella mappa. Ogni oggetto Character può essere manipolato da tastiera con i comandi WASD grazie al metodo *keyboardMgnt* presente all’interno del *MainState*

***Item***: questa classe consente di gestire i singoli oggetti del gioco che rappresentano i rifiuti come *Sprite*. Ogni *Item* possiede una proprietà *type* che conferisce il tipo di rifiuti a partire da un elenco enumerativo. Inoltre, grazie ad una lookup table della funzione seno ogni *item *è capace di muoversi su e giù conferendo un effetto animato utile per individuare la presenza di un item all’interno della mappa.

***SpeechBubble***: crea una nuvoletta contenente un qualsiasi testo che viene visualizzata ogni volta che viene attivata mediante una variabile enable. Questa classe viene utilizzata all’interno della finestra in cui si trovano tutti i simboli della raccolta differenziata. Ogni volta che la freccia del mouse passa sopra ad un simbolo si attiva una speechBubble che ne contiene la descrizione. La speechBubble può inoltre essere utilizzata con un Character per dare l’idea che stia parlando.

***Vocabulary***: questa classe crea una finestra sotto forma di taccuino al centro dello schermo che mostra in sequenza i simboli del riciclo con le loro descrizioni, tutti i bidoni e tutti gli oggetti presenti all’interno del gioco. Per passare da una pagina all’altra si utilizzano le frecce poste alla base del taccuino. La gestione di quale pagine mostrare viene fatta tramite l’uso dei delegati: il delegato che viene chiamato all’interno della Draw() viene modificato all’interno di Update() inserendo il metodo che mostrerà la pagina corretta.

<span id="anchor"></span>***Windows***: crea una finestra interattiva che permette di interagire con i rifiuti che si sono raccolti e il bidone disponibile.

Per costruire la finestra è stata scritta la classe *Windows* che comprende i metodi Draw() e Update() e altri per poter gestirne l’interazione con il giocatore.

La finestra presenta una texture di backgroud su cui vengono posizionati vari elementi, a sinistra:

-   una texture “card” che comprende l’immagine del rifiuto e i simboli specifici per quel tipo di riciclaggio;

NOTA: quando non è presente nessun rifiuto compare una card di default con scritto “nessun rifiuto”.

-   un numerino rappresentato dalla classe *Banner* che mostra il numero della “card” che si sta visualizzando (la sequenza delle card seguono l’ordine di raccolta)
-   una freccia destra e una freccia sinistra entrambe rappresentate dalla classe *Button* che, quando una viene cliccata, permette di passare da una card a un’altra così si possono raggiungere i rifiuti da depositare nel bidone disponibile

A destra è presente un “puzzle” composto da quadratini con ciascuno il nome di una tipologia di rifiuto, ciascun quadratino è rappresentato dalla classe *Button* poi è presente anche una texture di un riquadro con la scritta “prossimamente” che cambia rispetto al livello in cui si trova lo stato del gioco. Il riquadro indica che nel livello successivo saranno disponibili nuovi quadratini per nuove tipologie di rifiuto.

La classe presenta due texture di contorno una verde e una rossa che verranno mostrate lampeggiando al posto del contorno bianco quando il giocatore depositerà un rifiuto, se la risposta è corretta appare quella verde al contrario quello rossa.

Per visualizzare la finestra il giocatore deve posizionarsi davanti a un bidone e premere “spazio” sulla tastiera così verrà modificata la variabile di abilitazione, ciò verrà effettuato utilizzando i tile di cui si parlerà in seguito. Per uscire dall finestra bisogna premere il tasto Esc che modifica a sua volta l’abilitazione.

Aperta la finestra sarà possibile premere con il mouse solo il quadratino riferito a quel bidone specifico per depositare il rifiuto visualizzato, invece gli altri quadratini appariranno scoloriti e quindi non sarà possibile utilizzarli.

**Gli stati del gioco**

Come già anticipato il RecycleGame è strutturato come una macchina a stati i quali sono tutti oggetti della classe State e ognuno implementa una propria Draw() e Update(). Lo stato iniziale viene impostato all’interno della classe Game1 nel metodo *LoadContent *e nei metodi Draw e Update di Game1 vengono chiamati i corrispettivi all’interno del *\_currentState*.

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

Tiled e le matrici di tile

Tiled è un software gratuito e open source che è utilizzato per creare mappe 2D per videogiochi mediante la tecnica dei tile. Un tile è una porzione quadrata di schermo con una dimensione in genere di 16x16px o 32x32px come in RecycleGame. Ogni tile viene riempito con una texture fino a comporre una griglia la quale rappresenta la mappa del gioco, detta TileMap. Tiled permette la progettazione della mappa mediante layer di tile sovrapposti. L’unico layer visualizzato però sarà quello più in alto degli altri. Le TileMap vengono salvate in un file .tmx che contiene tutti i layer in codifica xml contenente sotto forma di array bidimensionali, righe e colonne della tilemap. Ogni elemento degli array è un numero che rappresenta una precisa texture di 32x32px.

Per utilizzare una mappa creata con Tiled in monogame è sufficiente esportare la mappa creata in formato .png o .jpg e poi importare quest’ultima nel LoadContent. Nell’immagine esportata però saranno visibili solo i tile dei layer che sono stati resi visibili.

In RecycleGame è stato implementato un metodo per leggere il file .tmx della tilemap e codificare tutti i layer in liste bidimensionali. In questo modo sapendo che la mappa è una griglia di 32x32px è possibile in runtime associare ogni singolo pezzetto della mappa a tutti i tile in quel punto per ogni layer. Così si possono creare dei layer nascosti che hanno lo scopo di indentificare delle zone della mappa particolari, come ad esempio gli ostacoli.

Per fare un esempio, nella figura di sinistra vi è un pezzo della mappa visibile nel gioco, nella figura di destra invece la stessa mappa ma con il layer contenente gli ostacoli che è stato reso visibile.

Il Character quando si sposta all’interno della mappa analizza tutti i tile con cui entra a contatto. Grazie ai layer nascosti è possibile capire se si trova davanti ad un ostacolo oppure no.

Con la stessa tecnica è stata resa possibile l’interazione diversa con altri elementi sparsi nel gioco: è presente un layer che contiene tile diversi uno per ogni bidone presente sulla mappa e in questo modo è possibile fare interagire il personaggio in maniera diversa in base al tile in cui si trova.

Di seguito a sinistra la mappa visibile nel gioco mentre a destra è visibile il layer che contiene i tile con cui il personaggio interagisce.

Altri layer nascosti sono stati creati per identificare delle zone particolari della mappa in cui far apparire solo un certo tipo di rifiuti, come ad esempio i rifiuti plastici che appaiono solo nella spiaggia.

Conclusioni e sviluppi futuri

Lo sviluppo di questo gioco è stato molto importante per la comprensione e la pratica della creazione di giochi 2D utilizzando il linguaggio C\# e Monogame. L’uso della programmazione ad oggetti ci ha permesso di affinare le nostre capacità da programmatore utilizzando tecniche quali polimorfismo ed ereditarietà.

Il lavoro di gruppo non sarebbe stato possibile se non avessimo usato il sistema di controllo versione (VCS) Git. In corso d’opera abbiamo fatto diversi cambiamenti e grazie a sempre nuove idee abbiamo migliorato il gioco passo dopo passo.

Recycle Game può essere ancora migliorato e ampliato. Abbiamo pensato che sarebbe interessante implementare un sistema di salvataggio dello stato del gioco in modo da recuperare una partita interrotta. Inoltre, con lo scopo di ampliare le prospettive del gioco, potrebbe essere utile aggiungere nuove mappe, nuovi oggetti e la possibilità del giocatore di usare degli *strumenti *che permettano una raccolta differenziata più efficiente.
