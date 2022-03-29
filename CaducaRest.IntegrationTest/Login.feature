# language: es
Característica: Login
	Dado que ya existen los siguientes usuarios:
	Carlos  Administrador
	Maria   Vendedor
	Juan    Cliente

@Login
Escenario: Login con usuario Administrador
	Dado El usuario administrador tiene la clave Carlos
	Y Y tiene el password  	
	Cuando Yo ejecuto el servicio Usuarios/Login con esos datos
	Entonces El resultado deberia ser Ok 

Escenario: Login con usuario Vendedor
	Dado Tecleo los siguientes datos del usuario
	| Usuario		| Password			|
	| Maria			| 	|
	Cuando Yo ejecuto el servicio Usuarios/Login con esos datos
	Entonces El resultado deberia ser Ok 