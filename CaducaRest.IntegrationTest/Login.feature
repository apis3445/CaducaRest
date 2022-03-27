# language: es
Característica: Login
	Dado que ya existen los siguientes usuarios:
	Carlos DtfhkmTRQ8mNzgRY Administrador
	Maria 8cYyY8paESGbzC5E  Vendedor
	Juan zUvyvsRSCMek58eR   Cliente

@Login
Escenario: Login con usuario Administrador
	Dado El usuario administrador tiene la clave Carlos
	Y Y tiene el password DtfhkmTRQ8mNzgRY	
	Cuando Yo ejecuto el servicio Usuarios/Login con esos datos
	Entonces El resultado deberia ser Ok 

Escenario: Login con usuario Vendedor
	Dado Tecleo los siguientes datos del usuario
	| Usuario		| Password			|
	| Maria			| 8cYyY8paESGbzC5E	|
	Cuando Yo ejecuto el servicio Usuarios/Login con esos datos
	Entonces El resultado deberia ser Ok 