﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.WebPages;
using ATMBot.Reminder;
using Dapper;
using DSharpPlus.CommandsNext;

namespace ATMBot.Reminder
{
    class Reminderdto
    {
        public int Id { get; set; }
        public string User_Id { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }

    class Reminder
    {
        public static DateTime ParseReminderTime(string input)
        {
            DateTime output = input.AsDateTime();
            return output;
        }

        public static void SaveReminder(CommandContext ctx, string message, DateTime time)
        {
            string id = ctx.User.Id.ToString();
            using (IDbConnection db = new SqlConnection(SqlController.conn))
            {
                db.Execute("INSERT INTO Reminders (User_Id, Message, Time) VALUES (@name, @mess, @time)", new { name = id, mess = message, time });
            }
        }

        public static List<Reminderdto> GetReminders()
        {
            List<Reminderdto> list = new List<Reminderdto>();
            using (IDbConnection db = new SqlConnection(SqlController.conn))
            {
                list = db.Query<Reminderdto>("SELECT * FROM Reminders").ToList();
            }
            return list;
        }

        public static void RemoveReminders(List<Reminderdto> list)
        {
            using (IDbConnection db = new SqlConnection(SqlController.conn))
            {
                foreach (Reminderdto current in list)
                {
                    db.Execute("DELETE FROM Reminders WHERE Id=@id", new { id = current.Id });
                }
            }

        }
    }
}
