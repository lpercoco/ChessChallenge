# ChessChallenge

Desarrollar una aplicación (usando la[s] tecnología[s] en la que te sientas más cómodo), donde hemos de modelar algunas de las reglas del ajedrez.


Para ello, supongamos unas reglas de ajedrez muy simplificadas. (dejamos de lado el enroque, promoción, jaque, jaque mate, en passant, etc.)

Tenemos un tablero de 8x8 (64 casilleros), en las que participan dos jugadores diferenciados por los colores de sus piezas (denominadas blancas y negras). Las filas se numeran del 1 al 8 y las columnas con las letras de la “a” a la “h”, tal como indica la figura. Los casilleros del tablero también se distinguen por su color, a menudo diferenciados por claros y oscuros. El casillero con coordenadas (1,a) es “oscuro”, el (1,b) es “claro”, y así alternamos sucesivamente hasta generar el patrón que vemos en la imagen.

Cada lado tiene 16 piezas, que se distribuyen como se indica:

    ocho Peones.
    dos Torres.
    dos Caballos.
    dos Alfiles.
    una Reina.
    un Rey.

Cada tipo de pieza tiene sus propias reglas de movimiento:

    Los peones solo pueden avanzar hacia adelante. (para las blancas, de fila 1 hacia fila 8 y para las negras de fila 8 hacia fila 1, a menos que tengan otra pieza enfrente bloqueando el camino)
    Las torres pueden avanzar horizontal o verticalmente, hacia atrás o adelante, tantos casilleros como quieran. (a menos que sean obstaculizadas por otra pieza)
    Los caballos se pueden mover en “L”, para atrás o adelante, izquierda o derecha. Esto significa que:
        Pueden moverse 1 casillero hacia atrás/adelante, y 2 a la derecha/izquierda.
        O, alternativamente, 2 casilleros hacia atrás/adelante, y 1 a la derecha/izquierda.

Adicionalmente, los caballos son las únicas piezas que pueden “saltear” obstáculos (otras piezas, blancas y negras) al moverse.

    Los alfiles se pueden mover en diagonal, hacia atrás y adelante, tantas piezas como quieran. (a menos que sean obstaculizadas)
    La reina puede moverse en diagonal, hacia atrás y adelante, y hacia los costados, tantas piezas como quieran. (a menos que sea obstaculizada)
    Finalmente, el rey puede moverse a cualquiera de los casilleros que lo rodean.

 

Tenemos cuatro consignas:

    El primer paso es generar una distribución al azar de las piezas en el tablero. No es necesario sea un juego posible; por esto nos referimos a que sea posible mover las piezas de su posición inicial cuando comienza el juego de tal manera que lleguemos a esta distribución. El único requisito es que las 32 piezas estén en el tablero, y que cada bando tenga uno de sus alfiles en un casillero “oscuro” y otro en un casillero “claro”. (si uno se fija en la imagen de la posición inicial y considera cómo se mueven los alfiles, no hay manera de impedir que haya un alfil “oscuro” y otro “claro” por cada jugador)
    Dibujar en pantalla la distribución del tablero. No tenemos requerimientos fuertes en el método o la librería que utilices para ello, la que te sientas más cómodo. Solo necesitamos poder observar el estado del tablero y razonar sobre él a la hora de realizar debugging. Algunas opciones son:
        en el standard output de una aplicación de consola a la terminal. (la opción más frecuentemente utilizada)
        en el browser, tanto dibujando y generando HTML apropiado o usando los métodos de console para generar el output.
        en una aplicación nativa usando el framework de UI de tu preferencia.
        utilizando canvas, WebGL, u otro API de gráficos que prefieras.
    Permitir que el usuario seleccione una pieza ingresando fila y columna. (por ejemplo, “2c”)
        No importa si la pieza es blanca o negra, no es necesario alternar entre el jugador de blancas y negras como en un juego de ajedrez propiamente dicho,
        Obviamente, si no hay pieza en ese casillero debería emitir un error y permitir que reintentemos.

Una vez que “seleccionamos” una pieza, permitir otro ingreso de coordenadas como antes, con el objetivo de “mover” la pieza seleccionada a ese casillero. Resolver si la movida es válida (para las reglas de la pieza seleccionada) y en caso contrario, informar al usuario por qué no es posible. Algunas razones pueden ser:

    Otra pieza en el camino.
    No se pueden comer piezas del mismo color.
    No es una movida válida para esta pieza.

    Redibujar el tablero con la nueva disposición y permitir que continuemos con el juego.
