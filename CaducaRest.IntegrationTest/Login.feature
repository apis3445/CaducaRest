# language: es
Característica: Login
	Dado que ya existen los siguientes usuarios:
    Usuario  Nombre
	Carlos   Carlos Hernández 
	Maria    Maria Lopez
	Juan     Juan Peréz

@Login
Escenario: Login con un usuario
	Dado Que Existe un usuario con la clave Carlos
	Cuando Yo ejecuto el servicio Usuarios/Login 
	Entonces Obtengo el nombre Carlos Hernández

Escenario: Login con usuarios en Tabla
	Dado Tecleo los siguientes datos del usuario
	| Usuario		| Nombre		| 
	| Maria			| Maria Lopez	|
	Cuando Yo ejecuto el servicio Usuarios/Login
	Entonces Obtengo el nombre <Nombre>

Ejemplos: 
	| Usuario		| Nombre		|
	| Maria			| Maria Lopez	|
    | Juan			| Juan Peréz	|