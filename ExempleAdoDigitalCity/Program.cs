// See https://aka.ms/new-console-template for more information
using ExempleAdoDigitalCity;
using Microsoft.Data.SqlClient;
using Models;
using System.Data.Common;
 




#region LEcture
//Me connecter à la DB
//Définir le chemin contant l'ip, les informations d'authenfication etc...
string connectionString = @"Data Source=LENOMIKE\TFTIC2022;Initial Catalog=ExempleAdo;Integrated Security=True;Connect Timeout=30;Encrypt=true;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
//Créer un objet permettant de se connecter

#endregion
DBManager dBManager = new DBManager(connectionString);
//insertion d'une joke
Jokes lablag= new Jokes();
Console.WriteLine("Entrez le titre de la blague");
lablag.Title = Console.ReadLine();
Console.WriteLine("Entre le corps de la blague");
lablag.Body = Console.ReadLine();

if(dBManager.InsertJokes(lablag))
    Console.WriteLine("Insertion effectuée");
