// See https://aka.ms/new-console-template for more information
using Microsoft.Data.SqlClient;
using System.Data.Common;
 




#region LEcture
//Me connecter à la DB
//Définir le chemin contant l'ip, les informations d'authenfication etc...
string connectionString = @"Data Source=LENOMIKE\TFTIC2022;Initial Catalog=ExempleAdo;Integrated Security=True;Connect Timeout=30;Encrypt=true;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
//Créer un objet permettant de se connecter
DbConnection connexion = new SqlConnection(connectionString);
try
{

    //Appeler la méthode qui permettra de se connecter
    //et Vérifier qu'on est connecte
    connexion.Open();

}
catch (SqlException ex)
{
    Console.WriteLine($"Erreru de connexion db : {ex.Message}");
}


if (connexion.State == System.Data.ConnectionState.Open)
{

    //Exécuter une requête select
    //string sqlQuery = @"GetJokes";
    //DbCommand command = connexion.CreateCommand();
    //command.CommandText = sqlQuery;
    //command.CommandType = System.Data.CommandType.StoredProcedure;
    //command.Parameters.Add(new SqlParameter("motcle", "vache"));

    try
    {
        string sqlQuery = @"Select Id, Title, Body 
                        From Jokes";
        DbCommand command = connexion.CreateCommand();
        command.CommandText = sqlQuery;
        //Afficher le résultat de ma requête
        DbDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            int? id = reader["Id"] == DBNull.Value ? null : (int?)reader["Id"];
            string? titre = reader["Title"] == DBNull.Value ? "" : reader["Title"].ToString();
            Console.WriteLine(titre);
        }
    }
    catch (SqlException ex)
    {
        Console.WriteLine(ex.Message);
    }



}

//je ferme
try
{


    connexion.Close();
}
catch (SqlException sqex)
{
    Console.WriteLine(sqex.Message);
} 
#endregion

//insertion d'une joke

//connexion
DbConnection oCon = new SqlConnection(connectionString);
// ouvrir
try
{
    oCon.Open();
}
catch (SqlException sqlex)
{
    Console.WriteLine(sqlex.Message);
}
//si je suis connecté
if(oCon.State == System.Data.ConnectionState.Open)
{
    //Vient de la console par exemple
    Console.WriteLine("Encodez le titre et le corps de la blague");
    string titre = Console.ReadLine(); 
    string body = Console.ReadLine(); 

    //commande
    DbCommand insertCommand = oCon.CreateCommand();
   
    // requête
    string maRequete = @"INSERT INTO JOKES (Title, Body) VALUES (@monTitre, @body)";

    //Version Longue
    DbParameter param2 = new SqlParameter();
    param2.ParameterName = "body";
    param2.Value = body;

    // des infos a inserer
    insertCommand.CommandText = maRequete;
    //Version rapide mais moins lisible
    insertCommand.Parameters.Add(new SqlParameter("monTitre", titre));
    insertCommand.Parameters.Add(param2);
    // executer
    insertCommand.ExecuteNonQuery();
}


//fermer
try
{
    oCon.Close();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}