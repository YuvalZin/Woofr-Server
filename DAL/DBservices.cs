﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using woofr.Models;
using System.Security.Cryptography;


/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{

    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    
    }
    //--------------------------------------------------------------------------------------------------
    // This method creates a token for the session over the application that is unique for a user. 
    //--------------------------------------------------------------------------------------------------
    public static string GenerateToken(string input)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Convert byte array to a string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2")); // Convert byte to hexadecimal string
            }
            return builder.ToString();
        }
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }


    //--------------------------------------------------------------------------------------------------
    //----------USER METHODES
    //--------------------------------------------------------------------------------------------------
    // User login by email and password
    public string LogIn(string email, string password)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@email", email);
        paramDic.Add("@password", password);


        cmd = CreateCommandWithStoredProcedure("SP_LoginUser", con, paramDic);             // create the command
        var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);

        returnParameter.Direction = ParameterDirection.ReturnValue;


        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (!dataReader.HasRows)
            {
                return null;
            }
            string token ="";
            while (dataReader.Read())
            {
                token = dataReader["Token"].ToString();
            }
            return token;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
            // note that the return value appears only after closing the connection
            var result = returnParameter.Value;
        }

    }  
    //not in use...
    public string GetFollowCount(string token)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@Token", token);

        cmd = CreateCommandWithStoredProcedure("SP_GetUserFollowData_ByToken", con, paramDic);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            string data = "error";

            if (dataReader.Read())
            {
                data = dataReader["followCount"].ToString();
                return data;
            }
            return data;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    //get user by token
    public User GetUser(string token)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@Token", token);

        cmd = CreateCommandWithStoredProcedure("SP_GetUser_ByToken", con, paramDic);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            User u = new();

            if (dataReader.Read())
            {
                u.Email = dataReader["Email"].ToString();
                u.Id = dataReader["Id"].ToString();
                u.Password = dataReader["Password"].ToString();
                u.FirstName = dataReader["FirstName"].ToString();
                u.LastName = dataReader["LastName"].ToString();
                u.ProfilePictureUrl = dataReader["ProfilePicture"].ToString();
                //u.BioDescription = dataReader["BioDescription"].ToString();
                u.Birthday = Convert.ToDateTime(dataReader["BirthDate"]);
                u.Gender = dataReader["Gender"].ToString();
                u.Token = dataReader["Token"].ToString();
            }
            return u;

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    public User GetUserInfoById(string id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@UserID", id);

        cmd = CreateCommandWithStoredProcedure("SP_GetUserDetailsById", con, paramDic);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            User u = new();

            if (dataReader.Read())
            {
                u.Email = dataReader["Email"].ToString();
                u.Id = dataReader["Id"].ToString();
                u.FirstName = dataReader["FirstName"].ToString();
                u.LastName = dataReader["LastName"].ToString();
                u.ProfilePictureUrl = dataReader["ProfilePicture"].ToString();
                //u.BioDescription = dataReader["BioDescription"].ToString();
                u.Birthday = Convert.ToDateTime(dataReader["BirthDate"]);
                u.Type = dataReader["Type"].ToString();
                u.Gender = dataReader["Gender"].ToString();
            }
            return u;

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    public User GetUserDbId(string token)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@Token", token);

        cmd = CreateCommandWithStoredProcedure("SP_GetUser_ByToken", con, paramDic);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            User u = new();

            if (dataReader.Read())
            {
                u.Id = dataReader["Id"].ToString();
                u.Email = dataReader["Email"].ToString();
                u.Password = dataReader["Password"].ToString();
                u.FirstName = dataReader["FirstName"].ToString();
                u.LastName = dataReader["LastName"].ToString();
                u.ProfilePictureUrl = dataReader["ProfilePicture"].ToString();
                //u.BioDescription = dataReader["BioDescription"].ToString();
                u.Birthday = Convert.ToDateTime(dataReader["BirthDate"]);
                u.Gender = dataReader["Gender"].ToString();
                u.Token = dataReader["Token"].ToString();
            }
            return u;

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    public User UploadImage(string id, string imageURL)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@Id", id);
        paramDic.Add("@ProfilePictureURL", imageURL);


        cmd = CreateCommandWithStoredProcedure("SP_UpdateProfilePicture", con, paramDic);             // create the command
        var returnParameter = cmd.Parameters.Add("@returnValue", SqlDbType.Int);

        returnParameter.Direction = ParameterDirection.ReturnValue;


        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (!dataReader.HasRows)
            {
                return null;
            }
            User u = new();
            while (dataReader.Read())
            {
                u.Email = dataReader["Email"].ToString();
                u.Id = dataReader["Id"].ToString();
                u.Password = dataReader["Password"].ToString();
                u.FirstName = dataReader["FirstName"].ToString();
                u.LastName = dataReader["LastName"].ToString();
                u.ProfilePictureUrl = dataReader["ProfilePicture"].ToString();
                //u.BioDescription = dataReader["BioDescription"].ToString();
                u.Birthday = Convert.ToDateTime(dataReader["BirthDate"]);
                u.Gender = dataReader["Gender"].ToString();
                u.Token = dataReader["Token"].ToString();
            }
            return u;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
            // note that the return value appears only after closing the connection
            var result = returnParameter.Value;
        }

    }
    public string RegisterUser(User user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();

        paramDic.Add("@Email", user.Email);
        paramDic.Add("@Id", user.Id);
        paramDic.Add("@Password", user.Password);
        paramDic.Add("@FirstName", user.FirstName);
        paramDic.Add("@LastName", user.LastName);
        paramDic.Add("@BirthDate", user.Birthday);
        //paramDic.Add("@BioDescription", user.Bio);
        paramDic.Add("@Gender", user.Gender);
        // Generate token and ensure it doesn't exceed 250 characters
        string token = GenerateToken(user.LastName + user.Email); // Example: Concatenate last name and email
        if (token.Length > 250)
        {
            token = token.Substring(0, 250); // Trim token to 250 characters if necessary
        }
        paramDic.Add("@Token", token);

        cmd = CreateCommandWithStoredProcedure("SP_RegisterUser", con, paramDic);  // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            //int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
            return token;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    public List<User> SearchUsers(string key)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@SearchKeyword", key);

        cmd = CreateCommandWithStoredProcedure("SP_SearchUsersByName", con, paramDic);             // create the command


        List<User> searchResults = new();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dataReader.Read())
            {
                User u = new User();
                u.Id = dataReader["Id"].ToString();
                u.FirstName = dataReader["FirstName"].ToString();
                u.LastName = dataReader["LastName"].ToString();
                u.ProfilePictureUrl = dataReader["ProfilePicture"].ToString();

                searchResults.Add(u);
            }
            return searchResults;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    //--------------------------------------------------------------------------------------------------
    //----------VET METHODES
    //--------------------------------------------------------------------------------------------------
    public List<Vet> GetVerifiedVets(Vet vetFilters)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        if (vetFilters.City != null)
            paramDic.Add("@City", vetFilters.City);
        if (vetFilters.Specialization != null)
            paramDic.Add("@Specialization", vetFilters.Specialization);
        if (vetFilters.Availability24_7 != null)
            paramDic.Add("@Availability24_7", vetFilters.Availability24_7);
        if (vetFilters.SellsProducts != null)
            paramDic.Add("@SellsProducts", vetFilters.SellsProducts);
        if (vetFilters.VetToHome != null)
            paramDic.Add("@VetToHome", vetFilters.VetToHome);
        paramDic.Add("@Type", vetFilters.Type);



        cmd = CreateCommandWithStoredProcedure("SP_GetVerifiedBusiness", con, paramDic);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            List<Vet> vets = new();

            while (dataReader.Read())
            {
                Vet v = new();
                v.Id = dataReader["Id"].ToString();
                v.UserId = dataReader["UserId"].ToString();
                v.DisplayName = dataReader["DisplayName"].ToString();
                v.City = dataReader["City"].ToString(); 
                v.Address = dataReader["Address"].ToString();
                v.Phone = dataReader["Phone"].ToString();
                v.ProfileImage = dataReader["ProfileImage"].ToString();
                v.Description = dataReader["Description"].ToString();
                v.Specialization = dataReader["Specialization"].ToString();
                v.RatingScore = Convert.ToInt32(dataReader["Ratings"]);
                v.Availability24_7 = Convert.ToBoolean(dataReader["Availability24_7"]);
                v.SellsProducts = Convert.ToBoolean(dataReader["SellsProducts"]);
                v.VetToHome = Convert.ToBoolean(dataReader["VetToHome"]);
                v.ActiveWoofr = Convert.ToBoolean(dataReader["ActiveWoofr"]);
                v.Notes = dataReader["Notes"].ToString();
                v.Type = dataReader["Type"].ToString();
                vets.Add(v);
            }
            return vets;

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    } 
    public Vet GetVerifiedVetById(string id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        
            paramDic.Add("@UserId", id);


        cmd = CreateCommandWithStoredProcedure("SP_GetVetByUserId", con, paramDic);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           

            if (dataReader.Read())
            {
                Vet v = new();
                v.Id = dataReader["Id"].ToString();
                v.UserId = dataReader["UserId"].ToString();
                v.DisplayName = dataReader["DisplayName"].ToString();
                v.City = dataReader["City"].ToString(); 
                v.Address = dataReader["Address"].ToString();
                v.Phone = dataReader["Phone"].ToString();
                v.ProfileImage = "";
                v.Description = dataReader["Description"].ToString();
                v.Specialization = dataReader["Specialization"].ToString();
                v.RatingScore = Convert.ToInt32(dataReader["Ratings"]);
                v.Availability24_7 = Convert.ToBoolean(dataReader["Availability24_7"]);
                v.SellsProducts = Convert.ToBoolean(dataReader["SellsProducts"]);
                v.VetToHome = Convert.ToBoolean(dataReader["VetToHome"]);
                v.ActiveWoofr = Convert.ToBoolean(dataReader["ActiveWoofr"]);
                v.Notes = dataReader["Notes"].ToString();
                return v;

            }
            return null;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    } 
    
    public Chat StartChat(Chat chat)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@ChatId", chat.ChatID);
        paramDic.Add("@Participant2ID", chat.Participant2ID);
        paramDic.Add("@Participant1ID", chat.Participant1ID);

        cmd = CreateCommandWithStoredProcedure("SP_CreateChat", con, paramDic);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            Chat c =  new();

            if (dataReader.Read())
            {
                c.ChatID = dataReader["ChatID"].ToString();
                c.Participant1ID = dataReader["Participant1ID"].ToString();
                c.Participant2ID = dataReader["Participant2ID"].ToString();
                c.Participant1UnreadCount = Convert.ToInt32(dataReader["Participant1UnreadCount"]);
                c.Participant2UnreadCount = Convert.ToInt32(dataReader["Participant2UnreadCount"]);
                c.Timestamp = Convert.ToDateTime(dataReader["Timestamp"]);
                c.LastMessage = dataReader["LastMessage"].ToString();

            }
            return c;

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    public List<Woof> GetUserPosts(string id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@UserID", id);

        cmd = CreateCommandWithStoredProcedure("SP_GetUserPosts", con, paramDic);             // create the command


        List<Woof> posts = new();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Woof p = new();
                p.Id = dataReader["Id"].ToString();
                p.Content = dataReader["Content"].ToString();
                p.MediaUrl = dataReader["MediaUrl"].ToString();
                p.UserId = dataReader["UserID"].ToString();
                p.CreatedAt = Convert.ToDateTime(dataReader["CreatedAt"]);

                posts.Add(p);
            }
            return posts;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }       
    public List<Review> GetReviewsByProUserId(string id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@ProUserId", id);

        cmd = CreateCommandWithStoredProcedure("SP_GetReviewsByProUserId", con, paramDic);             // create the command


        List<Review> reviews = new();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Review r = new();
                r.Id = dataReader["Id"].ToString();
                r.ProUserId = dataReader["ProUserId"].ToString();
                r.Rating = Convert.ToInt32(dataReader["Rating"]);
                r.UserId = dataReader["UserId"].ToString();
                r.ReviewText = dataReader["ReviewText"].ToString();
                r.DatePosted = Convert.ToDateTime(dataReader["DatePosted"]);

                reviews.Add(r);
            }
            return reviews;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }      
    public List<Woof> GetHomePagePosts(string id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@UserID", id);

        cmd = CreateCommandWithStoredProcedure("SP_GetUserPostsAndFollowingPosts", con, paramDic);             // create the command


        List<Woof> posts = new();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Woof p = new();
                p.Id = dataReader["Id"].ToString();
                p.Content = dataReader["Content"].ToString();
                p.MediaUrl = dataReader["MediaUrl"].ToString();
                p.UserId = dataReader["UserID"].ToString();
                p.CreatedAt = Convert.ToDateTime(dataReader["CreatedAt"]);
                posts.Add(p);
            }
            return posts;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }         
    public List<Chat> GetUsersChat(string id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@UserID", id);

        cmd = CreateCommandWithStoredProcedure("SP_GetUserChats", con, paramDic);             // create the command


        List<Chat> chats = new();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Chat c = new();
                c.ChatID = dataReader["ChatID"].ToString();
                c.Participant1ID = dataReader["Participant1ID"].ToString();
                c.Participant2ID = dataReader["Participant2ID"].ToString();
                c.Participant1UnreadCount = Convert.ToInt32(dataReader["Participant1UnreadCount"]);
                c.Participant2UnreadCount = Convert.ToInt32(dataReader["Participant2UnreadCount"]);
                c.LastMessage = dataReader["LastMessage"].ToString();
                c.Timestamp = Convert.ToDateTime(dataReader["Timestamp"]);

                chats.Add(c);
            }
            return chats;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }           
    public List<Message> GetChatMessages(string id, string readerId)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@ChatID", id);
        paramDic.Add("@ReaderID", readerId);

        cmd = CreateCommandWithStoredProcedure("SP_GetChatMessages", con, paramDic);             // create the command


        List<Message> messages = new();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Message m = new();
                m.ChatId = dataReader["ChatID"].ToString();
                m.MessageId = dataReader["MessageID"].ToString();
                m.SenderId = dataReader["SenderID"].ToString();
                m.ReceiverId = dataReader["ReceiverID"].ToString();
                m.MessageText = dataReader["MessageText"].ToString();
                m.Timestamp = Convert.ToDateTime(dataReader["Timestamp"]);
                messages.Add(m);
            }
            
            return messages;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }    
    public List<User> GetUserLikesByPost(string id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@PostId", id);

        cmd = CreateCommandWithStoredProcedure("SP_GetPostLikes", con, paramDic);             // create the command


        List<User> users = new();

        try
        {

            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                User u = new();
                u.Id = dataReader["Id"].ToString();
                u.ProfilePictureUrl = dataReader["ProfilePicture"].ToString();
                u.FirstName = dataReader["FirstName"].ToString();
                u.LastName = dataReader["LastName"].ToString();
                users.Add(u);
            }
            return users;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }           
    public List<User> GetUserFollowersByUserId(string id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@UserId", id);

        cmd = CreateCommandWithStoredProcedure("SP_GetUserFollowersByUserId", con, paramDic);             // create the command


        List<User> users = new();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                User u = new();
                u.Id = dataReader["Id"].ToString();
                u.ProfilePictureUrl = dataReader["ProfilePicture"].ToString();
                u.FirstName = dataReader["FirstName"].ToString();
                u.LastName = dataReader["LastName"].ToString();
                users.Add(u);
            }
            return users;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }               
    public List<User> GetUserFollowingsByUserId(string id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@UserId", id);

        cmd = CreateCommandWithStoredProcedure("SP_GetUserFollowingsByUserId", con, paramDic);             // create the command


        List<User> users = new();

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                User u = new();
                u.Id = dataReader["Id"].ToString();
                u.ProfilePictureUrl = dataReader["ProfilePicture"].ToString();
                u.FirstName = dataReader["FirstName"].ToString();
                u.LastName = dataReader["LastName"].ToString();
                users.Add(u);
            }
            return users;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    public int FollowUnfollowUser(string follower,string followed)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@FollowerID", follower);
        paramDic.Add("@FollowedID", followed);

        cmd = CreateCommandWithStoredProcedure("SP_FollowUnfollowUser", con, paramDic);             // create the command
        
        SqlParameter outputParam = new SqlParameter("@OutputStatus", SqlDbType.Bit);
        outputParam.Direction = ParameterDirection.Output;
        cmd.Parameters.Add(outputParam);

        try
        {
            cmd.ExecuteNonQuery();
            int outputStatus = Convert.ToInt32(cmd.Parameters["@OutputStatus"].Value);
            return outputStatus;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    public int InsertPost(Woof w)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();

        paramDic.Add("@Id", w.Id);
        paramDic.Add("@Content", w.Content);
        paramDic.Add("@UserID", w.UserId);
        paramDic.Add("@MediaUrl", w.MediaUrl);

        cmd = CreateCommandWithStoredProcedure("SP_InsertNewPost", con, paramDic);  // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            //int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    public int InsertReview(Review r)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();

        paramDic.Add("@ProUserId", r.ProUserId);
        paramDic.Add("@ReviewText", r.ReviewText);
        paramDic.Add("@UserId", r.UserId);
        paramDic.Add("@Rating", r.Rating);
        paramDic.Add("@Id", r.Id);

        cmd = CreateCommandWithStoredProcedure("SP_AddReviewAndUpdateRating", con, paramDic);  // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            //int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    public int RegisterVet(Vet v)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();

        paramDic.Add("@Id", v.Id);
        paramDic.Add("@DisplayName", v.DisplayName);
        paramDic.Add("@Address", v.Address);
        paramDic.Add("@Phone", v.Phone);
        paramDic.Add("@Description", v.Description);
        paramDic.Add("@Specialization", v.Specialization);
        paramDic.Add("@Availability24_7", v.Availability24_7);
        paramDic.Add("@SellsProducts", v.SellsProducts);
        paramDic.Add("@VetToHome", v.VetToHome);
        paramDic.Add("@Notes", v.Notes);
        paramDic.Add("@UserId", v.UserId);

        cmd = CreateCommandWithStoredProcedure("SP_RegisterVet", con, paramDic);  // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            //int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    public int UpdateUserBio(string bio, string token)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();

        paramDic.Add("@BioDescription", bio);
        paramDic.Add("@Token", token);

        cmd = CreateCommandWithStoredProcedure("SP_UpdateUserBio", con, paramDic);  // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            //int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    public int AddMessage(Message m)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();

        paramDic.Add("@MessageId", m.MessageId);
        paramDic.Add("@ChatID", m.ChatId);
        paramDic.Add("@SenderID", m.SenderId);
        paramDic.Add("@ReceiverID", m.ReceiverId);
        paramDic.Add("@MessageText", m.MessageText);

        cmd = CreateCommandWithStoredProcedure("SP_AddMessageToChat", con, paramDic);  // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            //int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    public int Delete(string id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();

        paramDic.Add("@PostID", id);

        cmd = CreateCommandWithStoredProcedure("SP_DeletePostByID", con, paramDic);  // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            //int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    public int LikePost(string post_id,string user_id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();

        paramDic.Add("@PostId", post_id);
        paramDic.Add("@UserID", user_id);

     

        cmd = CreateCommandWithStoredProcedure("SP_LikePost", con, paramDic);  // create the command
        SqlParameter outputParam = new SqlParameter("@IsLiked", SqlDbType.Bit);
        outputParam.Direction = ParameterDirection.Output;
        cmd.Parameters.Add(outputParam);

        try
        {
            cmd.ExecuteNonQuery(); // execute the command
            int outputStatus = Convert.ToInt32(cmd.Parameters["@IsLiked"].Value);
            return (outputStatus);

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    } 
    
    public int EditProfile(User userData)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();

        paramDic.Add("@Token", userData.Token);
        paramDic.Add("@ProfilePicture", userData.ProfilePictureUrl);
        paramDic.Add("@Password", userData.Password);
        paramDic.Add("@LastName", userData.LastName);
        paramDic.Add("@FirstName", userData.FirstName);
        

     

        cmd = CreateCommandWithStoredProcedure("SP_EditProfile", con, paramDic);  // create the command


        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
                                   //int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
           return (numEffected);

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    public int DeleteProfile(string token)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();

        paramDic.Add("@Token", token);

        cmd = CreateCommandWithStoredProcedure("SP_DeleteUserByToken", con, paramDic);  // create the command


        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
                                   //int numEffected = Convert.ToInt32(cmd.ExecuteScalar()); // returning the id
           return (numEffected);

        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    


    //---------------------------------------------------------------------------------
    // Create the SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedure(String spName, SqlConnection con, Dictionary<string, object> paramDic)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        if(paramDic != null)
            foreach (KeyValuePair<string, object> param in paramDic) {
                cmd.Parameters.AddWithValue(param.Key,param.Value);

            }


        return cmd;
    }
}
