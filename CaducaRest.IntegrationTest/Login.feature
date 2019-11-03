Feature: Login
	Dado que ya existen los siguientes usuarios:
	Carlos DtfhkmTRQ8mNzgRY Administrador
	Maria 8cYyY8paESGbzC5E  Vendedor
	Juan zUvyvsRSCMek58eR   Cliente

@Login
Scenario: Login con uusuario Administrador
	Given El usuario administrador tiene la clave Carlos
	And Y tiene el password DtfhkmTRQ8mNzgRY	
	When Yo ejecuto el servicio Usuarios/Login con esos datos
	Then El resultado deberia ser Ok 
