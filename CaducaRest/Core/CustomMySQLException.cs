using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.Core
{
    /// <summary>
    /// Mensajes personalizados para mysql
    /// </summary>
    public class CustomMySQLException
    {
        /// <summary>
        /// Definción de los mensajes de acuerdo a los códigos de error que arroje MySql cuando encuentre error.
        /// </summary>
        /// <param name="mysqlError">Excpeción del MySql.</param>
        /// <param name="tablaActual">Nombre de la tabla en donde se este haciendo la transacción es solo para
        /// complementar el mensaje de error.</param>
        /// <example>" el usuario " ó " la página "</example>
        /// <param name="claseOrigen">Clase en donde se este haciendo la transacción.</param>
        public string MuestraErrorMYSQL(MySqlException mysqlError, string tablaActual, string claseOrigen)
        {

            try
            {
                //Por el momento no se está usando la variable mensajeInicial
                //string mensajeTitulo = "Ha ocurrido un error interno.";
                string mensajeInicial = "Error interno en la base de datos";
                string mensajeDetalle = "Intente nuevamente si continua el error por favor comuníquese con el Administrador del Sistema he infórmele sobre el error ocurrido" + System.Environment.NewLine + System.Environment.NewLine + "(" + mysqlError.Message.ToString() + ") en " + mysqlError.Source.ToString();

                string tablaUno;
                string tablaDos;
                switch (mysqlError.Number)
                {
                    //Se verifica el número de un posible error y se evalúa
                    case 1370:
                        mensajeInicial = "El usuario no tiene permiso para realizar esta acción ";
                        break;
                    case 1451:
                        string[] cadena = mysqlError.Message.ToString().Split(Convert.ToChar("`"));
                        tablaUno = cadena[3];
                        tablaDos = cadena[9];
                        mensajeInicial = "No es posible borrar o actualizar (el/la) " + tablaDos + " seleccionado(a) debido a que ya encuentran " + tablaUno + "(s) asociados a este(a) " + tablaDos;
                        //mensajeTitulo = "Imposible borrar o actualizar";
                        mensajeDetalle = "Para borrar o actualizar (el/la)" + tablaDos + " debe eliminar los registros en (el/la) " + tablaDos + " y después intentar nuevamente.";

                        break;
                    case 1452:
                        string[] cadenas = mysqlError.Message.Split(Convert.ToChar("`"));
                        tablaUno = cadenas[3];
                        //se obtiene el nombre/descripción de la tabla
                        tablaDos = cadenas[9];
                        //se obtiene el nombre/descripción de la tabla
                        mensajeInicial = "No es posible agregar o actualizar (el/la) " + tablaDos + " seleccionado(a) debido a que ya encuentran " + tablaUno + "(s) asociados a este(a) " + tablaDos;
                        //mensajeTitulo = "Imposible agregar o actualizar";
                        mensajeDetalle = "Para agregar o actualizar (el/la)" + tablaDos + " debe ingresar (un/una) " + tablaUno + " existente.";

                        break;
                    case 1062:
                        tablaUno = tablaActual;
                        string[] erroers = mysqlError.Message.Split(Convert.ToChar("'"));
                        mensajeInicial = "No es posible agregar " + tablaUno + "ya existe";
                        break;
                    case 1014:
                    case 1015:
                    case 1016:
                    case 1017:
                    case 1018:
                    case 1019:
                        mensajeInicial = "No se puede abrir/leer el archivo o directorio de la base de datos";

                        break;
                    case 1020:
                        mensajeInicial = "El registro ha cambiado desde la última lectura ( " + tablaActual + ")";

                        break;
                    case 1021:
                        mensajeInicial = "Disco lleno, esperando para que se libere algo de espacio";
                        break;
                    case 1042:
                        mensajeInicial = "No fue posible conectarse al servidor ... Es posible que el servidor este apagado o existan problemas con la red";
                        break;
                    case 1207:
                    case 1401:
                    case 1412:
                    case 1480:
                        mensajeInicial = "Operación en proceso... El proceso no puede continuar porque la base de datos tiene tablas bloqueadas o una transacción activa";

                        break;
                    case 1406:
                        //mensajeTitulo = "Incorrecta longitud de datos";
                        mensajeInicial = "La longitud de los datos es muy larga";

                        break;
                    case 1431:
                        mensajeInicial = "Ha ocurrido un error interno. Identificador referencial no existe";

                        break;
                    case 1088:
                        //mensajeTitulo = "Registros duplicados";
                        mensajeInicial = "No es posible agregar o actualizar, intente nuevamamente";

                        break;
                    case 1091:
                        tablaUno = tablaActual;
                        //se obtiene el nombre/descripción de la tabla
                        //mensajeTitulo = "No es posible borrar";
                        mensajeInicial = "No es posible borrar (el/la) " + tablaUno + " porque ya no existe";
                        mensajeDetalle = "Salga de esta opción y verifique que no exista (el/la) " + tablaUno + " de lo contrario intente nuevamente por favor.";

                        break;
                    case 1132:
                        mensajeInicial = "Debes de tener permiso para actualizar tablas en la base de datos y para cambiar las claves";

                        break;
                    case 1044:
                    case 1045:
                        //mensajeTitulo = "Acceso denegado al sistema";
                        mensajeInicial = "Por el momento el sistema no se encuentra disponible, debido a una restricción de accesos. El sistema no registrará la transacción que usted está procesando, el error ha sido resgistrado y será solucionado.";
                        mensajeDetalle = "Comuníquese con el Administrador del Sistema he infórmele sobre el error ocurrido";

                        break;
                    case 1049:
                    case 1102:
                        mensajeInicial = "Base de datos desconocida/Nombre de base de datos incorrecto";

                        break;
                    case 1053:
                        mensajeInicial = "Desconexión de servidor en proceso";
                        break;
                    case 1130:
                        mensajeInicial = "El equipo no permite conectarse al servidor de la base de datos. Probablemente el servidor se esta actualizando.";

                        break;
                    case 1152:
                        mensajeInicial = "Conexión abortada para este usuario";

                        break;
                    case 2003:
                    case 2002:
                        //mensajeTitulo = "Acceso denegado al sistema";
                        mensajeInicial = "El servicio de MYSQL Server no se inicio, debe iniciar el servicio de MYSQL para que el sistema funcione.";

                        break;
                    case 1146:
                        /*cadena = mysqlError.Message.Split(Convert.ToChar("'"));
                        mensajeInicial = "La Tabla de la base de datos (" + cadena[1] + ") no existe";
                        */
                        break;
                    case 1149:
                        mensajeInicial = "Error de sintaxis al momento de ejecutar una consulta a la base de datos";

                        break;
                    case 1166:
                        mensajeInicial = "Error de sintaxis al momento de ejecutar una consulta a la base de datos, nombre de columna incorrecto";

                        break;
                    case 1179:
                        mensajeInicial = "No tiene el permiso para ejecutar este comando en una transacción";

                        break;
                    case 1012:
                        mensajeInicial = "No puedo leer el registro en la tabla del sistema";

                        break;
                    case 1104:
                        mensajeInicial = "La consulta que se esta realizando puede obtener muchos registros y probablemente tarde mucho tiempo";

                        break;
                    case 1105:
                        mensajeInicial = "Error desconocido";

                        break;
                    case 1107:
                    case 1108:
                        mensajeInicial = "Parámetros incorrectos para procedimiento almacenado";

                        break;
                    case 1114:
                        mensajeInicial = "La tabla " + tablaActual + " esta llena";

                        break;
                    case 1205:
                        //mensajeTitulo = "Error en transacción";
                        mensajeInicial = "Expiró el tiempo de espera para realizar un bloqueo. La transacción se canceló";

                        break;
                    case 1213:
                        //mensajeTitulo = "Error en transacción";
                        mensajeInicial = "Ocurrió un deadlock durante una transacción";

                        break;
                    case 1181:
                        //mensajeTitulo = "Error en transacción";
                        mensajeInicial = "Ocurrío un error al momento de regresar a un punto en la transacción";

                        break;
                    case 1216:
                        mensajeInicial = "Se está intentando agregar una fila, pero no hay una fila padre, lo que hace que una restricción de clave foránea falle. Se debe insertar antes la fila padre";

                        break;
                    case 1217:
                        mensajeInicial = "Se está intentando eliminar una fila padre que tiene filas hijas, lo que hace que una restricción de clave foránea falle. Se deben eliminar primero las filas hijas";

                        break;
                    case 2013:
                        mensajeInicial = "La consulta a la base de datos a tardado mucho tiempo";

                        break;
                    default:
                        mensajeInicial = "Error interno en la base de datos";
                        break;
                }
                return mensajeInicial;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

    }
}
