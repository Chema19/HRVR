using System;
using System.Data;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;

public class UserUnity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    List<User> GetUsers()
    {
        List<User> users = new List<User>();
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        builder.DataSource = "localhost";
        builder.UserID = "";
        builder.Password = "";
        builder.InitialCatalog = "BDHomeRepairVR";

        try
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                Debug.Log("connection established");
                // sql command
                string sql = "SELECT MAX(u.[Name]), " +
                "MAX(u.[AboutMe]), " +
                "MAX(u.[UserPrincipalName]), " +
                "string_agg(s.[Name], ', '), " +
                "u.Id FROM [dbo].[Users] u " +
                "inner join [dbo].[UserSkills] us " +
                "on us.UserId = u.Id " +
                "inner join [dbo].[Skills] s " +
                "on us.SkillId = s.Id " +
                "group by u.Id";
                // execute sql command
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // read
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // each line in the output
                        while (reader.Read())
                        {
                            // to avoid SqlNullValueException
                            if (!reader.IsDBNull(0) & !reader.IsDBNull(1) & !reader.IsDBNull(3))
                            {
                                // Skills list to be attached to each user object
                                List skills = new List();
                                // get output parameters
                                string username = reader.GetString(0);
                                string aboutString = reader.GetString(1);
                                string skillsString = reader.GetString(3);
                                // as we are getting a list of skills as 
                                // a single string delimited by comma
                                // we split the string
                                string[] skillsList = skillsString.Split(',');
                                // we now iterate through each skill to initialize our
                                // skill object and put it into skills list
                                foreach (string skillName in skillsList)
                                {
                                    // initialize a skill object with a trimmed string
                                    Skill skill = new Skill(skillName.Trim());
                                    // append to the skills array
                                    skills.Add(skill);
                                }
                                // initialize User oobject
                                User user = new User(username.Trim(), aboutString.Trim(), skills);
                                users.Add(user);
                            }
                        }
                    }
                }
            }
        }
        catch (SqlException e)
        {
            Debug.Log(e.ToString());
        }
        return 
    }

    public class User
    {
        public Int32 UserId { set; get; }
        public String UserName { set; get; }
        public String UserUrlPicture { set; get; }
        public DateTime? DateCreate { set; get; }
        public String Status { set; get; }
        public DateTime? DateUpdate { set; get; }

        public User(int userId, string username, string userurlpicture, DateTime? datecreate, string status, DateTime? dateupdate)
        {
            this.UserId = userId;
            this.UserName = username;
            this.UserUrlPicture = userurlpicture;
            this.DateCreate = datecreate;
            this.Status = status;
            this.DateUpdate = dateupdate;
        }
    }
}
