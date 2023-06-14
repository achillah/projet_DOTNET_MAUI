namespace MyReference.Services;

public partial class UserGestionService 
{

	public OleDbDataAdapter UsersAdapter = new();
	public OleDbConnection Connexion = new();

	public partial void ConfigOutils()
	{
		//Etape 1 : Etablir la connexion
		Connexion.ConnectionString = "Provider=Microsoft.ACE.OLEDB.16.0;"
									+ "Data Source=" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ServerDonnees", "UserAccounts.accdb")
									+ ";Persist Security Info=false";

        //Etape 2 : Creer les commandes
        string InsertCommandText = "INSERT INTO DB_Users (UserName,UserPassword,UserAccessType) VALUES (@UserName,@UserPassword,@UserAccessType);";
        string DeleteCommandText = "DELETE FROM DB_Users WHERE UserName = @UserName;";
        string SelectCommandText = "SELECT * FROM DB_Users ORDER BY User_ID;";
        //string UpdateCommandText = "UPDATE DB_Users SET UserPassword = @UserPassword, UserAccessType = @UserAccessType WHERE UserName = @UserName;";
        string UpdateCommandText = "UPDATE DB_Users SET UserName = @UserName, UserPassword = @UserPassword, UserAccessType = @UserAccessType WHERE User_ID = @User_ID;";




        OleDbCommand Insert_Command = new OleDbCommand(InsertCommandText, Connexion);
        OleDbCommand Delete_Command = new OleDbCommand(DeleteCommandText, Connexion);
        OleDbCommand Select_Command = new OleDbCommand(SelectCommandText, Connexion);
        OleDbCommand Update_Command = new OleDbCommand(UpdateCommandText, Connexion);


		UsersAdapter.InsertCommand = Insert_Command;
		UsersAdapter.DeleteCommand = Delete_Command;
		UsersAdapter.SelectCommand = Select_Command;
		UsersAdapter.UpdateCommand = Update_Command;

		UsersAdapter.TableMappings.Add("DB_Users", "Users");

		UsersAdapter.InsertCommand.Parameters.Add("@UserName", OleDbType.VarChar, 40, "UserName");
        UsersAdapter.InsertCommand.Parameters.Add("@UserPassword", OleDbType.VarChar, 40, "UserPassword");
        UsersAdapter.InsertCommand.Parameters.Add("@UserAccessType", OleDbType.Numeric, 100, "UserAccessType");

        UsersAdapter.DeleteCommand.Parameters.Add("@UserName", OleDbType.VarChar, 40, "UserName");

        UsersAdapter.UpdateCommand.Parameters.Add("@UserName", OleDbType.VarChar, 40, "UserName");
        UsersAdapter.UpdateCommand.Parameters.Add("@UserPassword", OleDbType.VarChar, 40, "UserPassword");
        UsersAdapter.UpdateCommand.Parameters.Add("@UserAccessType", OleDbType.Numeric, 100, "UserAccessType");
        UsersAdapter.UpdateCommand.Parameters.Add("@User_ID", OleDbType.Numeric, 100, "User_ID");
    }

    public async Task ReadAccessTable()
    {
        OleDbCommand SelectCommand = new OleDbCommand("SELECT * FROM DB_Access ORDER BY Access_ID;", Connexion);
		try
		{
			Connexion.Open();

			OleDbDataReader oleDbDataReader = SelectCommand.ExecuteReader();

			if(oleDbDataReader.HasRows)
			{
				while(oleDbDataReader.Read())
				{
					Globals.UserSet.Tables["Access"].Rows.Add(new object[] { oleDbDataReader[0], oleDbDataReader[1], oleDbDataReader[2], oleDbDataReader[3], oleDbDataReader[4], oleDbDataReader[5]});
				}
			}

			oleDbDataReader.Close();

		}
		catch (Exception ex)
		{

			await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
		}
		finally
		{
			Connexion.Close();
		}
    }

    public async Task ReadUserTable()
    {
        OleDbCommand SelectCommand = new OleDbCommand("SELECT * FROM DB_Users ORDER BY User_ID;", Connexion);
        try
        {
           
            Connexion.Open();

            OleDbDataReader oleDbDataReader = SelectCommand.ExecuteReader();

            if (oleDbDataReader.HasRows)
            {
                while (oleDbDataReader.Read())
                {
                    Globals.UserSet.Tables["Users"].Rows.Add(new object[] { oleDbDataReader[0], oleDbDataReader[1], oleDbDataReader[2], oleDbDataReader[3]});
                }
            }

            oleDbDataReader.Close();

        }
        catch (Exception ex)
        {

            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
        finally
        {
            Connexion.Close();
        }
    }

    /*public async Task RemplirUserTable()
    {
        Globals.UserSet.Tables["Users"].Clear();

        try
        {
            Connexion.Open();

			UsersAdapter.Fill(Globals.UserSet.Tables["Users"]);

			

        }
        catch (Exception ex)
        {

            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
        finally
        {
            Connexion.Close();
        }
    }*/

	public async Task InsertUser(string name, string password, Int32 access)
	{
        try
        {
            Connexion.Open();
            UsersAdapter.InsertCommand.Parameters[0].Value = name;
            UsersAdapter.InsertCommand.Parameters[1].Value = password;
            UsersAdapter.InsertCommand.Parameters[2].Value = access;

            if(UsersAdapter.InsertCommand.ExecuteNonQuery() != 0)
            {
                await Shell.Current.DisplayAlert("Database", "User inséré", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Database", "User non inséré", "OK");

            }


        }
        catch (Exception ex)
        {

            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
        finally
        {
            Connexion.Close();
        }
    }

    public async Task DeletetUser(string name)
    {
        try
        {
            Connexion.Open();
            UsersAdapter.DeleteCommand.Parameters[0].Value = name;
            

            if (UsersAdapter.DeleteCommand.ExecuteNonQuery() != 0)
            {
                await Shell.Current.DisplayAlert("Database", "User supprimé", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Database", "User non supprimé", "OK");

            }


        }
        catch (Exception ex)
        {

            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
        finally
        {
            Connexion.Close();
        }
    }

    public async Task UpdateUser(Int32 user_id, string name, string password, Int32 access)
    {
        UsersAdapter.UpdateCommand.Parameters[0].Value = name;
        UsersAdapter.UpdateCommand.Parameters[1].Value = password;
        UsersAdapter.UpdateCommand.Parameters[2].Value = access;
        UsersAdapter.UpdateCommand.Parameters[3].Value = user_id;
        try
        {
            Connexion.Open();

            UsersAdapter.UpdateCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {

            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
        finally
        {
            Connexion.Close();
        }
    }

}