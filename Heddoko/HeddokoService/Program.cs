﻿/**
 * @file Program.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Hangfire;
using Services;

namespace HeddokoService
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (Environment.UserInteractive || args.Contains("-h") || args.Contains("-flush"))
            {

                try
                {
                    if (args.Count() == 0)
                    {
                        HangFireOption option = HangFireOptions.Get();
                        BackgroundJobServer Server = new BackgroundJobServer(option.Options, option.Storage);
                        JobStorage.Current = option.Storage;
                        Trace.TraceInformation("Server is starting");
                        Trace.TraceInformation("Server is stoping");
                        Server.Dispose();
                    }
                    foreach (string arg in args)
                    {
                        switch (arg)
                        {
                            case "-i":
                            case "-install":
                                Install(true);
                                break;
                            case "-u":
                            case "-uninstall":
                                Install(false);
                                break;
                            case "-h":
                                Trace.TraceInformation("Server is starting");
                                HangFireOption option = HangFireOptions.Get();
                                BackgroundJobServer Server = new BackgroundJobServer(option.Options, option.Storage);
                                JobStorage.Current = option.Storage;
                                Trace.TraceInformation("Server is stoping");
                                Server.Dispose();
                                break;
                            case "-m":
                                Trace.TraceInformation("Server is pending");
                                List<string> migrations = DatabaseManager.Pending().ToList();
                                migrations.ForEach(Console.WriteLine);
                                Trace.TraceInformation("Server is migrating");

                                DatabaseManager.Migrate();

                                Trace.TraceInformation("Server is migrated");
                                break;
                            case "-r":
                                Trace.TraceInformation("Server is rollback");
                                string target = Console.ReadLine();
                                Trace.TraceInformation($"Revert to {target}");

                                DatabaseManager.Rollback(target);

                                Trace.TraceInformation("Server is reverted");
                                break;
                            case "-p":
                                List<string> migros = DatabaseManager.Pending().ToList();

                                migros.ForEach(Console.WriteLine);
                                break;
                            case "-flush":
                                Trace.TraceInformation("FLUSHALL is started");

                                RedisManager.Flush();

                                Trace.TraceInformation("FLUSHALL is stoped");
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError("Main :" + ex);
                }
            }
            else
            {
                ServiceBase.Run(new HeddokoService());
            }
        }

        public static void Install(bool install)
        {
            try
            {
                Console.WriteLine(install ? "Installing" : "Uninstalling");
                using (AssemblyInstaller Installer = new AssemblyInstaller(typeof(Program).Assembly, new string[] {
                    "/name=" + Config.ServiceName,
                }))
                {
                    IDictionary state = new Hashtable();
                    Installer.UseNewContext = true;
                    try
                    {
                        if (install)
                        {
                            Installer.Install(state);
                            Installer.Commit(state);
                        }
                        else
                        {
                            Installer.Uninstall(state);
                        }
                    }
                    catch
                    {
                        try
                        {
                            Installer.Rollback(state);
                        }
                        catch { }
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Install :" + ex);
            }
        }
    }
}
