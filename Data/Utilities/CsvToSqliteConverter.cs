namespace Portail_OptiVille.Data.Utilities
{
    using System;
    using System.Data;
    using System.IO;
    using Microsoft.Data.Sqlite;

    public class CsvToSqliteConverter
    {
        public async Task ConvertCsvToSqlite(string csvFilePath, string sqliteFilePath, string[] requiredColumns)
        {
            // Supprimer l'ancienne base SQLite si elle existe déjà
            if (File.Exists(sqliteFilePath))
            {
                File.Delete(sqliteFilePath);
            }

            // Créer le fichier SQLite
            File.Create(sqliteFilePath).Dispose();

            // Créer une connexion SQLite
            using (var connection = new SqliteConnection($"Data Source={sqliteFilePath};"))
            {
                connection.Open();

                // Lire le fichier CSV
                using (var reader = new StreamReader(csvFilePath))
                {
                    // Lire les en-têtes (colonnes du CSV)
                    var headers = reader.ReadLine()?.Split(',');

                    if (headers == null) throw new Exception("Le CSV ne contient pas d'en-têtes.");

                    // Trouver les indices des colonnes à conserver
                    var columnIndices = new List<int>();
                    foreach (var column in requiredColumns)
                    {
                        int index = Array.IndexOf(headers, column);
                        if (index != -1)
                        {
                            columnIndices.Add(index);
                        }
                        else
                        {
                            throw new Exception($"La colonne '{column}' n'existe pas dans le fichier CSV.");
                        }
                    }

                    // Créer la table avec les colonnes nécessaires dans SQLite
                    var createTableCommandText = $"CREATE TABLE Entreprise ({string.Join(", ", requiredColumns.Select(c => $"{c} TEXT"))})";
                    using (var createTableCommand = connection.CreateCommand())
                    {
                        createTableCommand.CommandText = createTableCommandText;
                        createTableCommand.ExecuteNonQuery();
                    }

                    // Insérer les lignes du CSV, mais uniquement les colonnes sélectionnées
                    string line;
                    int nbrLine = 0;
                    var insertCommand = connection.CreateCommand();
                    while ((line = reader.ReadLine()) != null && nbrLine < 6)
                    {
                        var values = line.Split(',');

                        // Filtrer les valeurs selon les indices des colonnes à garder
                        var filteredValues = columnIndices.Select(index => values[index]);

                        var insertCommandText = $"INSERT INTO Entreprise ({string.Join(", ", requiredColumns)}) VALUES ({string.Join(", ", filteredValues.Select(v => $"'{v}'"))})";

                        insertCommand.CommandText += insertCommandText;
                        nbrLine++;
                    }
                    insertCommand.ExecuteNonQuery();
                }

                connection.Close();
            }

            Console.WriteLine("CSV filtré et converti en SQLite avec succès.");
        }
    }

}
