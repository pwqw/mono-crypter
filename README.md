Mono-Crypter
============

## Simple crypter writed in C# Mono

### Uso:

Mono-Crypter *crea* o *edita* un archivo *.conf* que contiene: 
* La ConnectionString a la base de datos
* El nombre del súper usuario del sistema
* La contraseña del súper usuario del sistema,

está cifrado bajo el algoritmo DES. 

Para poder acceder a ésta información se desarrolló un Crypter el cual lee el archivo cifrado y muestra los datos en la interfaz gráfica del programa (los mantiene en memoria), es decir, los datos desencriptados nunca se almacenan en el disco rígido.
![Screenshot](https://github.com/AlexisCaffa/mono-crypter/blob/master/Readme-files/1.png "Screenshot 1")

Puedo crear un archivo nuevo o seleccionar uno existente para editarlo. Al apretar en el control para seleccionar archivo existente:
![Seleccionar archivo](https://github.com/AlexisCaffa/mono-crypter/blob/master/Readme-files/2.png "Screenshot 2")

Finalmente:
![Archivo desencriptado](https://github.com/AlexisCaffa/mono-crypter/blob/master/Readme-files/3.png "Screenshot 3")

Ejemplo en XP:
![Screenshot](https://github.com/AlexisCaffa/mono-crypter/blob/master/Readme-files/4.png "Screenshot 4")

Fin.
