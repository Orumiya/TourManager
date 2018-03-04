// <copyright file="Logger.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager.Helper
{
    using System;

    /// <summary>
    /// The class is to help debugging, providing useful log messages
    /// Example:
    /// Logger.AddLogMethod(Console.WriteLine);
    /// Logger.AddLogMethod(m =>
    ///       { using (StreamWriter sw =
    ///            new StreamWriter("log.txt", true))
    ///            {
    ///              sw.WriteLine(m);
    ///            }
    ///  });
    /// Logger.Log("az adatbáziskapcsolat felépült");
    /// </summary>
    public static class Logger
    {
        private static Action<string> log;

        public static void AddLogMethod(Action<string> logMethod)
        {
            log += logMethod;
        }

        public static void Log(string message)
        {
            log("[" + DateTime.Now.ToString() + "]" + message);
        }
    }
}
